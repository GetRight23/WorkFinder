namespace Tests
{
	internal class PostgreStorage
	{
		private object testConnection;

		public PostgreStorage(object testConnection)
		{
			this.testConnection = testConnection;
		}
	}
}