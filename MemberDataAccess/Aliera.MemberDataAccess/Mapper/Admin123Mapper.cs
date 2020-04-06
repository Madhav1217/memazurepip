using Aliera.BusinessObjects.Broker;
using Aliera.DatabaseEntities.Models;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess.Mapper
{
    public class Admin123Mapper
    {
        private readonly IUnitOfWork _unitOfWork;

        public Admin123Mapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Posts the URI asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="httpContent">Content of the HTTP.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="memberObject">The member object.</param>
        /// <param name="admin123IdOrExternalId">The admin123 identifier or external identifier.</param>
        /// <returns></returns>
        public async Task<string> PostURIAsync(Uri uri, HttpContent httpContent, string userName, string password,
            string memberObject, string admin123IdOrExternalId)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", userName, password))));
                var url = string.Format("{0}{1}.json", uri, admin123IdOrExternalId);

                var result = await client.PostAsync(url, httpContent);

                if (result.IsSuccessStatusCode)
                {
                    var adminResponse = await result.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<RESPONSERootObject>(adminResponse);
                    if (responseObj != null)
                    {
                        if (!responseObj.SUCCESS)
                        {
                            response = BrokerConstants.ADMIN123_ERROR_MSG;
                        }
                        else
                        {
                            response = result.StatusCode.ToString();
                        }
                    }
                    else
                    {
                        response = BrokerConstants.ADMIN123_ERROR_MSG;
                    }
                    await InsertAdmin123LogAsync(memberObject, adminResponse);
                }
            }

            return response;
        }

        /// <summary>
        /// Inserts the admin123 log asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="response">The response.</param>
        public async Task InsertAdmin123LogAsync(string content, string response)
        {
            var admin123Log = new Admin123Log
            {
                Content = content,
                Response = response,
                CreatedOn = DateTime.UtcNow,
                Type = (int)EntityType.Member
            };
            await _unitOfWork.GetRepository<Admin123Log>().InsertAsync(admin123Log);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}