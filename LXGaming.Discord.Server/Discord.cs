using System;
using CitizenFX.Core;

namespace LXGaming.Discord.Server {

    public class Discord : BaseScript {

        public Discord() {
            EventHandlers["discord:initialize"] += new Action(OnInitialize);
        }

        private void OnInitialize() {
            TriggerClientEvent("discord:initialize", "657192358726729739");
            TriggerClientEvent("discord:update_activity", "", "redm", "", "", "");
        }
    }
}