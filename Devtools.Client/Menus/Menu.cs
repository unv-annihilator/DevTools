using System;
using System.Collections.Generic;
using System.Linq;

namespace Devtools.Client.Menus {
    public class Menu : List<MenuItem> {

        private int _index;

        public Menu(string title, Menu parent = null) {
            Title = title;
            Parent = parent;
        }

        public string Title { get; }
        public Menu Parent { get; }

        public int CurrentIndex
        {
            get => Math.Max(0, Math.Min(_index, VisibleCount - 1));
            set
            {
                if (value < 0) value = VisibleCount - 1;
                _index = Math.Max(0, value % VisibleCount);

                var item = GetVisibleItems().ElementAt(_index);
                item.Select.Invoke();
            }
        }

        private int VisibleCount => GetVisibleItems().Count();

        public MenuItem CurrentItem => GetVisibleItems().ElementAt(CurrentIndex);

        public virtual void OnEnter() {
        }

        public virtual void OnExit() {
        }

        protected new void Remove(MenuItem i) {
            if (base.Remove(i))
                Refresh();
        }

        public new void Add(MenuItem item) {
            base.Add(item);
            Refresh();
        }

        public new void AddRange(IEnumerable<MenuItem> items) {
            base.AddRange(items);
            Refresh();
        }

        private void Refresh() {
            Sort((a, b) => b.Priority - a.Priority);
            CurrentIndex = _index;
        }

        public IEnumerable<MenuItem> GetVisibleItems() {
            return this.Where(i => i.IsVisible);
        }
    }

}