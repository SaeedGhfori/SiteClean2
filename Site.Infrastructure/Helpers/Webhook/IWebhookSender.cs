using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Helpers.Webhook
{
    public interface IWebhookSender
    {
        void SendWebhook(WebhookPayload payload);
        Task SendWebhookAsync(WebhookPayload payload);
        void SendBatchWebhook(IEnumerable<WebhookPayload> payloads);
    }
}

