namespace ServerDLL
{
    public interface ServerInterface
    {
        void createUser(string username);
        void createChatrooom(string chatname);
        Boolean usernameExists(string username);
        void joinChatroom(string username, string chatname);
        void sendMessage(string message, string chatname);

    }
}