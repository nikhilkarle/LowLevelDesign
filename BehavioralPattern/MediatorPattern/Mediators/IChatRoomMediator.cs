using MediatorPattern.Colleagues;

namespace MediatorPattern.Mediators
{
    public interface IChatRoomMediator
    {
        void Send(string fromUser, string toUser, string message);
        void Broadcast(string fromUser, string message);
        void Register(User user);
    }
}