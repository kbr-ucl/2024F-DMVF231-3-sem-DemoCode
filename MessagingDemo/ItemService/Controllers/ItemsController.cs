using IntegrationEvents;
using ItemService.Model;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemService.Controllers;

[Route("item")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public ItemsController(ILogger<ItemsController> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    //// GET: api/<ItemsController>
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new[] { "value1", "value2" };
    //}

    //// GET api/<ItemsController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    // POST api/<ItemsController>
    [HttpPost]
    public async Task<IActionResult> Post(ItemDto item)
    {
        await _publishEndpoint.Publish<ItemCreatedEvent>(new ItemCreatedEvent
        {
            Id = Guid.NewGuid(),
            Name = item.Name,
            Description = item.Description
        });

        return Ok();
    }

    //// PUT api/<ItemsController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<ItemsController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}