using MediatorPattern.Colleagues;
using System.Collections.Generic;
using System;

namespace MediatorPattern.Mediators
{
    public class ChatRoomMediator : IChatRoomMediator
    {
        private readonly Dictionary<string, User> _users;

        public ChatRoomMediator()
        {
            _users = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);
        }

        public void Register(User user)
        {
            if (_users.ContainsKey(user.Name))
            {
                return;
            }
            _users.Add(user.Name, user);
            foreach(KeyValuePair<string, User> entry in _users)
            {
                entry.Value.ReceiveSystem(user.Name + " has joined the chat.");
            }
        }

        public void Send(string fromUser, string toUser, string message)
        {
            User recipient;
            bool found = _users.TryGetValue(toUser, out recipient);

            if (!found)
            {
                User sender;
                bool senderFound = _users.TryGetValue(fromUser, out sender);

                if (senderFound)
                {
                    sender.ReceiveSystem("User " + toUser + " not found");
                }
                return;
            }

            recipient.Receive(fromUser, message);
        }

        public void Broadcast(string fromUser, string message)
        {
            foreach(KeyValuePair<string, User> entry in _users)
            {
                User user = entry.Value;

                if (!string.Equals(user.Name, fromUser, StringComparison.OrdinalIgnoreCase))
                {
                    user.Receive(fromUser, message);
                }
            }
        }
    }
}