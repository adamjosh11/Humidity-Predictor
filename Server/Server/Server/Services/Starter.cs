using System;
using System.Reflection;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Server.DataModels;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace Server.Demo
{
	public class Starter
	{
		private readonly FirestoreProvider _firestoreProvider;
		private readonly CancellationToken _token;

		public Starter()
		{
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Authentication.json");
            var jsonString = File.ReadAllText(path);
            var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
            FirestoreDb db = FirestoreDb.Create("rainfallcapstone", builder.Build());
        
            _firestoreProvider = new FirestoreProvider(db);
			CancellationTokenSource source = new CancellationTokenSource();
			_token = source.Token;
        }

		public async void Add(ClimateData user)
		{
            user.DateTime = DateTime.Now.ToUniversalTime();
			user.ID = Guid.NewGuid().ToString("N");
            await _firestoreProvider.AddOrUpdate(user, _token);
		}

        public async void Add(Prediction user)
        {
            user.DateTime = DateTime.Now.ToUniversalTime();
            user.ID = Guid.NewGuid().ToString("N");
            await _firestoreProvider.AddOrUpdate(user, _token);
        }

        public async Task<ClimateData> Get(string id)
		{
			return await _firestoreProvider.Get<ClimateData>(id, _token);
		}

        public async Task<IReadOnlyCollection<ClimateData>> GetAll()
        {
            return await _firestoreProvider.GetAll<ClimateData>(_token);
        }

        public async Task<ClimateData?> GetLatest()
        {
            return await _firestoreProvider.GetLatest<ClimateData>(_token);
        }

        public async Task<Prediction?> GetLatestPrediction()
        {
            return await _firestoreProvider.GetLatest<Prediction>(_token);
        }

        public async Task<IReadOnlyCollection<ClimateData>> GetByDateTime(DateTime dateTime)
        {
            return await _firestoreProvider.WhereEqualTo<ClimateData>(nameof(ClimateData.DateTime), dateTime, _token);
        }
    }
}

