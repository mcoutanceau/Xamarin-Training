using System;
using UIKit;
using CoreLocation;
using Foundation;
using MapKit;
using System.Collections.Generic;
using System.Linq;

namespace Mapping
{
	public partial class ViewController : UIViewController
	{
        private readonly CLLocationManager      _locMan       = new CLLocationManager();
		private readonly CLLocationCoordinate2D _locSanFran   = new CLLocationCoordinate2D(37.7833, -122.4167);
		private readonly CLLocationCoordinate2D _locBoston    = new CLLocationCoordinate2D(42.3601, -71.0589);
		private readonly CLLocationCoordinate2D _locLondon    = new CLLocationCoordinate2D(51.5072, -0.1275);
		private readonly CLLocationCoordinate2D _locSingapore = new CLLocationCoordinate2D(1.3, 103.8);

		public ViewController (IntPtr handle) : base (handle)
		{
		}

        public void AddAnnotation(CLLocationCoordinate2D pCoord, string pTitle, string pSubtitle)
        {
            map.AddAnnotation(new MKPointAnnotation()
            {
                Coordinate = pCoord,
                Title = pTitle,
                Subtitle = pSubtitle
            });
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0))
				_locMan.RequestWhenInUseAuthorization ();
			map.MapType = MKMapType.Hybrid;
			map.ShowsUserLocation = true;
		}

		public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0) == true)
                {
                    var yesNoAlertController = UIAlertController.Create(
                        "Unable to determine location", 
                        "This application works best when it can determine your current position.  " +
                        "Would you like to go to Settings to enable location data?", 
                        UIAlertControllerStyle.Alert);

                    yesNoAlertController.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default,
                        alert => {
                            var settingsString = UIApplication.OpenSettingsUrlString;
                            var url = new NSUrl(settingsString);
                            UIApplication.SharedApplication.OpenUrl(url);
                        }));

                    yesNoAlertController.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Cancel, null));
                    this.PresentViewController(yesNoAlertController, true, null);
                }
                else
                {
                    var alert = new UIAlertView ("Unabled to determine location",
                        "This application works best when it can determine your current position.  " +
                        "Please open the Settings and enable Location Services for this app.", 
                        null,"OK");
                    alert.Show ();
                }
            }
 	    }

        partial void btnPoints_Activated (UIBarButtonItem sender)
        {
            this.AddAnnotation(_locSanFran, "If you are going to...", "don't forget to put some flowers in your hair.");
            this.AddAnnotation(_locBoston, "Boston", "Business and leisure at the same time.");
            this.AddAnnotation(_locLondon, "London...", "...calling");
            this.AddAnnotation(_locSingapore, "Singapore", "Welcome to the Lion City");
            btnAddPoints.Enabled = false;
            btnRemovePoints.Enabled = true;
        }

		partial void btnRemove_Activated (UIBarButtonItem sender)
		{
            IMKAnnotation[] annotations;
            this.map.RemoveAnnotations(annotations = this.map.Annotations);
            foreach (var annotation in annotations)
                annotation.Dispose();//Dispose necessaire ?
            btnAddPoints.Enabled    = true;
            btnRemovePoints.Enabled = false;
        }
	}
}

