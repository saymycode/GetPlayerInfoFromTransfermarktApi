using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace getplayers
{
    class Program
    {
        static async Task Main()
        {
            int i = 0;

            while (true)
            {
                string playerIdApiUrl = "https://transfermarkt-api.vercel.app/players/" + i.ToString() + "/profile";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(playerIdApiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            Objects.Player player = JsonConvert.DeserializeObject<Objects.Player>(responseBody);

                            response = await client.GetAsync("https://transfermarkt-api.vercel.app/clubs/search/" + player.club.lastClubName + "?page_number=1");
                            responseBody = await response.Content.ReadAsStringAsync();
                            Objects.ClubResponse responseClub = JsonConvert.DeserializeObject<Objects.ClubResponse>(responseBody);

                            string connectionString = "Server=yourHost;Database=yourDb;User ID=yourUserId;Password=yourPassword;Trusted_Connection=False";
                            using (var context = new Objects.PlayerDbContext(connectionString))
                            {
                                context.Database.CreateIfNotExists();

                                var playerInfo = new Objects.PlayerInfo
                                {
                                    Name = player.name,
                                    PlaceOfBirth = player.placeOfBirth.country.ToString(),
                                    Age = player.age,
                                    Team = player.club.name,
                                    Position = player.position.main,
                                    League = responseClub.results[0].Country,
                                    IsRetired = player.isRetired
                                };

                                context.PlayerInfoes.Add(playerInfo);
                                context.SaveChanges();
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine($"Error Code: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Message: {ex.Message}");
                    }
                }
                i++;
            }
        }
    }
}
