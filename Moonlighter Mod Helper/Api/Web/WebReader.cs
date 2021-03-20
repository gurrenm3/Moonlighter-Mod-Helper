using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Moonlighter_Mod_Helper.Api.Web
{
    /// <summary>
    /// Class for reading text from a URL
    /// </summary>
    public class WebReader
    {
        /// <summary>
        /// Downloads string of text from the URL. Needs to be run on a thread if using UI, otherwise the UI will freeze
        /// </summary>
        public string ReadText_FromURL(string url)
        {
            //Guard.ThrowIfArgumentIsNull(url, "Can't read text from url. Url argument is null", "url");
            var webClient = CreateWebClient();

            string downloadedText = "";
            string lastExeption = "";
            for (int i = 0; i <= 250; i++)
            {
                try
                {
                    downloadedText = webClient.DownloadString(url);
                    if (!String.IsNullOrEmpty(downloadedText))
                        break;
                }
                catch (Exception e)
                {
                    if (e.Message == lastExeption)
                        continue;

                    Console.WriteLine(e.Message);
                    lastExeption = e.Message;
                }
            }
            return downloadedText;
        }

        private WebClient CreateWebClient()
        {
            WebClient client = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            client.Headers.Add("user-agent", " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            return client;
        }
    }
}
