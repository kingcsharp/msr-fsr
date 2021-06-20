using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Abstractions;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.Answer.Models;
using Newtonsoft.Json;

namespace MSR.Infrastructure.Resources.Answer
{
    public class AnswerClient: IAnswerRestClient
    {
        private readonly HttpClient _httpClient;
        
        public AnswerClient(HttpClient client)
        {
            _httpClient = client;
        }
        
        public async Task<string> PostWorkOrderAsync(CreateWorkOrder command, CancellationToken cancellationToken = default)
        {
            
            var request = JsonConvert.SerializeObject(command);
            var post = new HttpRequestMessage(HttpMethod.Post, "/v1/WorkOrder/Create")
            {
                Content = new StringContent(request, Encoding.UTF8, "application/json")
            };
            post.Headers.Add("Authorization", $"Bearer {CurrentUser.GetTokenString()}");

            var httpResponse = await _httpClient.SendAsync(post, cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new DomainException($"Error attempting to create WorkOrder: {httpResponse.StatusCode}", DomainError.InternalServerError);
            }
            
            var result = await httpResponse.Content.ReadAsStringAsync();

            var answerResponse = JsonConvert.DeserializeObject<AnswerResponse>(result);

            if (answerResponse.ErrorMessages.Any())
            {
                throw new DomainException($"Error attempting to create WorkOrder: {string.Join("|-|", answerResponse.ErrorMessages.ToList())}", DomainError.RemoteServerError);
            }

            return answerResponse.Object;
        }
    }
}