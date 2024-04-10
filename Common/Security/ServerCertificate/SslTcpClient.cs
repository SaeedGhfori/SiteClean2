using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security.ServerCertificate
{
    public class SslTcpClient
    {
        public async Task RunClientAsync(string machineName, string serverName)
        {
            // Create a TCP/IP client socket.
            TcpClient client = new TcpClient(machineName, 443);

            // Create an SSL stream that will close the client's stream.
            using (SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null))
            {
                // The server name must match the name on the server certificate.
                try
                {
                    await sslStream.AuthenticateAsClientAsync(serverName);
                }
                catch (AuthenticationException e)
                {
                    // Handle exception
                    // Log the exception using a logging framework or throw it
                    client.Close();
                    throw;
                }

                // Encode a test message into a byte array.
                byte[] message = Encoding.UTF8.GetBytes("Hello from the client.");
                // Send hello message to the server. 
                await sslStream.WriteAsync(message, 0, message.Length);
                await sslStream.FlushAsync();
            }
        }

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors == SslPolicyErrors.None;
        }
    }

}
