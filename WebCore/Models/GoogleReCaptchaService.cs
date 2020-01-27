using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebCore.Models
{
    public class GoogleReCaptchaService
    {
        private ReCAPTCHASettings _settings;
        public GoogleReCaptchaService(IOptions<ReCAPTCHASettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual async Task<GoogleRespo> VerifyreCaptcha(string token)
        {
            GoogleCaptchaData myData = new GoogleCaptchaData
            {
                Response = token,
                Secret = _settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={myData.Secret}&response={myData.Response}");
            var capresp = JsonConvert.DeserializeObject<GoogleRespo>(response);
            return capresp;
        }
    }

    public class GoogleCaptchaData
    {
        public string Response { get; set; } //token
        public string Secret { get; set; } //token
    }

    public class GoogleRespo
    {
        public bool success { get; set; }
        //public double score { get; set; }   
        //public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }


    }
}
