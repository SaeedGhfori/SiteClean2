using RestSharp;

namespace Site.Infrastructure.Helpers.Webhook
{
    public class WebhookSender : IWebhookSender
    {
        private readonly RestClient _client;
        private readonly string _webhookUrl;
        public WebhookSender(string baseUrl, string webhookUrl)
        {
            _client = new RestClient(baseUrl);
            _webhookUrl = webhookUrl;
        }

        public void SendWebhook(WebhookPayload payload)
        {
            var request = new RestRequest(_webhookUrl, Method.Post);
            request.AddJsonBody(payload);
            var response = _client.Execute(request);
            if (!response.IsSuccessful)
                throw new IntegrationException(response.Content);
        }

        public async Task SendWebhookAsync(WebhookPayload payload)
        {
            var request = new RestRequest(_webhookUrl, Method.Post);
            request.AddJsonBody(payload);
            var response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new IntegrationException(response.Content);
        }

        public void SendBatchWebhook(IEnumerable<WebhookPayload> payloads)
        {
            foreach (var payload in payloads)
            {
                SendWebhook(payload);
            }
        }
    }

    public class WebhookPayload
    {
        public string EventType { get; set; }
        public string VerifyToken { get; set; }
        public dynamic Data { get; set; }
    }
}
