namespace DentalStudio.Web.Infrastructure
{
    public class BoolExtensions
    {
        public static string BoolToString(bool b)
        {
            return b ? "Yes" : "No";
        }
    }
}
