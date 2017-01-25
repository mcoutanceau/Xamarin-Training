using System;

using UIKit;
using Foundation;
using System.Collections.Generic;

namespace Phoneword_iOS
{
	public partial class ViewController : UIViewController
	{        

		// translatedNumber was moved here from ViewDidLoad ()
		string translatedNumber = String.Empty;

		public List<string> PhoneNumbers { get; set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			//initialize list of phone numbers called for Call History screen
			PhoneNumbers = new List<string>();

		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
				// Convert the phone number with text to a number 
				// using PhoneTranslator.cs
				translatedNumber = PhoneTranslator.ToNumber (
					PhoneNumberText.Text);

				// Dismiss the keyboard if text field was tapped
				PhoneNumberText.ResignFirstResponder ();

				if (translatedNumber == "") {
					CallButton.SetTitle ("Call ", UIControlState.Normal);
					CallButton.Enabled = false;
				} else {
					CallButton.SetTitle ("Call " + translatedNumber,
						UIControlState.Normal);
					CallButton.Enabled = true;
				}
			};

			CallButton.TouchUpInside += (object sender, EventArgs e) => {
				PhoneNumbers.Add(translatedNumber);
				var url = new NSUrl ("tel:" + translatedNumber);
				// Use URL handler with tel: prefix to invoke Apple's Phone app, 
				// otherwise show an alert dialog                
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var alert = UIAlertController.Create ("Not supported", "Scheme 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
					alert.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
					PresentViewController (alert, true, null);
				}
			};

			CallHistoryButton.TouchUpInside += (object sender, EventArgs e) => {
				// Launches a new instance of CallHistoryController
				CallHistoryController callHistory = this.Storyboard.InstantiateViewController("CallHistoryController") as CallHistoryController;
				if (callHistory != null)
				{
					callHistory.PhoneNumbers = PhoneNumbers;
					this.NavigationController.PushViewController(callHistory, true);
				}
			};
		}

		//public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		//{
		//	base.PrepareForSegue(segue, sender);
		//	var destViewCtrl = segue.DestinationViewController;
  // 			var callHistoryContoller = destViewCtrl as CallHistoryController;

		//	//set the Table View Controller’s list of phone numbers to the
		//	// list of dialed phone numbers

		//	if (callHistoryContoller != null)
		//	{
		//		callHistoryContoller.PhoneNumbers = PhoneNumbers;
		//	}
		//}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

