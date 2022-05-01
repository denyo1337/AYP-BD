using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.MappingProfiles;

namespace Application.DTO
{
    public class UserSteamDtaDto
    {
        public Response Response { get; set; }
    }
    public class Response
    {
        public List<Players> Players{ get; set; }
    }
    public class Players 
    {
        [JsonProperty("personaname")]
        public string SteamNickName { get; set; }
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }
        [JsonProperty("avatarfull")]
        public string AvatarfullUrl { get; set; }
        [JsonProperty("realname")]
        public string RealName { get; set; }
        [JsonProperty("timecreated")]
        public long AccountCreated { get; set; }
        [JsonProperty("loccountrycode")]
        public string SteamNationality { get; set; }
        [JsonProperty("steamid")]
        public string SteamId { get; set; }
        [JsonProperty("personastate")]
        public int IsOnline { get; set; }
    }
}
