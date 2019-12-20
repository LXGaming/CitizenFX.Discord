using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace LXGaming.Discord.Client {

    public class Discord : BaseScript {

        private bool Initialized { get; set; }
        private string ApplicationId { get; set; }
        private string Detail { get; set; }
        private string Asset { get; set; }
        private string AssetText { get; set; }
        private string AssetSmall { get; set; }
        private string AssetSmallText { get; set; }

        public Discord() {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["onClientResourceStop"] += new Action<string>(OnClientResourceStop);

            EventHandlers["discord:initialize"] += new Action<string>(OnInitialize);
            EventHandlers["discord:update_activity"] += new Action<string, string, string, string, string>(OnUpdateActivity);
        }

        private void OnClientResourceStart(string resourceName) {
            if (API.GetCurrentResourceName() != resourceName) {
                return;
            }

            TriggerServerEvent("discord:initialize");
        }

        private void OnClientResourceStop(string resourceName) {
            if (API.GetCurrentResourceName() != resourceName) {
                return;
            }

            Tick -= UpdateActivity;
            Initialized = false;
        }

        private void OnInitialize(string applicationId) {
            if (ApplicationId == applicationId) {
                return;
            }

            if (applicationId != null) {
                ApplicationId = applicationId;

                if (!Initialized) {
                    Initialized = true;
                    Tick += UpdateActivity;
                }
            } else {
                if (Initialized) {
                    Tick -= UpdateActivity;
                    Initialized = false;
                }

                ApplicationId = null;
                Asset = null;
                AssetText = null;
                AssetSmall = null;
                AssetSmallText = null;
            }
        }

        private void OnUpdateActivity(string detail, string asset, string assetText, string assetSmall, string assetSmallText) {
            Detail = detail;
            Asset = asset;
            AssetText = assetText;
            AssetSmall = assetSmall;
            AssetSmallText = assetSmallText;
        }

        private async Task UpdateActivity() {
            if (!Initialized) {
                return;
            }

            API.SetDiscordAppId(ApplicationId);

            if (!string.IsNullOrEmpty(Detail)) {
                API.SetRichPresence(Detail);
            } else {
                var count = API.NetworkGetNumConnectedPlayers();
                if (count == 1) {
                    API.SetRichPresence("1 player");
                } else {
                    API.SetRichPresence($"{count} players");
                }
            }

            API.SetDiscordRichPresenceAsset(Asset);
            API.SetDiscordRichPresenceAssetText(AssetText);
            API.SetDiscordRichPresenceAssetSmall(AssetSmall);
            API.SetDiscordRichPresenceAssetSmallText(AssetSmallText);

            await Delay(60000);
        }
    }
}