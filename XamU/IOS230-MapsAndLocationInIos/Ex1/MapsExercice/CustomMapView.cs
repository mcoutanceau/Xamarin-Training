using Foundation;
using MapKit;
using System.Drawing;
using UIKit;
using CoreGraphics;
using CoreLocation;

namespace MapsExercice
{
    [Register("CustomMapView")]
    public class CustomMapView : UIView
    {
        internal MKMapView _mkMapView;
        internal UIToolbar _uiToolBar;
        internal UIBarButtonItem _barBtStandard;
        internal UIBarButtonItem _barBtSatellite;
        internal UIBarButtonItem _barBtHybrid;
        internal CLLocationManager _locationMan;

        public CustomMapView() { Initialize(); }
        public CustomMapView(RectangleF bounds) : base(bounds) { Initialize(); }
        public CustomMapView(CGRect bounds) : base(bounds) { Initialize(); }

        private void Initialize()
        {
            _mkMapView = new MKMapView(this.Frame);
            _mkMapView.ZoomEnabled           = true;
            _mkMapView.ScrollEnabled         = true;
            _mkMapView.RotateEnabled         = true;
            _mkMapView.ShowsBuildings        = true;
            _mkMapView.ShowsPointsOfInterest = true;
            _mkMapView.PitchEnabled          = true;
            if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
                _mkMapView.ShowsUserLocation = true;

            _uiToolBar = new UIToolbar();
            _uiToolBar.BackgroundColor = UIColor.LightGray;

            this.AddSubviews(_mkMapView, _uiToolBar);

            _mkMapView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Top,    NSLayoutRelation.Equal, this, NSLayoutAttribute.Top,    1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Left,   NSLayoutRelation.Equal, this, NSLayoutAttribute.Left,   1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Right,  NSLayoutRelation.Equal, this, NSLayoutAttribute.Right,  1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0));

            _uiToolBar.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddConstraint(NSLayoutConstraint.Create(_uiToolBar, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 0f, 50));
            this.AddConstraint(NSLayoutConstraint.Create(_uiToolBar, NSLayoutAttribute.Left,   NSLayoutRelation.Equal, this, NSLayoutAttribute.Left,   1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_uiToolBar, NSLayoutAttribute.Right,  NSLayoutRelation.Equal, this, NSLayoutAttribute.Right,  1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_uiToolBar, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0));

            _barBtStandard = new UIBarButtonItem()
            {
                Style = UIBarButtonItemStyle.Bordered,
                Title = "Standard",
            };
            _barBtSatellite = new UIBarButtonItem()
            {
                Style = UIBarButtonItemStyle.Bordered,
                Title = "Satellite",
            };
            _barBtHybrid = new UIBarButtonItem()
            {
                Style = UIBarButtonItemStyle.Bordered,
                Title = "Hybrid",
            };
            _uiToolBar.SetItems(new UIBarButtonItem[] { _barBtStandard, _barBtSatellite, _barBtHybrid }, false);
        }
    }
}
