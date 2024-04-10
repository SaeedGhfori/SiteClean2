using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace Site.Infrastructure.Helpers.SoapClient
{
    public class SoapClientHelper : ISoapClient
    {
        public string Get(string url, string action, string soapEnvelopeXml)
        {
            HttpWebRequest request = CreateWebRequest(url, action);
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(soapEnvelopeXml);
            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeDocument.Save(stream);
            }
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return soapResult;
                }
            }
        }

        public string Post(string url, string action, string soapEnvelopeXml)
        {
            HttpWebRequest request = CreateWebRequest(url, action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, request);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    return soapResult;
                }
            }
        }

        public HttpWebRequest CreateWebRequest(string url, string action)
        {
            // ایجاد یک درخواست وب برای SOAP
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public void InsertSoapEnvelopeIntoWebRequest(string soapEnvelopeXml, HttpWebRequest webRequest)
        {
            // وارد کردن SOAP Envelope به درخواست وب
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(soapEnvelopeXml);
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeDocument.Save(stream);
            }
        }

        public async Task<string> SendAsync(string url, string action, string soapEnvelopeXml, string method)
        {
            var request = CreateWebRequest(url, action, method);
            await InsertSoapEnvelopeIntoWebRequestAsync(soapEnvelopeXml, request);

            using (var response = await request.GetResponseAsync())
            {
                using (var rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = await rd.ReadToEndAsync();
                    return soapResult;
                }
            }
        }

        public HttpWebRequest CreateWebRequest(string url, string action, string method)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = method;
            return webRequest;
        }

        public async Task InsertSoapEnvelopeIntoWebRequestAsync(string soapEnvelopeXml, HttpWebRequest request)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(soapEnvelopeXml);

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                soapEnvelopeDocument.Save(stream);
            }
        }

    }
}
