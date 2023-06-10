using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace ApiDogsCrud.WebApi.Tests
{
    public class PerformanceTests
    {
        private readonly HttpClient _httpClient;

        public PerformanceTests()
        {
            var application = new WebApplicationFactory<Program>();
            _httpClient = application.CreateClient();
        }

        [Fact]
        public async Task RateLimiting_ShouldAllowTenRequestsPerSecond()
        {
            const string url = "/Dog/ping";
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 1; i <= 15; i++)
            {
                tasks.Add(_httpClient.GetAsync(url));
            }

            await Task.WhenAll(tasks);

            var succcessResponses = tasks.Select(x => x.Result).Where(x => x.StatusCode == HttpStatusCode.OK);
            var failedResponses = tasks.Select(x => x.Result).Where(x => x.StatusCode == HttpStatusCode.TooManyRequests);

            Assert.Equal(10, succcessResponses.Count());
            Assert.Equal(5, failedResponses.Count());
        }
    }
}