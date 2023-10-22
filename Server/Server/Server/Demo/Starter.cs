using System;
using System.Reflection;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace Server.Demo
{
	public class Starter
	{
		private readonly FirestoreProvider _firestoreProvider;
		private readonly CancellationToken _token;

		public Starter()
		{
			//var dummy = Directory.GetCurrentDirectory();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Authentication.json");
            var jsonString = File.ReadAllText(path);
            var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
            FirestoreDb db = FirestoreDb.Create("rainfallcapstone", builder.Build());
            //var db = new FirestoreDbBuilder
            //{
            //    ProjectId = "RainFallCapstoneeeeeee",
            //    JsonCredentials = jsonString
            //}.Build();
            _firestoreProvider = new FirestoreProvider(db);
			CancellationTokenSource source = new CancellationTokenSource();
			_token = source.Token;
        }

		public async void Add(User user)
		{
			user.ID = Guid.NewGuid().ToString("N");
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

