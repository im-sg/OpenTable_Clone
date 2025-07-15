using Newtonsoft.Json;

namespace OpenTable.Models
{
    public class Cookies
    {
        public static List<ReservationItem> ReservationCookies(HttpRequest request, string cookieName)
        {
            if (request.Cookies.TryGetValue(cookieName, out string? cookieValue))
            {
                return JsonConvert.DeserializeObject<List<ReservationItem>>(cookieValue) ?? new List<ReservationItem>();
            }
            return new List<ReservationItem>();
        }
    }
}
