namespace Devtools.Server {
    public class ServerAccessor {

        public ServerAccessor(Server server) {
            Server = server;
        }

        protected Server Server { get; }
    }
}