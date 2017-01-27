using System;

using UIKit;

namespace MailBox
{
    public partial class ViewController : UIViewController
    {
        private UITableView _uiTableView;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _uiTableView = new UITableView(View.Frame);
            this.View.AddSubview(_uiTableView);

            _uiTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Top,    NSLayoutRelation.Equal, this.View, NSLayoutAttribute.TopMargin, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Left,   NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Left     , 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Width,  NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width    , 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Height   , 1, 0));
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}