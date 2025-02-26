﻿using System.Linq;
using System.Threading.Tasks;

namespace Devtools.Client.Menus {
    public class MenuItemSubMenu : MenuItem {

        public MenuItemSubMenu(Client client, Menu owner, Menu child, string label = "", int priority = -1) : base(client, owner, label,
            priority) {
            Label = label.Any() ? label : child.Title;
            Child = child;
        }

        public Menu Child { get; }

        protected override Task OnActivate() {
            if (Child != null) Client.Menu.CurrentMenu = Child;
            return Task.FromResult(0);
        }
    }
}