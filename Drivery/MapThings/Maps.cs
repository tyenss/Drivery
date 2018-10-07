using System;
using UIKit;
using Xamarin.Forms.Maps;

namespace MapThings
{
    public static class Maps
    {
        
        public static void LocationToPin(MapData mapDat,Map map)
        {
            Pin pin = new Pin (){
            Position = new Position (37, -122),
            Label = "Some Pin!"
            };
            map.Pins.Add(pin);
        }
        
        public static void LocationToPinArray(MapData[] mapData,Map map)
        {
            for (int i=0;i<mapData.Length;i++)
            {
                Pin pin = new Pin (){
                Position = new Position (mapData[i].Latitude, mapData[i].Longitude),
                Label = mapData[i].RestaurantName
                };
                map.Pins.Add(pin);
            }
        }
        
        public static void AddressToLatLong()
        {
            
        }
    }
}
