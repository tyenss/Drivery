using System;
using System.Text.RegularExpressions;
using System.Xml;
using UIKit;
using MapThings;

namespace Drivery
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public ViewController(string name)
        {
            
        }

        public Maps getMap()
        {
            return null;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        
        private double[] AddresstoLatLong(string cityState)
        {
                string bingMapsUri = string.Format(@"http://dev.virtualearth.net/REST/v1/Locations/US/" + cityState + "?o=xml&amp;key=BingMapsKey");
                XmlDocument bingMapsXmlDoc = new XmlDocument();
                bingMapsXmlDoc.Load(bingMapsUri);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(bingMapsXmlDoc.NameTable);
                nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");
                string sLong = bingMapsXmlDoc.DocumentElement.SelectSingleNode(@".//rest:Longitude", nsmgr).InnerText;
                string sLat = bingMapsXmlDoc.DocumentElement.SelectSingleNode(@".//rest:Latitude", nsmgr).InnerText;
                 
                return new double[2]{Convert.ToDouble(sLat),Convert.ToDouble(sLong)};
        }
    }
}
