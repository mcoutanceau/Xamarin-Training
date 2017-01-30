using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace WeatherApp
{
	partial class WeatherTVC : UITableViewController
	{
		const string CELL_ID = "cell_id";
		List<Weather> _data;

		public WeatherTVC (IntPtr handle) : base (handle)
		{
			_data = WeatherFactory.GetWeatherData ();
           this.TableView.RegisterClassForCellReuse(typeof(WeatherCell), CELL_ID);
        }

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
            return this.GetWeatherCell(tableView, indexPath);
		}

        public WeatherCell GetWeatherCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (WeatherCell)tableView.DequeueReusableCell(CELL_ID);
            cell.UpdateData(_data[indexPath.Row]);
            return cell;
        }

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _data.Count;
		}
	}
}
