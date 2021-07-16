using System;
using Xunit;
using Webapi.Contact.Features.Contacts;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Steeltoe;
using Steeltoe.Common.Extensions;
using Steeltoe.Common.Http;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Http;
using Webapi.Contact.Helpers;
namespace UnitTestContact
{
    public class GetContactProvider
    {
        private HttpResponseMessage response;

     
        public void GetResponse(string url, string opn,HttpContent content=null)
        {
            var client = new HttpClient();
            string baseUrl = "https://localhost/" + url;
            switch (opn)
            {
                case "Get":
                    Task.Run(() => client.GetAsync(baseUrl))
                        .ContinueWith(task => response = task.Result)
                           .Wait();
                    break;
                         
                case "Post":
                    Task.Run(() => client.PostAsync(baseUrl, content))
                        .ContinueWith(task => response = task.Result)
                           .Wait();
                    break;
                case "Put":
                    Task.Run(() => client.PutAsync(baseUrl, content))
                        .ContinueWith(task => response = task.Result)
                           .Wait();
                    break;
                case "Delete":
                    Task.Run(() => client.DeleteAsync(baseUrl))
                        .ContinueWith(task => response = task.Result)
                           .Wait();
                    break;

            };
           

            
        }
        [Fact]
        public void getContactProvider_sucess()
        {
            GetResponse("V1/Contact/2", "Get");
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("S", getContactRespose.Type);
     
        }
        [Fact]
        public void getContactProvider_Failure()
        {
            GetResponse("V1/Contact/2444", "Get");
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("F", getContactRespose.Type);

        }
        [Fact]
        public void deleteContactProvider_sucess()
        {
            GetResponse("V1/Contact/2", "Delete");
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("S", getContactRespose.Type);

        }
        [Fact]
        public void deleteContactProvider_Failure()
        {
            GetResponse("V1/Contact/2444", "Delete");
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("F", getContactRespose.Type);

        }

        [Fact]
        public void insertContactProvider_sucess()
        {
            var requestBody = @"{
                    ""FirstName"":""testFirst"",
                    ""LastName"" = ""testLast"",
                     ""PhoneNumber"" = ""12345"",
                ""Email"" = ""test@gmail.com"",
                ""Status"" = true
                }";
            var requestContent = new StringContent(requestBody, System.Text.Encoding.Default, "application/json");
            GetResponse("V1/Contact/2", "Post",requestContent);
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("S", getContactRespose.Type);

        }
        [Fact]
        public void insertContactProvider_Failure()
        {
            var requestBody = @"{
                    ""FirstName"":"" test"",                  
                     ""PhoneNumber"" = ""12345"",
                ""Email"" = ""test@gmail.com"",
                ""Status"" = true
                }";
            var requestContent = new StringContent(requestBody, System.Text.Encoding.Default, "application/json");
            GetResponse("V1/Contact/2", "Post", requestContent);
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("F", getContactRespose.Type);

        }


        [Fact]
        public void updateContactProvider_sucess()
        {
            var requestBody = @"{
                    ""ContactId"":""7895""
                    ""FirstName"":""testFirst"",
                    ""LastName"" = ""testLast"",
                     ""PhoneNumber"" = ""12345"",
                ""Email"" = ""test@gmail.com"",
                ""Status"" = true
                }";
            var requestContent = new StringContent(requestBody, System.Text.Encoding.Default, "application/json");
            GetResponse("V1/Contact/2", "Put", requestContent);
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("S", getContactRespose.Type);

        }
        [Fact]
        public void updateContactProvider_Failure()
        {
            var requestBody = @"{
                    ""ContactId"":""7895""
                    ""FirstName"":"" test"",                  
                     ""PhoneNumber"" = ""12345"",
                ""Email"" = ""test@gmail.com"",
                ""Status"" = true
                }";
            var requestContent = new StringContent(requestBody, System.Text.Encoding.Default, "application/json");
            GetResponse("V1/Contact/2", "Put", requestContent);
            var getContactRespose = response.Content.ReadAsJsonAsync<SummaryResponse>().Result;

            Assert.Equal("F", getContactRespose.Type);

        }
    }
}
