﻿using System;
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
        CLLocationManager locMan = new CLLocationManager();
        
		CLLocationCoordinate2D locSanFran = new CLLocationCoordinate2D(37.7833, -122.4167);
		CLLocationCoordinate2D locBoston = new CLLocationCoordinate2D(42.3601, -71.0589);
		CLLocationCoordinate2D locLondon = new CLLocationCoordinate2D(51.5072, -0.1275);
		CLLocationCoordinate2D locSingapore = new CLLocationCoordinate2D(1.3, 103.8);

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0) == true) {
				locMan.RequestWhenInUseAuthorization ();
			}

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
            // TODO:
        }

		partial void btnRemove_Activated (UIBarButtonItem sender)
		{
            // TODO:
		}
	}
}

