using System;
using CitizenFX.Core;

namespace LXGaming.Discord.Server {

    public class Discord : BaseScript {

        public Discord() {
            EventHandlers["discord:initialize"] += new Action<Player>(OnInitialize);
        }

        private void OnInitialize([FromSource] Player player) {
            player.TriggerEvent("discord:initialize", "657192358726729739");
            player.TriggerEvent("discord:update_activity", "", "redm", "", "", "");
        }
    }
}