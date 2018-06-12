using System.Threading;
using xNet;

namespace LinkShorter
{
    public static class Shorter
    {
        public static string Short(string link)
        {
            try
            {
                HttpRequest request = new HttpRequest();

                request.Cookies = new CookieDictionary();

                request.UserAgent = Http.ChromeUserAgent();

                string s = request.Get("https://bitly.com").ToString();

                string xsrfToken = s.Substring("name=\"_xsrf\" value=\"", "\"");

                request.AddHeader("X-XSRFToken", xsrfToken);

                RequestParams shortParams = new RequestParams();

                shortParams["url"] = link;

                Thread.Sleep(3000);

                s = request.Post("https://bitly.com/data/shorten", shortParams).ToString();

                link = s.Substring("\"aggregate_link\": \"", "\"");

                return link;
            }

            catch
            {
                return link;
            }
        }
    }
}
