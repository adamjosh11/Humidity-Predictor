using System;
using Google.Cloud.Firestore;
using Server.Demo;

namespace Server.DataModels
{
    [FirestoreData]
    public class Prediction : IFirebaseEntity
    {
        [FirestoreProperty]
        public string? ID { get; set; }
        [FirestoreProperty]
        public DateTime? DateTime { get; set; }
        [FirestoreProperty]
        public string Pred { get; set; }
        

        public Prediction(DateTime dateTime, string prediction)
		{
            ID = Guid.NewGuid().ToString("N");
            DateTime = dateTime;
            Pred = prediction;
        }
        public Prediction()
        {

        }
	}
}

