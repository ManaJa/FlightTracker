using System;
namespace FlightTracker.Provider
{
    public interface IFlightTrackerProvider
    {
        System.Collections.Generic.IEnumerable<FlightTracker.Provider.Entity.FlightTrackEntity> GetFlightTracks();
    }
}
