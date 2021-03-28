using System;
using System.Threading.Tasks;

namespace Devtools.Client.Helpers {
    public abstract class CachedValue<T> {
        protected T CachedVal;
        protected DateTime LastUpdate = DateTime.MinValue;

        protected CachedValue(float expirationMs) {
            Expiration = expirationMs;
        }

        public float Expiration { get; }

        public T Value
        {
            get
            {
                if (!((DateTime.UtcNow - LastUpdate).TotalMilliseconds > Expiration)) return CachedVal;

                CachedVal = Update();
                LastUpdate = DateTime.UtcNow;
                return CachedVal;
            }
        }

        protected abstract T Update();
    }

    public abstract class CachedValueAsync<T> : CachedValue<T> {
        private bool _lock;

        protected CachedValueAsync(float expirationMs, T defaultValue = default) : base(expirationMs) {
            CachedVal = defaultValue;
        }

        protected override T Update() {
            if (_lock) return CachedVal;
            _lock = true;

            Task.Factory.StartNew(async () => {
                try {
                    LastUpdate = DateTime.UtcNow;
                    CachedVal = await UpdateAsync();
                }
                catch (Exception ex) {
                    Log.Error(ex);
                }

                _lock = false;
            });
            return CachedVal;
        }

        protected abstract Task<T> UpdateAsync();
    }
}