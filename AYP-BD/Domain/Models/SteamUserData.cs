using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SteamUserData : IEntity
    {
        public long Id { get; set; }
        public string PersonName { get; set; }
        public string ProfileUrl { get; set; }
        public string AvatarfullUrl { get; set; }
        public string RealName { get; set; }
        public string AccountCreated { get; set; }
        public string SteamNationality { get; set; }
        public User User { get; set; }
        public long? UserId { get; set; }

    }
}
