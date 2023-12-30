using System;
using System.Threading.Tasks;

namespace Quizzie.Services;

public interface IEmailService
{
    Task SendHtmlEmailAsync(string toEmail, string subject, string templateFileName, object model);
}
