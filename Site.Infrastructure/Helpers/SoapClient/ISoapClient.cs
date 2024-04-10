using System.Net;

namespace Site.Infrastructure.Helpers.SoapClient
{
    public interface ISoapClient
    {
        string Get(string url, string action, string soapEnvelopeXml);
        string Post(string url, string action, string soapEnvelopeXml);
        HttpWebRequest CreateWebRequest(string url, string action);
        void InsertSoapEnvelopeIntoWebRequest(string soapEnvelopeXml, HttpWebRequest webRequest);
        Task<string> SendAsync(string url, string action, string soapEnvelopeXml, string method);
        HttpWebRequest CreateWebRequest(string url, string action, string method);
        Task InsertSoapEnvelopeIntoWebRequestAsync(string soapEnvelopeXml, HttpWebRequest request);
    }
}
