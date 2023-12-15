namespace Agrisustain_Jamaica.Models
{
    public class UserIPAddress
    {
        public string GetUserIpAddress(HttpContext httpContext)
        {
            string ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
            return ipAddress;
        }

    }
}
