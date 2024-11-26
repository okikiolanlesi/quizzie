using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")] // Root endpoint
public class RootController : ControllerBase
{
    [HttpGet]
    public ActionResult GetRootResponse()
    {
        // Example JSON response
        var response = new
        {
            Message = "Welcome to the Quizzard API!",
            Status = "Success",
            Documentation = "https://documenter.getpostman.com/view/22751768/2s9YsT6os6",
            Timestamp = DateTime.UtcNow
        };

        return Ok(response); // Returns a 200 OK with JSON
    }
}