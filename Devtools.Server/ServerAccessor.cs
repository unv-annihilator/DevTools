namespace Devtools.Server {
    public class ServerAccessor {

        protected ServerAccessor(Server server) {
            Server = server;
        }

        protected Server Server { get; }
    }
}