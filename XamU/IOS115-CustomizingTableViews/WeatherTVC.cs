using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace WeatherApp
{
	partial class WeatherTVC : UITableViewController
	{
		const string CELL_ID = "id";
		private readonly List<Weather> data;

		public WeatherTVC (IntPtr handle) : base (handle)
		{
			data = WeatherFactory.GetWeatherData();
			TableView.ContentInset = new UIEdgeInsets(this.TopLayoutGuide.Length, 0, 0, 0);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CELL_ID);

            if (cell == null)
            { 
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CELL_ID);
                cell.TextLabel.TextColor = UIColor.FromRGB(59, 102, 136);
                cell.DetailTextLabel.TextColor = UIColor.FromRGB(0, 142, 255);
            }
            else
                cell.ImageView.Image?.Dispose();

            var weatherData = data[indexPath.Row];
            cell.TextLabel.Text = weatherData.City;
            cell.DetailTextLabel.Text = weatherData.ToString();
            cell.ImageView.Image = UIImage.FromBundle(weatherData.CurrentConditions + ".png");
			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return data.Count;
		}
	}
}
