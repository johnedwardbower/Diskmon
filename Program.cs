using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Windows;
using System.Net;
using System.IO;

namespace DiskMon_Webhook_
{

    public class SlackClient
    {
        private readonly Uri _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public SlackClient(Uri webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task<HttpResponseMessage> SendMessageAsync(string message,
            string channel = null, string username = null)
        {
            var payload = new
                {
                    text = message,
                    channel,
                    username,
                };
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var response = await _httpClient.PostAsync(_webhookUrl,
            new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
            return response;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
            {
                Task.WaitAll(IntegrateWithSlackAsync());  
            }

        private static async Task IntegrateWithSlackAsync()
            {
            var drives = DriveInfo.GetDrives();
            



            foreach (DriveInfo info in drives)
                {
                if (info.IsReady)
                    {
                    Console.WriteLine(info.Name + " " + info.TotalFreeSpace/(1024*1024*1024) + " GB");
                    if (info.TotalFreeSpace/(1024 * 1024 * 1024) <= 10)
                        {
                            var message = "Alert Low Space\nServer: " + Dns.GetHostName() + "\nDrive: " + info.Name + "\nSpace left: " + (info.TotalFreeSpace / 1024 / 1024 / 1024) + " GB";
                            var webhookUrl = new Uri("https://hooks.slack.com/services/putYourUniqueWebhookInfoHere");
                            var slackClient = new SlackClient(webhookUrl);
                            var response = await slackClient.SendMessageAsync(message);
                            var isValid = response.IsSuccessStatusCode ? "valid" : "invalid";
                        }
                    

                    }
                }
        
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday) 
            {
                Console.WriteLine("This is a weekend");
                var message = "Just checking in: " + Dns.GetHostName();
                var webhookUrl = new Uri("https://hooks.slack.com/services/putYourUniqueWebhookInfoHere");
                var slackClient = new SlackClient(webhookUrl);
                var response = await slackClient.SendMessageAsync(message);
                var isValid = response.IsSuccessStatusCode ? "valid" : "invalid";
        }
    }
}
}
