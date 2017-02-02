using System;
using UIKit;
using CoreLocation;
using System.Linq;

namespace BananaFinder
{
    public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

        private const string _mapAnnotReuseIdentifier = "mapPin";

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//position the map over downtown Vancouver
			map.Camera.CenterCoordinate = new CLLocationCoordinate2D (49.28275, -123.12);
			map.Camera.Altitude = 10000;
            //Use either this version or the GroceryMapDelegate.
            //map.GetViewForAnnotation = (mapView, mapAnnot) => new MKPinAnnotationView(mapAnnot, _mapAnnotReuseIdentifier) { PinColor = MKPinAnnotationColor.Purple };
            map.Delegate = new GroceryMapDelegate();
            this.AddStoreAnnotations();
		}

        private void AddStoreAnnotations()
        {
            map.AddAnnotations(StoreFactory.GetStores()
                                           .Select(s => new StoreAnnotation(s))
                                           .ToArray());
        }
	}
}

