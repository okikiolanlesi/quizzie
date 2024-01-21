using System;


using MimeKit;
using MailKit.Net.Smtp;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Quizzie.Services;
public class EmailService : IEmailService
{
    private readonly string _templatesFolderPath;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public EmailService(string templatesFolderPath, ILogger logger, IConfiguration configuration)
    {
        _templatesFolderPath = templatesFolderPath;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendHtmlEmailAsync(string toEmail, string subject, string templateFileName, object model)
    {
        var templateFileNameWithSuffix = templateFileName + ".html";

        var templatePath = Path.Combine(_templatesFolderPath, templateFileNameWithSuffix);

        System.Console.WriteLine(templatePath);

        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template file not found: {templateFileNameWithSuffix}");
        }

        var templateContent = await File.ReadAllTextAsync(templatePath);
        var mergedContent = MergeTemplateWithModel(templateContent, model);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Quizzie", _configuration.GetSection("AppSettings:EmailUsername").Value));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = mergedContent
        };

        message.Body = bodyBuilder.ToMessageBody();



        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync(_configuration.GetSection("AppSettings:EmailUsername").Value, _configuration.GetSection("AppSettings:EmailPassword").Value);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        _logger.LogDebug($"{templateFileName} email sent to {toEmail}");
    }

    private string MergeTemplateWithModel(string templateContent, object model)
    {
        // Replace placeholders in the template with actual values from the model
        // This can be done in various ways depending on your templating needs
        // For simplicity, this example uses string.Replace
        foreach (var property in model.GetType().GetProperties())
        {
            var placeholder = $"{{{{{property.Name}}}}}";
            var value = property.GetValue(model)?.ToString();
            templateContent = templateContent.Replace(placeholder, value);
        }

        return templateContent;
    }
}

