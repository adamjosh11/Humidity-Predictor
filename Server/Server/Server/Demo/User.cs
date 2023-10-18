using System;
using Google.Cloud.Firestore;

namespace Server.Demo
{
	[FirestoreData]
	public class User : IFirebaseEntity
	{
		[FirestoreProperty]
		public string Id { get; set; }
        [FirestoreProperty]
        public DateTime DateTime { get; set; }
        [FirestoreProperty]
        public float Humidity { get; set; }
        [FirestoreProperty]
        public float Temperature { get; set; }
        [FirestoreProperty]
        public Boolean Rained { get; set; }

        public User(DateTime dateTime, float humidity, float temperature, Boolean rained)
		{
			Id = Guid.NewGuid().ToString("N");
			DateTime = dateTime;
			Humidity = humidity;
			Temperature = temperature;
			Rained = rained;
		}
	}
}

