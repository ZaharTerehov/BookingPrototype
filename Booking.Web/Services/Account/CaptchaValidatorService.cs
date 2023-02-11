using Booking.Web.Interfaces.Account;
using Booking.Web.Models.Account;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Booking.Web.Services.Account
{
    public class CaptchaValidatorService : ICaptchaValidator
    {
        private const string _remoteAddress = "https://www.google.com/recaptcha/api/siteverify";
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly CaptchaConfig _captchaConfig;

        public CaptchaValidatorService(IHttpClientFactory httpClient, IOptions<CaptchaConfig> captchaConfig)
        {
            _httpClientFactory = httpClient;
            _captchaConfig = captchaConfig.Value;
        }

        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            dynamic response = await GetCaptchaResultDataAsycn(token);

            if(response.success = true)
                return Convert.ToDouble(response.score) >= _captchaConfig.AcceptableScore;

            return false;
        }

        private async Task<JObject> GetCaptchaResultDataAsycn(string token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _captchaConfig.SecretKey),
                new KeyValuePair<string, string>("response", token)
            });

            using var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(_remoteAddress, content);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException(response.ReasonPhrase);

            var jsonResult = await response.Content.ReadAsStringAsync();

            return JObject.Parse(jsonResult);
        }
    }
}
