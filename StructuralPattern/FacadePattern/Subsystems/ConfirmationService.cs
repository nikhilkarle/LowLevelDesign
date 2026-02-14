using System;

namespace FacadePattern.Subsystems
{
    public class ConfirmationService
    {
        public string SendConfirmation(string guestName)
        {
            return "CONF-" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpperInvariant();
        }
    }
}
