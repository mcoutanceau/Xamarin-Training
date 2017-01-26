using CoreGraphics;
using UIKit;

namespace TipCalculator
{
    public class MyViewController : UIViewController
    {
        public MyViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.Yellow;

            var txtfTotalAmountFrame = new CGRect(x: 20, y: 28, width: this.View.Bounds.Width - 20 - 20, height: 35);
            var txtfTotalAmount = new UITextField(txtfTotalAmountFrame)
            {
                KeyboardType = UIKeyboardType.DecimalPad,
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Enter Total Amount."
            };

            var btCalculate = new UIButton(UIButtonType.Custom)
            {
                Frame = new CGRect(x:       20
                                 , y:       txtfTotalAmountFrame.Bottom + 8
                                 , width:   this.View.Bounds.Width - 20 - 20
                                 , height:  45),
                BackgroundColor = UIColor.FromRGB(0,0.5f, 0)
            };
            btCalculate.SetTitle("Calculate", UIControlState.Normal);

            var resultLabel = new UILabel(new CGRect(
                x:      20,
                y:      btCalculate.Frame.Bottom + 8,
                width:  this.View.Bounds.Width - 20 - 20,
                height: 45)) {
                TextColor = UIColor.Blue,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = "Tip is $0.00"
            };

            this.View.AddSubviews(txtfTotalAmount, btCalculate, resultLabel);
        }
    }
}

