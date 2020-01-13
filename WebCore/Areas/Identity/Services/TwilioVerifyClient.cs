﻿using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCore.Areas.Identity.Models;

namespace WebCore.Areas.Identity.Services
{
    public class TwilioVerifyClient
    {
        private readonly HttpClient _client;

        public TwilioVerifyClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<TwilioSendVerificationCodeResponse> StartVerification(int countryCode, string phoneNumber)
        {
            var requestContent = new FormUrlEncodedContent(new[] {
               new KeyValuePair<string, string>("via", "sms"),
               new KeyValuePair<string, string>("country_code", countryCode.ToString()),
               new KeyValuePair<string, string>("phone_number", phoneNumber),
           });

            var response = await _client.PostAsync("protected/json/phones/verification/start", requestContent);

            var content = await response.Content.ReadAsStringAsync();

            // this will throw if the response is not valid
            return JsonConvert.DeserializeObject<TwilioSendVerificationCodeResponse>(content);
        }
        public async Task<TwilioCheckCodeResponse> CheckVerificationCode(int countryCode, string phoneNumber, string verificationCode)
        {
            var queryParams = new Dictionary<string, string>()
           {
               {"country_code", countryCode.ToString()},
               {"phone_number", phoneNumber},
               {"verification_code", verificationCode },
           };

            var url = QueryHelpers.AddQueryString("protected/json/phones/verification/check", queryParams);

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            // this will throw if the response is not valid
            return JsonConvert.DeserializeObject<TwilioCheckCodeResponse>(content);
        }


    }
}
