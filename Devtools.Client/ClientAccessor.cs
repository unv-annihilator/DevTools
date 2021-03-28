namespace Devtools.Client {
    public class ClientAccessor {

        public ClientAccessor(Client client) {
            Client = client;
        }

        protected Client Client { get; }
    }
}