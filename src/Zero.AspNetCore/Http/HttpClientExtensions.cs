namespace Zero.AspNetCore.Http
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 发送请求并且执行重试
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="requestMessage"></param> 
        /// <param name=""></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendWithRetryAsync(this HttpClient httpClient, HttpRequestMessage requestMessage, CancellationToken cancellationToken, int retryTimes = 3)
        {
            Polly.Retry.AsyncRetryPolicy poily = Policy.Handle<HttpRequestException>()
                          .WaitAndRetryAsync(retryTimes,
                          retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                          (exception, timeSpan) =>
                          {

                          });
            return await poily.ExecuteAsync(async () =>
            {
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationToken: cancellationToken);
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException($"发送请求异常，`{requestMessage.RequestUri}`。");
                return response;
            });
        }
    }
}
