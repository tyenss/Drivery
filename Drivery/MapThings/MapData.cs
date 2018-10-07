using System;

namespace MapThings
{
    public class MapData
    {
        public double Latitude {get;set;}
        public double Longitude {get;set;}
        public string RestaurantType{get;set;}
        public string RestaurantName{get;set;}
		public double Review{get;set;}
        
        public MapData(double latitude,double longitude,string restaurantName,double review)
        {
            Latitude=latitude;
            Longitude=longitude;
            RestaurantName=restaurantName;
			Review=review;
        }
    }
}
