using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();// Perform any additional setup after loading the view, typically from a nib.
            if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0))
				_locMan.RequestWhenInUseAuthorization ();
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

		partial void btnSanFran_Activated (UIBarButtonItem sender)
		{
            map.Camera.Altitude  = 1000;
            map.CenterCoordinate = _locSanFran;
            map.MapType = MKMapType.Standard;
            map.Camera.Pitch = 45;
            map.ShowsBuildings = true;
            map.PitchEnabled = true;
		}

		partial void btnBoston_Activated (UIBarButtonItem sender)
		{
            map.Camera.Altitude = 1000;
            map.CenterCoordinate = _locBoston;
            map.MapType = MKMapType.Standard;
            map.Camera.Pitch = 45;
            map.ShowsBuildings = true;
            map.PitchEnabled = true;
        }

		partial void btnLondon_Activated (UIBarButtonItem sender)
		{
            var oldCam = map.Camera;
            var newCam = new MKMapCamera();
            newCam.CenterCoordinate = _locLondon;
            newCam.Altitude = 10000;
            newCam.Pitch = 45;
            newCam.Heading = 180;
            map.Camera = newCam;
            oldCam.Dispose(); //Dispose nécessaire ?
        }

		partial void btnSingapore_Activated (UIBarButtonItem sender)
		{
            var newCam = new MKMapCamera();
            newCam.CenterCoordinate = _locSingapore;
            newCam.Altitude = 10000;
            newCam.Pitch = 45;
            newCam.Heading = 180;
            var oldCam = map.Camera;
            map.SetCamera(newCam, true);
            oldCam.Dispose(); //Dispose nécessaire ?
        }
	}
}

