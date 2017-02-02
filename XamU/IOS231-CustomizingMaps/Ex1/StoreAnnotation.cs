using System;
using CoreLocation;

namespace BananaFinder
{
    internal class StoreAnnotation : MapKit.MKPointAnnotation
    {
        private readonly GroceryStore _store;

        public double TimeOpen { get { return _store.TimeOpen; } }
        public double TimeClosed { get { return _store.TimeClosed; } }
        public override string Title { get { return _store.Name; } }
        public override string Subtitle { get { return _store.Description; } }
        public override CLLocationCoordinate2D Coordinate { get { return new CLLocationCoordinate2D(_store.Latitude, _store.Longitude); } }

        public StoreAnnotation(GroceryStore store)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));
            _store = store;
        }
    }
}
