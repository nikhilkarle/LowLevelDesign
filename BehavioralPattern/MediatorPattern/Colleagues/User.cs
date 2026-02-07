using MediatorPattern.Mediators;
using System;

namespace MediatorPattern.Colleagues
{
    public class User
    {
        private readonly IChatRoomMediator _chatRoom;
        public string Name {get; }

        public User(string name, IChatRoomMediator chatRoom)
        {
            Name = name;
            _chatRoom = chatRoom;
        }

        public void SendTo(string toUser, string message)
        {
            _chatRoom.Send(Name, toUser, message);
        }

         public void Broadcast(string message)
        {
            _chatRoom.Broadcast(Name, message);
        }

        public void Receive(string fromUser, string message)
        {
            Console.WriteLine("[" + Name + "]" + "received message " + message + " from " + fromUser);
        }

        public void ReceiveSystem(string message)
        {
            Console.WriteLine("[" + Name + "]" + "SYSTEM " + message);
        }
    }
}