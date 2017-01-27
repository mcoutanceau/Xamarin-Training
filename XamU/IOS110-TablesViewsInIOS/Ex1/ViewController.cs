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

            _uiTableView.Source = new EmailTableViewSource();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        #region Nested types

        private class EmailTableViewSource : UITableViewSource
        {
            private readonly EmailServer _emailServer = new EmailServer(50);

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                //Default style :
                //var cell = new UITableViewCell(UITableViewCellStyle.Default, null);
                //cell.TextLabel.Text  = _emailServer.Email[indexPath.Row].Subject;
                //cell.ImageView.Image = _emailServer.Email[indexPath.Row].GetImage();

                //Subtitle style:
                var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
                var email = _emailServer.Email[indexPath.Row];
                cell.TextLabel.Text            = email.Subject;
                cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                cell.ImageView.Image           = email.GetImage();
                cell.DetailTextLabel.Text      = email.Body;
                cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                cell.DetailTextLabel.TextColor = UIColor.LightGray;

                //Value1 style
                //var cell = new UITableViewCell(UITableViewCellStyle.Value1, null);
                //var email = _emailServer.Email[indexPath.Row];
                //cell.TextLabel.Text            = email.Subject.Substring(0, 20) + "...";
                //cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                //cell.ImageView.Image           = email.GetImage();
                //cell.DetailTextLabel.Text      = email.Body;
                //cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                //cell.DetailTextLabel.TextColor = UIColor.LightGray;

                //Value2 style
                //var cell = new UITableViewCell(UITableViewCellStyle.Value2, null);
                //var email = _emailServer.Email[indexPath.Row];
                //cell.TextLabel.Text            = email.Subject.Substring(0, 20) + "...";
                //cell.TextLabel.Font            = UIFont.FromName("Helvetica Light", 14);
                ////No image assignement with Value2 Style
                //cell.DetailTextLabel.Text      = email.Body;
                //cell.DetailTextLabel.Font      = UIFont.FromName("Helvetica Light", 12);
                //cell.DetailTextLabel.TextColor = UIColor.LightGray;

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return _emailServer.Email.Count;
            }
        }

        #endregion Nested types
    }
}