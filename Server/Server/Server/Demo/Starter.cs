using System;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace Server.Demo
{
	public class Starter
	{
		private readonly FirestoreProvider _firestoreProvider;
		private readonly CancellationToken _token;

		public Starter()
		{
            var jsonString = File.ReadAllText("/ Users/joshadams/CS6010/CapStone/Rainfall - Predictor/Server/firebase-SDK.json");
            var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
            FirestoreDb db = FirestoreDb.Create("RainFallCapstone", builder.Build());
			_firestoreProvider = new FirestoreProvider(db);
			CancellationTokenSource source = new CancellationTokenSource();
			_token = source.Token;
        }

		public async void Add(DateTime dateTime, float humidity, float temperature, Boolean rained)
		{
			var user = new User(dateTime, humidity, temperature, rained);
			await _firestoreProvider.AddOrUpdate(user, _token);
		}

		public async Task<User> Get(string id)
		{
			return await _firestoreProvider.Get<User>(id, _token);
		}

        public async Task<IReadOnlyCollection<User>> GetAll()
        {
            return await _firestoreProvider.GetAll<User>(_token);
        }

        public async Task<IReadOnlyCollection<User>> GetByDateTime(DateTime dateTime)
        {
            return await _firestoreProvider.WhereEqualTo<User>(nameof(User.DateTime), dateTime, _token);
        }
    }
}

