using System;
using Foundation;
using UIKit;
using Mailbox;
using CoreGraphics;

namespace MailBox
{
    //Manually coded class
    public partial class ViewController : UIViewController
    {

        private const string _reuseIdentifier = "emailCell";
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
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Top,    NSLayoutRelation.Equal, this.View, NSLayoutAttribute.TopMargin, 1, 20));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Left,   NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Left     , 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Width,  NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width    , 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_uiTableView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Height   , 1, 0));

            _uiTableView.Source = new EmailTableViewSource(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        #region Nested types

        private class EmailTableViewSource : UITableViewSource
        {
            private readonly EmailServer _emailServer = new EmailServer(1000);
            private readonly ViewController _viewController;

            public EmailTableViewSource(ViewController viewController)
            {
                this._viewController = viewController;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var email = _emailServer.Emails[indexPath.Row];

                //Default style :
                //var cell = new UITableViewCell(UITableViewCellStyle.Default, null);
                //cell.TextLabel.Text  = email.Subject;
                //cell.ImageView.Image = email.GetImage();

                //Subtitle style:
                //var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
                //cell.TextLabel.Text            = email.Subject;
                //cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                //cell.ImageView.Image           = email.GetImage();
                //cell.DetailTextLabel.Text      = email.Body;
                //cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                //cell.DetailTextLabel.TextColor = UIColor.LightGray;

                //Value1 style
                //var cell = new UITableViewCell(UITableViewCellStyle.Value1, null);
                //cell.TextLabel.Text            = email.Subject.Substring(0, 20) + "...";
                //cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                //cell.ImageView.Image           = email.GetImage();
                //cell.DetailTextLabel.Text      = email.Body;
                //cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                //cell.DetailTextLabel.TextColor = UIColor.LightGray;

                //Value2 style
                //var cell = new UITableViewCell(UITableViewCellStyle.Value2, null);
                //cell.TextLabel.Text            = email.Subject.Substring(0, 20) + "...";
                //cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                ////No image assignement with Value2 Style
                //cell.DetailTextLabel.Text      = email.Body;
                //cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                //cell.DetailTextLabel.TextColor = UIColor.LightGray;

                //Exercice 4: DetailDisclosureButton
                UITableViewCell cell;

                if ((cell = this._viewController._uiTableView.DequeueReusableCell(_reuseIdentifier)) == null)
                    cell = GetEmailCell();
                else
                    cell.ImageView.Image?.Dispose();

                cell.TextLabel.Text       = email.Subject;
                cell.ImageView.Image      = email.GetImage();
                cell.DetailTextLabel.Text = email.Body;
                return cell;
            }

            private static UITableViewCell GetEmailCell()
            {
                var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, _reuseIdentifier);
                cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                cell.DetailTextLabel.TextColor = UIColor.LightGray;
                cell.Accessory                 = UITableViewCellAccessory.DetailDisclosureButton;
                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return _emailServer.Emails.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var detailViewController = (DetailsViewController)UIStoryboard.FromName("Main", null)
                                                                              .InstantiateViewController("DetailsViewController");
                detailViewController.Item = _emailServer.Emails[indexPath.Row];
                _viewController.ShowDetailViewController(detailViewController, _viewController);
            }

            public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
            {
                var email = _emailServer.Emails[indexPath.Row];
                var alertCtrl = UIAlertController.Create("Email Details", email.ToString(), UIAlertControllerStyle.Alert);
                alertCtrl.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                _viewController.PresentViewController(alertCtrl, true, null);
            }
        }

        #endregion Nested types
    }
}