using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniHubApi.Infra.Soap
{
    public interface ISoapService
    {
        string Url { get; set; }

        string Token{ get; set; }

        Task<string> PostSoapAsync(string xml);

        Task<T> PostSoapAsync<T>(T xml);
    }
}
