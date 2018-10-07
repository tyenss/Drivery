using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yelp.Api;
using Yelp.Api.Models;
using System.Xml;
using System.Text.RegularExpressions;

namespace MapThings{
    public class YelpReader
    {
        // This is the main entry point of the application.
        async static Task<MapData[]> YelpParser(string RestaurantName,double lat, double longi) 
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            return Task.Run(async () => {
                return await AsyncParser(RestaurantName,lat,longi);
            }).GetAwaiter().GetResult();
        }

        static async Task<MapData[]> AsyncParser(string RestaurantName,double lat, double longi) {
            Console.WriteLine("Hello World");

            var client = new Yelp.Api.Client("aH_2cWcPiZ4d-HQoo4B6iE_lsEM199j3cIH5RoKErtdKwPLc6ugHKKljAqfrx_hJXE-pXClCYrcK3VoRKFnUmgEqDAhEpCHj8GN9420tUO-4wK5gzJzCBDohs0cBWnYx");
            return await search(client,10,10);
        }
        //static async Task<IList<BusinessResponse>> search(Yelp.Api.Client client, double lat, double longi
        static async Task<MapData[]> search(Yelp.Api.Client client, double lat, double longi) {
            SearchResponse results = await client.SearchBusinessesAllAsync("Restaurants", lat, longi);

            IList<BusinessResponse> filteredList = results.Businesses.Where(b => b.Rating > 3.0).ToList();
            
            List<MapData> restaurantsList = new List<MapData>();
            
            foreach (var b in results.Businesses) {
                double[] thing=AddresstoLatLong(b.Location.Address1,b.Location.City,b.Location.State);
                restaurantsList.Add(new MapData(thing[0],thing[1],b.Name,b.Rating));
                /*Console.WriteLine("Name: " + b.Name);
                Console.WriteLine("Location: " + b.Location);
                Console.WriteLine("Distance: " + b.Distance);
                Console.WriteLine("Rating: " + b.Rating);*/
            }
//Return an array
            return restaurantsList.ToArray();
        }
        
        public double[] AddresstoLatLong(string street, string city, string state)
        {
            string bingMapsUri = string.Format(@"http://dev.virtualearth.net/REST/v1/Locations/US/" + Regex.Replace
            (street,"#","") + ", " + city + ", " + state + "?o=xml&amp;key=BingMapsKey");
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

