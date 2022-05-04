using Application.DTO;

namespace Application.Common
{
    public static class Extensions
    {
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static long GetLongValue(this List<Stats> coll, string statsName)
        {
            return coll.FirstOrDefault(x => x.Name == statsName).Value;
        }
    }
}
