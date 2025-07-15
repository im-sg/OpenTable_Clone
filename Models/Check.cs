using OpenTable.Models.DataLayer;

namespace OpenTable.Models
{
    public static class Check
    {
        public static string EmailExists(OpenTableDbContext ctx, string email)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(email)) {
                var customer = ctx.Customers.FirstOrDefault(
                    c => c.EmailAddress.ToLower() == email.ToLower());
                if (customer != null) 
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }
    }
}