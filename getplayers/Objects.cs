using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getplayers
{
    public class Objects
    {
        public class PlayerInfo
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }

            public string PlaceOfBirth { get; set; }

            public string Age { get; set; }

            public string Team { get; set; }

            public string League { get; set; }

            public string Position { get; set; }

            public bool IsRetired { get; set; }

            // Diğer özellikleri buraya ekleyin.
        }
        public class PlayerDbContext : DbContext
        {
            public PlayerDbContext(string connectionString) : base(connectionString)
            {
            }

            public DbSet<PlayerInfo> PlayerInfoes { get; set; }
        }
        public class Player
        {
            public string id { get; set; }
            public string url { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string imageURL { get; set; }
            public string dateOfBirth { get; set; }
            public PlaceOfBirth placeOfBirth { get; set; }
            public string age { get; set; }
            public List<string> citizenship { get; set; }
            public bool isRetired { get; set; }
            public string retiredSince { get; set; }
            public Position position { get; set; }
            public Club club { get; set; }
            public string updatedAt { get; set; }
        }

        public class PlaceOfBirth
        {
            public string city { get; set; }
            public string country { get; set; }
        }

        public class Position
        {
            public string main { get; set; }
            public List<string> other { get; set; }
        }

        public class Club
        {
            public string name { get; set; }
            public string lastClubID { get; set; }
            public string lastClubName { get; set; }
            public string mostGamesFor { get; set; }
        }
        public class ClubResponse
        {
            public string query { get; set; }
            public int pageNumber { get; set; }
            public int lastPageNumber { get; set; }
            public List<ClubResult> results { get; set; }
            public string updatedAt { get; set; }
        }

        public class ClubResult
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string Squad { get; set; }
            public string MarketValue { get; set; }
        }
    }
}

