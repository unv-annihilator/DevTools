using System;
using System.Threading.Tasks;

namespace Devtools.Client.Menus {
    public class MenuItem {
        private static int _menuItems = short.MaxValue;

        internal readonly Func<Task> Select, Left, Right;
        internal Func<Task> Activate;

        public MenuItem(Client client, Menu owner, string label, int priority = -1) {
            Client = client;
            Menu = owner;
            Label = label;
            _menuItems--;
            Priority = priority < 0 ? _menuItems : priority;

            Select += OnSelect;
            Activate += OnActivate;
            Left += OnLeft;
            Right += OnRight;
        }

        public Menu Menu { get; }

        public int Priority { get; }

        public string Label { get; protected set; }

        protected Client Client { get; }

        public virtual bool IsVisible { get; } = true;

        protected virtual Task OnSelect() {
            return Task.FromResult(0);
        }

        protected virtual Task OnActivate() {
            return Task.FromResult(0);
        }

        protected virtual Task OnLeft() {
            return Task.FromResult(0);
        }

        protected virtual Task OnRight() {
            return Task.FromResult(0);
        }
    }
}