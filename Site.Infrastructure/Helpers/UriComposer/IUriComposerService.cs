using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Helpers.UriComposer
{
    public interface IUriComposerService
    {
        string ComposeImageUri(string src);
    }

    public class UriComposerService : IUriComposerService
    {
        public string ComposeImageUri(string src)
        {
            return "https://localhost:7131/" + src.Replace("\\", "//");
        }
    }
}
