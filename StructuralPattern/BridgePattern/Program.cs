using BridgePattern.Abstractions;
using BridgePattern.Implementations;
using System;

namespace BridgePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INotification email = new EmailNotification();
            INotification sms = new SMSNotification();

            NotificationAbstraction normal = new NormalNotification(email);
            normal.Notify("Nikhil", "Email");
            NotificationAbstraction urgent = new UrgentNotification(sms);
            urgent.Notify("Nikhil", "SMS");


        }
    }
}