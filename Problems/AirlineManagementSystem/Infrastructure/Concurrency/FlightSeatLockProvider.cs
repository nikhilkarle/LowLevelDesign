using System;
using System.Collections.Generic;

namespace AirlineManagementSystem.Infrastructure.Concurrency;

public class FlightSeatLockProvider
{
    private readonly Dictionary<Guid, object> _flightLocks = new();
    private readonly object _dictionaryLock = new();

    public object GetLock(Guid flightId)
    {
        lock (_dictionaryLock)
        {
            if (!_flightLocks.ContainsKey(flightId))
            {
                _flightLocks[flightId] = new object();
            }

            return _flightLocks[flightId];
        }
    }
}