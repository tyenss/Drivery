using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yelp.Api;
using Yelp.Api.Models;

namespace MapThings{
    public class YelpReader
    {
        // This is the main entry point of the application.
        async static MapData[] YelpParser(string RestaurantName,double lat, double longi) 
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            return Task.Run(async () => {
                await AsyncParser(RestaurantName,lat,longi);
            }).GetAwaiter().GetResult();;
        }

        static async /*Task*/ MapData[] AsyncParser(string RestaurantName,double lat, double longi) {
            Console.WriteLine("Hello World");

            var client = new Yelp.Api.Client("aH_2cWcPiZ4d-HQoo4B6iE_lsEM199j3cIH5RoKErtdKwPLc6ugHKKljAqfrx_hJXE-pXClCYrcK3VoRKFnUmgEqDAhEpCHj8GN9420tUO-4wK5gzJzCBDohs0cBWnYx");
            return await search(client,10,10);
        }
        //static async Task<IList<BusinessResponse>> search(Yelp.Api.Client client, double lat, double longi
        static async MapData[] search(Yelp.Api.Client client, double lat, double longi) {
            SearchResponse results = await client.SearchBusinessesAllAsync("Restaurants", lat, longi);

            IList<BusinessResponse> filteredList = results.Businesses.Where(b => b.Rating > 3.0).ToList();
            
            List<MapData> restaurantsList = new List<MapData>();
            
            foreach (var b in results.Businesses) {
                restaurantsList.add(new MapData(b.Latitude,b.Longitude,b.Name,b.Rating));
                /*Console.WriteLine("Name: " + b.Name);
                Console.WriteLine("Location: " + b.Location);
                Console.WriteLine("Distance: " + b.Distance);
                Console.WriteLine("Rating: " + b.Rating);*/
            }
//Return an array
            return restaurantsList.ToArray();
        }
    }

}

