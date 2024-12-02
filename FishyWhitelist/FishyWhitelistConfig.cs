namespace FishyWhitelist
{
    internal class FishyWhitelistConfig
    {
        public bool WhitelistEnabled { get; set; } = true;
        public string KickMessage { get; set; } = "You are not on the server whitelist!";
        public string KickMessageColor { get; set; } = "ff5555";

        public static List<string> GetWhiteList()
        {
            string whiteListPath = Path.Combine(Environment.CurrentDirectory, "whitelist.txt");
            if (!File.Exists(whiteListPath))
                CreateDefaultWhiteList();
            try
            {
                return File.ReadAllText(whiteListPath).Split("\n").ToList();
            }
            catch(Exception ex) 
            {
                FishyWhitelist.LogError( "error reading whitelist!\n" + ex);
            }
            return new List<string>() { };
        }
        private static void CreateDefaultWhiteList()
        {
            string whiteListPath = Path.Combine(Environment.CurrentDirectory, "whitelist.txt");
            File.WriteAllText(whiteListPath, string.Join("\n", new string[]{ "76561197960287930", "76561197960287930" }));
        }
    }
}
