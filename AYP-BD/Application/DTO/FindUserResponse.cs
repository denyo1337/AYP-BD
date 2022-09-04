namespace Application.DTO
{
    public class GetUserSteamIdDto
    {
        public FindUserResponse Response { get; set; }
    }
    public class FindUserResponse
    {
        public string SteamId { get; set; }
        public int Success { get; set; }
    }
}
