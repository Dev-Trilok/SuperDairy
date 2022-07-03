namespace SuperDairy.Models
{
    public class Common
    {
        private static string connstring="";
        public static string GetConnectionString()
        {
            if (connstring != "")
                return connstring;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            Common.connstring = configuration.GetValue<string>("ConnectionStrings:value");
            return connstring;
        }
    }
}
