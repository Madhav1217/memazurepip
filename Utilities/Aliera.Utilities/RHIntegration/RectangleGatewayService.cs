using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Text = System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Aliera.Utilities.RHIntegration
{
    public class RectangleGatewayService : IRectangleGatewayService
    {
        public async Task<RHPaymentResponse> AddPaymentDetails(RectangleHealthCustomer rectangleHealthCustomer, IOptions<AppSettings> appSettings, bool isUpdate)
        {
            // Make get API call 
            using (var client = new HttpClient())
            {
                string input = ConvertObectToXMLToString(rectangleHealthCustomer, isUpdate);
                var response = await client.PostAsync(appSettings.Value.RHGatewayURL, GetHttpContent(input));
                return FromXml<RHPaymentResponse>(await ReadResponseMessage(response));
            }
        }

        public async Task<RHPaymentResponse> DeletePaymentDetails(DeleteRectangleHealthCustomer deleteRectangleHealthCustomer,
         IOptions<AppSettings> appSettings)
        {
            using (var client = new HttpClient())
            {
                string input = ConvertObectToXMLToString(deleteRectangleHealthCustomer);
                var response = await client.PostAsync(appSettings.Value.RHGatewayURL, GetHttpContent(input));
                return FromXml<RHPaymentResponse>(await ReadResponseMessage(response));
            }
        }

        public async Task<RHCompleteTransactionResponse> CompletePaymentAction(string tokenId, IOptions<AppSettings> appSettings)
        {
            using (var client = new HttpClient())
            {
                RHCompleteAction completeActionWithRH = new RHCompleteAction();
                completeActionWithRH.APIKey = appSettings.Value.RHAPIKey;
                completeActionWithRH.TokenId = tokenId;
                string result = ConvertObectToXMLToString(completeActionWithRH, false);
                var response = await client.PostAsync(appSettings.Value.RHGatewayURL, GetHttpContent(result));
                return FromXml<RHCompleteTransactionResponse>(await ReadResponseMessage(response));
            }
        }

        public async Task<RHVaultResponse> GetPaymentDetails(string vaultId, IOptions<AppSettings> appSettings)
        {
            using (var client = new HttpClient())
            {
                string queryString = string.Format("username={0}&password={1}&report_type=customer_vault&customer_vault_id={2}",
                                                   appSettings.Value.RHUserName, appSettings.Value.RHPassword, vaultId);
                string uri = string.Format("{0}{1}", appSettings.Value.RHQueryAPIURL, queryString);
                var response = await client.GetAsync(new Uri(uri));
                return FromXml<RHVaultResponse>(await ReadResponseMessage(response));
            }
        }

        private async Task<RHPaymentResponse> SendPaymentDetailsToRH(RectangleHealthCustomer rectangleHealthCustomer, IOptions<AppSettings> appSettings, bool IsUpdateCustomer)
        {
            using (var client = new HttpClient())
            {
                string result = ConvertObectToXMLToString(rectangleHealthCustomer, IsUpdateCustomer);
                var response = await client.PostAsync(appSettings.Value.RHGatewayURL, GetHttpContent(result));
                return FromXml<RHPaymentResponse>(await ReadResponseMessage(response));
            }
        }

        private async Task<string> ReadResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            var bytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            return Text.Encoding.UTF8.GetString(bytes);
        }

        private StringContent GetHttpContent(string result)
        {
            var content = new StringContent(result);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            return content;
        }

        private string ConvertObectToXMLToString(object businessModel, bool isUpdateCustomer = false)
        {
            string result = string.Empty;
            XmlSerializer xmlSerializer;

            if (isUpdateCustomer)
            {
                xmlSerializer = new XmlSerializer(businessModel.GetType(), RootOverride());
            }
            else
            {
                xmlSerializer = new XmlSerializer(businessModel.GetType());
            }

            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, businessModel);
                xmlStream.Position = 0;
                result = new StreamReader(xmlStream).ReadToEnd();
            }
            return result;
        }

        private static XmlRootAttribute RootOverride() => new XmlRootAttribute("update-customer");

        private T FromXml<T>(string xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        // String passed is not XML, simply return defaultXmlClass
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return returnedXmlClass;
        }
    }

    [XmlRoot("add-customer")]
    public class RectangleHealthCustomer
    {
        [XmlElement("api-key")]
        public string APIKey { get; set; }

        [XmlElement("redirect-url")]
        public string RedirectURL { get; set; }

        [XmlElement("customer-vault-id")]
        public string CustomerVaultId { get; set; }

        [XmlElement("merchant-defined-field-1")]
        public string Merchant1 { get; set; }

        [XmlElement("merchant-defined-field-2")]
        public string Merchant2 { get; set; }

        [XmlElement("billing")]
        public RectangleBillingInfo Billing { get; set; }

        [XmlElement("shipping")]
        public string Shipping { get; set; }
    }

    public class RectangleBillingInfo
    {
        [XmlElement("billing-id")]
        public string Id { get; set; }

        [XmlElement("first-name")]
        public string FirstName { get; set; }

        [XmlElement("last-name")]
        public string LastName { get; set; }

        [XmlElement("address1")]
        public string Address1 { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("postal")]
        public string Postal { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("company")]
        public string Company { get; set; }

        [XmlElement("address2")]
        public string Address2 { get; set; }

        [XmlElement("fax")]
        public string Fax { get; set; }

        [XmlElement("account-type")]
        public string AccountType { get; set; }

        [XmlElement("entity-type")]
        public string EntityType { get; set; }

        [XmlElement("priority")]
        public string Priority { get; set; }
    }

    public class RHBillingResponse
    {
        [XmlElement("billing-id")]
        public string Id { get; set; }

        [XmlElement("first-name")]
        public string FirstName { get; set; }

        [XmlElement("last-name")]
        public string LastName { get; set; }

        [XmlElement("address1")]
        public string Address1 { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("postal")]
        public string Postal { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("cc-number")]
        public string CCNumber { get; set; }

        [XmlElement("cc-exp")]
        public string CCExp { get; set; }      

        [XmlElement("priority")]
        public string Priority { get; set; }

        [XmlElement("account-name")]
        public string AccountName { get; set; }

        [XmlElement("account-number")]
        public string AccountNumber { get; set; }

        [XmlElement("routing-number")]
        public string RoutingNumber { get; set; }

        [XmlElement("account-type")]
        public string AccountType { get; set; }
    }

    [XmlRoot("response")]
    public class RHPaymentResponse
    {
        [XmlElement("result")]
        public string Result { get; set; }

        [XmlElement("result-text")]
        public string Text { get; set; }

        [XmlElement("result-code")]
        public string Code { get; set; }

        [XmlElement("form-url")]
        public string FormURL { get; set; }
    }

    [XmlRoot("response")]
    public class RHCompleteTransactionResponse
    {
        [XmlElement("result")]
        public string Result { get; set; }

        [XmlElement("result-text")]
        public string Text { get; set; }

        [XmlElement("result-code")]
        public string Code { get; set; }

        [XmlElement("transaction-id")]
        public string TransactionId { get; set; }

        [XmlElement("customer-id")]
        public string CustomerId { get; set; }

        [XmlElement("customer-vault-id")]
        public string CustomerVaultId { get; set; }

        [XmlElement("action-type")]
        public string ActionType { get; set; }

        [XmlElement("amount")]
        public string Amount { get; set; }

        [XmlElement("billing")]
        public RHBillingResponse Billing { get; set; }     
    }

    [XmlRoot("complete-action")]
    public class RHCompleteAction
    {
        [XmlElement("api-key")]
        public string APIKey { get; set; }

        [XmlElement("token-id")]
        public string TokenId { get; set; }
    }

    [XmlRoot("delete-customer")]
    public class DeleteRectangleHealthCustomer
    {
        [XmlElement("api-key")]
        public string APIKey { get; set; }

        [XmlElement("customer-vault-id")]
        public string CustomerVaultId { get; set; }
    }

    [XmlRoot("nm_response")]
    public class RHVaultResponse
    {
        [XmlElement("customer_vault")]
        public RHCustomerVault RHCustomerVault { get; set; }
    }

    public class RHCustomerVault
    {
        [XmlElement("customer")]
        public RHCustomer Customer { get; set; }
    }

    public class RHCustomer
    {
        [XmlElement("first_name")]
        public string FirstName { get; set; }

        [XmlElement("last_name")]
        public string LastName { get; set; }

        [XmlElement("cc_number")]
        public string CreditCardNumber { get; set; }

        [XmlElement("cc_exp")]
        public string CreditExp { get; set; }

        [XmlElement("customer_vault_id")]
        public string VaultId { get; set; }

        [XmlElement("account_type")]
        public string AccountType { get; set; }

        [XmlElement("check_account")]
        public string ACHAccount { get; set; }
    }

}
