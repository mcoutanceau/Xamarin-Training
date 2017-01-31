using System;
using System.Drawing;
using UIKit;
using Foundation;
using MapKit;
using CoreGraphics;

namespace MapsExercice
{
    [Register("UniversalView")]
    public class CustomMapView : UIView
    {
        internal MKMapView _mkMapView;
        internal UIToolbar _uiToolBar;
        internal UIBarButtonItem _barBtStandard;
        internal UIBarButtonItem _barBtSatellite;
        internal UIBarButtonItem _barBtHybrid;

        public CustomMapView() { Initialize(); }
        public CustomMapView(RectangleF bounds) : base(bounds) { Initialize(); }
        public CustomMapView(CGRect bounds) : base(bounds) { Initialize(); }

        private void Initialize()
        {
            nfloat toolbarHeigth = 50;
            
            this.BackgroundColor = UIColor.Red;
            _mkMapView = new MKMapView(this.Frame);
            _mkMapView.ZoomEnabled = true;
            _mkMapView.ScrollEnabled = true;
            _mkMapView.RotateEnabled = true;
            _mkMapView.ShowsBuildings = true;
            _mkMapView.ShowsPointsOfInterest = true;
            _mkMapView.PitchEnabled = true;

            _uiToolBar = new UIToolbar(new CGRect(0, this.Frame.Bottom - toolbarHeigth, this.Frame.Width, toolbarHeigth));
            _uiToolBar.BackgroundColor = UIColor.LightGray;
            this.AddSubviews(_mkMapView, _uiToolBar);

            //TODO: Les contraintes ne fonctionnent pas encore, voir pourquoi...
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this, NSLayoutAttribute.Right, 1, 0));
            this.AddConstraint(NSLayoutConstraint.Create(_mkMapView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0));

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

    [Register("MyViewController")]
    public class MyViewController : UIViewController
    {
        public MyViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CustomMapView view;
            this.View = view = new CustomMapView(this.View.Frame);
            view._barBtStandard.Clicked  += _barBtStandard_Clicked;
            view._barBtSatellite.Clicked += _barBtSatellite_Clicked;
            view._barBtHybrid.Clicked    += _barBtHybrid_Clicked;
        }

        public override void ViewWillUnload()
        {
            base.ViewWillUnload();
            CustomMapView view = (CustomMapView)this.View;
            view._barBtStandard.Clicked  -= _barBtStandard_Clicked;
            view._barBtSatellite.Clicked -= _barBtSatellite_Clicked;
            view._barBtHybrid.Clicked    -= _barBtHybrid_Clicked;
        }

        private void _barBtHybrid_Clicked(object sender, EventArgs e)
        {
            CustomMapView view = (CustomMapView)this.View;
            view._mkMapView.MapType = MKMapType.Hybrid;
        }

        private void _barBtSatellite_Clicked(object sender, EventArgs e)
        {
            CustomMapView view = (CustomMapView)this.View;
            view._mkMapView.MapType = MKMapType.Satellite;
        }

        private void _barBtStandard_Clicked(object sender, EventArgs e)
        {
            CustomMapView view = (CustomMapView)this.View;
            view._mkMapView.MapType = MKMapType.Standard;
        }
    }
}