using CoreGraphics;
using Foundation;
using Mailbox;
using System;
using UIKit;

namespace MailBox
{
    //Class created by the designer, then implemented manually.
    public partial class TableViewController : UITableViewController
    {
        private readonly EmailServer _emailServer = new EmailServer();

        public TableViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _emailServer.Email.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell(CGRect.Empty);
            cell.TextLabel.Text = _emailServer.Email[indexPath.Row].Subject;
            return cell;
        }
    }
}