using Dapr;
using Hello.Crosscut.IntegrationMessages;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Subscribe.Controllers;

[Route("[controller]")]
[ApiController]
public class SubscribeHelloController : ControllerBase
{
    private readonly ILogger<SubscribeHelloController> _logger;

    public SubscribeHelloController(ILogger<SubscribeHelloController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> HelloReceived(HelloMessage hello)
    {
        _logger.LogInformation($"Received Hello message: {hello.Message}");
        await Task.CompletedTask;
        return Ok();
    }
}