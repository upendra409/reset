using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tasks.backend.initservice.Models;
using tasks.backend.initservice.Services;

namespace tasks.backend.initservice.Controllers
{
    [ApiController]
    [Route("initapi/v1/[controller]")]
    public class InitialController : ControllerBase
    {
        private readonly ILogger<InitialController> _logger;
        private readonly IExchangeRate _exchangeRate;
        private readonly IPublishEventService _publishEventService;
        private string eventLog;
        public InitialController(ILogger<InitialController> logger
            , IExchangeRate exchangeRate
            , IPublishEventService publishEventService)
        {
            _logger = logger;
            _exchangeRate = exchangeRate;
            _publishEventService = publishEventService;
        }

        [HttpGet]
        [Route("hpu")]
        public async Task<IActionResult> Get()
        {
            eventLog = "api get call --" + System.Guid.NewGuid().ToString() + " : " + System.DateTimeOffset.Now.ToString();
            Console.WriteLine(eventLog);
            string jsonString = "{name:Bruce Wayne, title:The Dark Knight}";
            //return Ok(await _taskService.GetTasks("Kingslayer"));
            eventLog = "api get call ++" + System.Guid.NewGuid().ToString() + " : " + System.DateTimeOffset.Now.ToString();
            Console.WriteLine(eventLog);
            return Ok(jsonString);
        }
        [HttpGet("dcu/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string jsonString = "{name:Tony Stark, title:Iron Man}";
            var data = await _exchangeRate.GetRate();
            //return Ok(await _taskService.GetTasks("Kingslayer"));
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Event evt)
        {
            eventLog = "api post call --" + System.Guid.NewGuid().ToString() + " : " + System.DateTimeOffset.Now.ToString();
            Console.WriteLine(eventLog);
            await _publishEventService.PublishEvent(evt);
            eventLog = "api post call ++" + System.Guid.NewGuid().ToString() + " : " + System.DateTimeOffset.Now.ToString();
            Console.WriteLine(eventLog);
            return Ok(System.Guid.NewGuid().ToString());
        }

    }
    public class Event
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
