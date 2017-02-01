using System;
using UIKit;
using Foundation;
using MapKit;

namespace MapsExercice
{
    [Register("CustomMapViewController")]
    public class CustomMapViewController : UIViewController
    {
        private CustomMapView _mapView;

        public CustomMapViewController() {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mapView = new CustomMapView();
            this.View.AddSubview(_mapView);
            _mapView._barBtStandard.Clicked  += _barBtStandard_Clicked;
            _mapView._barBtSatellite.Clicked += _barBtSatellite_Clicked;
            _mapView._barBtHybrid.Clicked    += _barBtHybrid_Clicked;

            //Les contraintes sur les SubViews doivent être définies par les parents.
            this._mapView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.View.AddConstraint(NSLayoutConstraint.Create(_mapView, NSLayoutAttribute.Top,    NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Top,    1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_mapView, NSLayoutAttribute.Left,   NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Left,   1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_mapView, NSLayoutAttribute.Right,  NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Right,  1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_mapView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Bottom, 1, 0));
        }

        public override void ViewWillUnload()
        {
            base.ViewWillUnload();
            _mapView._barBtStandard.Clicked  -= _barBtStandard_Clicked;
            _mapView._barBtSatellite.Clicked -= _barBtSatellite_Clicked;
            _mapView._barBtHybrid.Clicked    -= _barBtHybrid_Clicked;
        }

        private void _barBtHybrid_Clicked(object sender, EventArgs e)
        {
            _mapView._mkMapView.MapType = MKMapType.Hybrid;
        }
        private void _barBtSatellite_Clicked(object sender, EventArgs e)
        {
            _mapView._mkMapView.MapType = MKMapType.Satellite;
        }
        private void _barBtStandard_Clicked(object sender, EventArgs e)
        {
            _mapView._mkMapView.MapType = MKMapType.Standard;
        }
    }
}