using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using tasks.backend.initservice.Controllers;

namespace tasks.backend.initservice.Services
{
    public interface IPublishEventService
    {
        Task PublishEvent(Event evt);
    }
    public class PublishEventService : IPublishEventService
    {
        private IConfiguration _configuration;
        private string _eventserviceUrl;
        private string _xapikey;
        private string _eventLog;
        private HttpClient _httpClient;
        public PublishEventService(IConfiguration configuration,IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient("namedClient");
            _eventserviceUrl = _configuration["Eventservice:url"];
            Console.WriteLine(_eventserviceUrl);
            _xapikey = _configuration["TasksXApiKey"];
            Console.WriteLine(_xapikey);
        }
        public async Task PublishEvent(Event evt)
        {
            _eventLog = "Publish Event -- " + System.Guid.NewGuid().ToString() + " : " + System.DateTimeOffset.Now.ToString();
            Console.WriteLine(_eventLog);
            Console.WriteLine(_eventserviceUrl);
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _eventserviceUrl);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(evt);

            httpRequestMessage.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _xapikey);
            Console.WriteLine(_eventLog);
            //await _httpClient.SendAsync(httpRequestMessage);
            Console.WriteLine(_eventLog);

        }

    }

}