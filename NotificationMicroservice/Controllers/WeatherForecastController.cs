//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using NotificationEngine;
//using NotificationMicroservice.Services;

//namespace NotificationMicroservice.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };

//        private readonly ILogger<WeatherForecastController> _logger;
//        private readonly IHubContext<MessageHub, IMessageHubClient> _messageHub;

//        private readonly IPolicyExpiryService _PolicyExpiryService;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<MessageHub, IMessageHubClient> messageHub, IPolicyExpiryService PolicyExpiryService)
//        {
//            _logger = logger;
//            _messageHub = messageHub;
//            _PolicyExpiryService = PolicyExpiryService;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }

//        [HttpPost(Name = "Notifications")]
//        public string GetMessages()
//        {
//            List<string> msg = new List<string>();
//            msg.Add("Hi");
//            msg.Add("Good morning");

//            _messageHub.Clients.All.SendMessage(msg);
//            return "Messages sent successfully";
//        }

//        // POST: api/notification/send-expiring-policies
//        // This endpoint retrieves expiring policies for a given ClientId and sends the message to the SignalR group
//        [HttpPost("send-expiring-policies")]
//        public async Task<IActionResult> SendExpiringPolicies([FromQuery] int clientId)
//        {
//            try
//            {

//                // Fetch policies that are expiring in 15 days for this ClientId
//                var expiringPolicies = await _PolicyExpiryService.GetExpiringPoliciesForClient(clientId);

//                if (!expiringPolicies.Any())
//                {
//                    return NotFound($"No expiring policies found for client {clientId}");
//                }

//                // Prepare the message to be sent
//                var message = expiringPolicies.Select(p => $"Policy Number {p.Number} is due to expire on {p.ExpirationDate:yyyy-MM-dd}").ToList();

//                // Send the message to the clients in the SignalR group associated with this ClientId
//                await _messageHub.Clients.Group(clientId.ToString()).SendMessage(message);

//                //await _messageHub.Clients.SendMessageToClient(clientId.ToString());

//                return Ok("Message sent to client " + clientId);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }


//    }
//}
