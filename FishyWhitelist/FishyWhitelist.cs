using Fishy.Extensions;
using Fishy.Models.Packets;
using Fishy.Models;
namespace FishyWhitelist
{
    public class FishyWhitelist : FishyExtension
    {

        FishyWhitelistConfig config = new FishyWhitelistConfig();
        public override void OnPlayerJoin(Player player)
        {
            if (!config.WhitelistEnabled)
                return;
            if (!FishyWhitelistConfig.GetWhiteList().Contains(player.SteamID.ToString()))
                return;
            try
            {
                SendPacketToPlayer(new MessagePacket(config.KickMessage, config.KickMessageColor), player.SteamID);
                KickPlayer(player);
                Log($"Kicked player {player.Name} {player.SteamID} for not being on whitelist!");
            }
            catch (Exception e)
            {
                LogError(e.ToString());
            }
        }
        public override void OnInit()
        {
            refreshConfig();
        }

        private void refreshConfig()
        {
            FishyWhitelistConfig.GetWhiteList(); // Generate default whitelist if none exists;
            Log("Refreshing config...");
            string configPath = Path.Combine(Environment.CurrentDirectory, "whitelistConfig.toml");
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, Tomlyn.Toml.FromModel(config));
            }
            try
            {
                config = Tomlyn.Toml.ToModel<FishyWhitelistConfig>(File.ReadAllText(configPath));
            }
            catch (Exception e)
            {
                LogError("couldn't read TOML!" + e.ToString());
            }
            LogSuccess("Refreshed config!");
        }

        public static void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[FISHYWHITELIST] ");
            Console.ForegroundColor = color;
            Console.Write(message + "\n");
            Console.ForegroundColor = prevColor;
        }
        public static void LogError(string message)
        {
            Log(message, ConsoleColor.Red);
        }
        public static void LogSuccess(string message)
        {
            Log(message, ConsoleColor.Green);
        }
    }
}
