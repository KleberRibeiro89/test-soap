using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MiniHubApi.Infra.Soap
{
    public class SoapService : ISoapService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; }
        public string Token { get; set; }


        public SoapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> PostSoapAsync(string xml)
        {
            var messageBytes = Encoding.UTF8.GetBytes(xml);
            var content = new ByteArrayContent(messageBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            content.Headers.Add("SOAPAction", Url);

            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Token);
            }

            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            var result = await _httpClient.PostAsync(Url, content);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            throw new ArgumentException(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> PostSoapAsync<T>(T xml)
        {
            var result = string.Empty;
            if (xml != null)
            {
                using var stringWriter = new StringWriter();
                var xmlSerializer = new XmlSerializer(xml.GetType());

                result = await PostSoapAsync(stringWriter.ToString());
            }
            else
            {
                result = await PostSoapAsync(string.Empty);
            }


            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(byteArray);

            var resultObj = new XmlSerializer(typeof(T)).Deserialize(stream);
            return (T)resultObj;
        }

        public async Task<string> GetSoapAsync(string xml)
        {
            var messageBytes = Encoding.UTF8.GetBytes(xml);
            var content = new ByteArrayContent(messageBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            content.Headers.Add("SOAPAction", Url);

            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Token);
            }

            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            var result = await _httpClient.GetAsync(Url);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync();

            throw new ArgumentException(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> GetSoapAsync<T>(T xml)
        {
            var result = string.Empty;
            if (xml != null)
            {
                using var stringWriter = new StringWriter();
                var xmlSerializer = new XmlSerializer(xml.GetType());

                result = await GetSoapAsync(stringWriter.ToString());
            }
            else
            {
                result = await GetSoapAsync(string.Empty);

            }


            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(byteArray);

            var resultObj = new XmlSerializer(typeof(T)).Deserialize(stream);

            return (T)resultObj;
        }
    }
}
