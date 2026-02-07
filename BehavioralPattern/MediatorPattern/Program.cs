using System;
using MediatorPattern.Mediators;
using MediatorPattern.Colleagues;

namespace MediatorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IChatRoomMediator chatRoom = new ChatRoomMediator();

            User nikhil = new User("Nikhil", chatRoom);
            User virat = new User("Virat", chatRoom);
            User karle = new User("Karle", chatRoom);

            chatRoom.Register(nikhil);
            chatRoom.Register(virat);
            chatRoom.Register(karle);

            Console.WriteLine();

            nikhil.SendTo("karle", "HI K");
            karle.SendTo("nikhil", "HI N");
            virat.Broadcast("Team meeting at 3");

            Console.WriteLine();
            nikhil.SendTo("habibi", "Are you there?");
        }
    }
}