using Foundation;
using Mailbox;
using System;
using UIKit;

namespace MailBox
{
    public partial class DetailsViewController : UIViewController
    {

		public DetailsViewController (IntPtr handle) : base (handle)
        {
        }

        private EmailItem _item;
        public EmailItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                UpdateItem();
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateItem();
        }

        public void UpdateItem()
        {
            if (EmailText != null)
                EmailText.Text = (Item != null) ? Item.ToString() : String.Empty;
        }

        partial void OnDismiss(UIButton sender)
        {
            DismissViewController(true, null);
        }
    }
}