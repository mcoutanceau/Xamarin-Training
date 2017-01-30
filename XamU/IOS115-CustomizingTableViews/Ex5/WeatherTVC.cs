using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Linq;
using System.Collections.Generic;

namespace WeatherApp
{
	partial class WeatherTVC : UITableViewController
	{
		private const string CELL_ID = "cell_id";
		private readonly IGrouping<string, Weather>[] _data;
        private readonly string[] _indices;

        public WeatherTVC (IntPtr handle) : base (handle)
		{
            this._data = WeatherFactory.GetWeatherData()
                                       .OrderBy(w => w.City, StringComparer.InvariantCultureIgnoreCase)
                                       .GroupBy(w => w.City.Substring(0, 1), StringComparer.InvariantCultureIgnoreCase)
                                       .ToArray();
            this._indices = this._data.Select(g => g.Key.ToLowerInvariant()).ToArray();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.ContentInset = new UIEdgeInsets(20, 0, 0, 0);
		}

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return _data[section].Key;
        }

        public override string TitleForFooter(UITableView tableView, nint section)
        {
            return String.Format("{0} Cities", _data[section].Count());
        }

        public override string[] SectionIndexTitles(UITableView tableView)
        {
            return _indices;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (WeatherTableCell)tableView.DequeueReusableCell(CELL_ID);
            var weather = _data[indexPath.Section].ElementAt(indexPath.Row);
            cell.UpdateData(weather);
			return cell;
		}

        public override nint NumberOfSections(UITableView tableView)
        {
            return _data.Length;
        }

        public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _data[section].Count();
		}
	}
}
