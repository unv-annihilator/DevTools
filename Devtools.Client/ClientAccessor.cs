namespace Devtools.Client {
    public class ClientAccessor {

        protected ClientAccessor(Client client) {
            Client = client;
        }

        protected Client Client { get; }
    }
}