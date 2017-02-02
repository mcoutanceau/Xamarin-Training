using MapKit;
using UIKit;

namespace BananaFinder
{
    internal class GroceryMapDelegate : MKMapViewDelegate
    {
        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation pAnnotation)
        {
            StoreAnnotation annotation = (StoreAnnotation)pAnnotation;
            return new MKPinAnnotationView(annotation, reuseIdentifier: "pin")
            {
                PinTintColor = (annotation.TimeOpen < 9) ? UIColor.Purple : UIColor.Gray
            };
        }
    }
}
