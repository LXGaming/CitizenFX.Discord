using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace LXGaming.Discord.Server {

    public class Discord : BaseScript {

        private string ApplicationId { get; set; }

        public Discord() {
            EventHandlers["onServerResourceStart"] += new Action<string>(OnServerResourceStart);

            EventHandlers["discord:initialize"] += new Action<Player>(OnInitialize);
        }

        private void OnServerResourceStart(string resourceName) {
            if (API.GetCurrentResourceName() != resourceName) {
                return;
            }

            ApplicationId = API.GetResourceMetadata(API.GetCurrentResourceName(), "application_id", 0);
        }

        private void OnInitialize([FromSource] Player player) {
            if (string.IsNullOrEmpty(ApplicationId)) {
                return;
            }

            player.TriggerEvent("discord:initialize", ApplicationId);
            player.TriggerEvent("discord:update_activity", "", "redm", "", "", "");
        }
    }
}