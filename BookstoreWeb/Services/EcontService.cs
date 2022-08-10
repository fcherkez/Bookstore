using BookStore.Web.Econt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Web.Services
{
    public class EcontService
    {
        public HttpClient Client { get; set; }

        public EcontService(HttpClient client)
        {
            this.Client = client;
        }

        public async Task<LabelResponse> CreateLabel()
        {
            var label = new LabelRequest()
            {
                Mode = "create",
                Label = new Label()
                {
                    SenderClient = new ErClient()
                    {
                        Name = "Иван Иванов",
                        Phones = new List<string>() { "0888888888" }
                    },

                    SenderAddress = new ErAddress()
                    {
                        City = new City()
                        {
                            Country = new Country()
                            {
                                Code3 = "BGR"
                            },
                            Name = "София",
                            PostCode = 1700
                        },

                        Street = "бул. Св. Климент Охридски",
                        Num = 7
                    },

                    ReceiverClient = new ErClient()
                    {
                        Name = "Димитър Димитров",
                        Phones = new List<string>() { "0876543210" }
                    },
                    ReceiverAddress = new ErAddress()
                    {
                        City = new City()
                        {
                            Country = new Country()
                            {
                                Code3 = "BGR"
                            },
                            Name = "София",
                            PostCode = 1000
                        },
                        Street = "Граф Игнатиев",
                        Num = 84
                    },
                    PackCount = 1,
                    ShipmentType = "PACK",
                    ShipmentDescription = "Книги",
                    Weight = 1

                }
            };



            var content = new StringContent(JsonConvert.SerializeObject(label), Encoding.UTF8, "application/json");

            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes("iasp-dev:iasp-dev"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
            var response = await Client.PostAsync("Shipments/LabelService.createLabel.json", content);

            var labelResponse = await response.Content.ReadAsStringAsync();
            var econtResponse = JsonConvert.DeserializeObject<LabelResponse>(labelResponse);

            return econtResponse;

        }
    }
}


