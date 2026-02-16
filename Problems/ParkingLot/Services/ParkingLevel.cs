using ParkingLot.Domain;

namespace ParkingLot.Services;

public sealed class ParkingLevel
{
    private readonly ReaderWriterLockSlim _lock = new();
    private readonly List<ParkingSpot> _spots;

    public int LevelNumber{get;}
    public IReadOnlyList<ParkingSpot> Spots => _spots;

    public ParkingLevel(int levelNumber, IEnumerable<ParkingSpot> spots)
    {
        LevelNumber = levelNumber;
        _spots = spots.ToList();
    }

    public bool TryPark(ParkingSpot spot, Vehicle vehicle)
    {
        _lock.EnterWriteLock();
        try
        {
            if (!spot.IsFree || !spot.CanFit(vehicle)) return false;
            spot.Park(vehicle);
            return true;
        }
        finally {_lock.ExitWriteLock();}
    }

    public bool TryUnpark(string spotId)
    {
        _lock.EnterWriteLock();
        try
        {
            var spot = _spots.FirstOrDefault(s => s.Id == spotId);
            if (spot is null || spot.IsFree) return false;
            spot.Unpark();
            return true;
        }
        finally { _lock.ExitWriteLock(); }
    }
}