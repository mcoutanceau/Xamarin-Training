using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace WeatherApp
{
    internal class WeatherCell : UITableViewCell
    {
        private readonly UILabel     _lblCity    = new UILabel();
        private readonly UIImageView _imgWeather = new UIImageView();
        private readonly UILabel     _lblHigh    = new UILabel();
        private readonly UILabel     _lblLow     = new UILabel();

        public WeatherCell(IntPtr handle) : base(handle)
        {
            this.ContentView.AddSubviews(_lblCity, _imgWeather, _lblHigh, _lblLow);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var width = ContentView.Frame.Width;
            _imgWeather.Frame  = new CGRect(0, 0, 30, 30);
            _imgWeather.Center = this.ContentView.Center;

            _lblCity.Frame = new CGRect(20, 7, 100, 30);
            _lblLow.Frame  = new CGRect(width - 50, 7, 30, 30);
            _lblHigh.Frame = new CGRect(_lblLow.Frame.Left - 30, 7, 30, 30);
        }

        public void UpdateData(Weather pWeather)
        {
            if (pWeather == null)
                throw new ArgumentNullException(nameof(pWeather));
            this._lblCity.Text      = pWeather.City;

            this._imgWeather.Image?.Dispose();
            this._imgWeather.Image  = UIImage.FromBundle(pWeather.CurrentConditions.ToString() + ".png");
            this._lblHigh.Text      = pWeather.High.ToString();
            this._lblLow.Text       = pWeather.Low.ToString();
        }
    }
}
