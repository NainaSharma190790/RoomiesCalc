
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using RoomiesCalc;
using RoomiesCalc.iOS.Renderer;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RCEntry), typeof(RCEntryRenderer))]

namespace RoomiesCalc.iOS.Renderer
{
    public class RCEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.BorderColor = UIColor.White.CGColor;
                Control.Layer.BorderWidth = 0f;
                Control.ClipsToBounds = true;

            }
        }
    }
}

