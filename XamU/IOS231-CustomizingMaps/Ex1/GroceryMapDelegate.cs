using MapKit;
using System;
using UIKit;

namespace BananaFinder
{
    internal class GroceryMapDelegate : MKMapViewDelegate
    {
        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation pAnnotation)
        {
            return GetViewForEx2(mapView, pAnnotation);
        }

        /// <summary>
        /// Shows pins, color is changing depending on the TimeOpen property.
        /// </summary>
        private static MKAnnotationView GetViewForEx1(MKMapView mapView, IMKAnnotation pAnnotation)
        {
            StoreAnnotation annotation = (StoreAnnotation)pAnnotation;
            return new MKPinAnnotationView(annotation, reuseIdentifier: "pin")
            {
                PinTintColor = (annotation.TimeOpen < 9) ? UIColor.Purple : UIColor.Gray//Having a dynamic color may be note suitable with a reuseIdentifier.
            };
        }

        /// <summary>
        /// Shows annotation with a banana picture.
        /// </summary>
        private static MKAnnotationView GetViewForEx2(MKMapView mapView, IMKAnnotation pAnnotation)
        {
            const string reuseIdentifier = "pin";
            var annotView = mapView.DequeueReusableAnnotation(reuseIdentifier);
            if (annotView != null)
                annotView.Annotation = pAnnotation;
            else
                annotView = new MKAnnotationView(pAnnotation, reuseIdentifier)
                {
                    Image = UIImage.FromBundle("banana_pin.png"),
                    CenterOffset = new CoreGraphics.CGPoint(0, -20),
                    CanShowCallout = true,
                    RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure)
                };
            return annotView;
        }

        public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
        {
            StoreAnnotation annot =(StoreAnnotation)view.Annotation;
            using (var alert = new UIAlertView(annot.Title
                                             , String.Format("Hours\r\n {0} till {1}", annot.TimeOpen, annot.TimeClosed), null, "OK", null))
                alert.Show();
        }
    }
}
