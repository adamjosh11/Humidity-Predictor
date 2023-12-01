using System;
using Google.Cloud.Firestore;

namespace Server.Demo
{
	[FirestoreData]
	public class ClimateData : IFirebaseEntity
	{
		[FirestoreProperty]
		public string? ID { get; set; }
        [FirestoreProperty]
        public DateTime? DateTime { get; set; }
        [FirestoreProperty]
        public float Humidity { get; set; }
        [FirestoreProperty]
        public float Temperature { get; set; }
        

        public ClimateData(DateTime dateTime, float humidity, float temperature)
		{
			ID = Guid.NewGuid().ToString("N");
			DateTime = dateTime;
			Humidity = humidity;
			Temperature = temperature;
		}
		public ClimateData()
		{

		}
	}
}

