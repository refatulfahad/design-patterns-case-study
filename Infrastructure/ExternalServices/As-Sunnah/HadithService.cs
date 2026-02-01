using design_pattern_case_1.ThirdParty.As_Sunnah;
using Polly;
using System.Net;

namespace design_pattern_case_1.ThirdParty
{
    public class HadithService: IHadithService
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy = 
            Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(5, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        //inject 3rd party service
        public HadithService() { }

        public async Task<string> GetHadith()
        {
            //call 3rd party api
            var response = await _retryPolicy.ExecuteAsync(() => Task.FromResult<HttpResponseMessage>(
                new HttpResponseMessage(){
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Sample Hadith from 3rd party service")
            }));
            return await response.Content.ReadAsStringAsync();
        }
    }
}