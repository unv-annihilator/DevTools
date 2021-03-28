using System;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using Devtools.Client.Helpers;
using Devtools.Client.Menus;

namespace Devtools.Client.Controllers {
    public class TimeCycleMenu : Menu {
        public TimeCycleMenu(Client client, Menu parent) : base("Time Cycles", parent) {
            foreach (TimeCycle cycle in Enum.GetValues(typeof(TimeCycle)))
                Add(new MenuItemTimeCycle(client, this, Enum.GetName(typeof(TimeCycle), cycle) ?? "default"));
        }

        private class MenuItemTimeCycle : MenuItem {

            public MenuItemTimeCycle(Client client, Menu owner, string label, int priority = -1) : base(client, owner, label, priority) {
            }

            protected override Task OnActivate() {
                Function.Call(Hash.SET_TIMECYCLE_MODIFIER, Label);
                return base.OnActivate();
            }
        }
    }
}