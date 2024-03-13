using System.Data.SqlClient;

namespace Task2
{
	internal class Program
	{
		internal class StorageValue
		{
			public string Name;
			public string Type;
			public string Provider;
			public int Quantity;
			public double PrimeCost;
			public DateOnly OrderDate;
		}

		static void Main(string[] args) {
			string connectionStr = @"Data Source=CRYCOMBATPC\MSSQL2019;Integrated Security=True;Connect Timeout=30;";
			using SqlConnection connection = new SqlConnection(connectionStr);

			Console.WriteLine("Connecting...");
			try {
				connection.Open();
			}
			catch (Exception x) {
				Console.WriteLine("Cant connect");
				return;
			}
			Console.WriteLine("Connected to server");

			try {
				using SqlCommand testCommand = new SqlCommand("USE Storehouse", connection);
				testCommand.ExecuteNonQuery();
			}
			catch {
				Console.WriteLine("Cant connect to Storehouse database");
				return;
			}

			Console.WriteLine("Connected to Storehouse database");

            Console.Write(@"1. Update value with specific ID
2. Update values providers
3. Update values types
> ");
            switch (Console.ReadKey(true).KeyChar) {
				case '1':
					Console.Write("\nId to update: ");
					int id = int.Parse(Console.ReadLine());

					StorageValue value = new StorageValue();
					Console.Write("New Name: ");
					value.Name = Console.ReadLine() ?? "Default";
					Console.Write("New Type: ");
					value.Type = Console.ReadLine() ?? "Default";
					Console.Write("New Provider: ");
					value.Provider = Console.ReadLine() ?? "Default";
					Console.Write("New Quantity: ");
					value.Quantity = int.Parse(Console.ReadLine() ?? "1");
					Console.Write("New PrimeCost: ");
					value.PrimeCost = double.Parse(Console.ReadLine() ?? "1");
					Console.Write("New OrderDate: ");
					value.OrderDate = DateOnly.Parse(Console.ReadLine() ?? "");
					UpdateAllValuesWhere(connection, value, id);
					break;
				case '2':
                    Console.Write("\nUpdate providers from: ");
					string? oldProvider = Console.ReadLine();
                    Console.Write("To: ");
					string? newProvider = Console.ReadLine();
					if (oldProvider is null || newProvider is null) {
                        Console.WriteLine("cant update with nulls");
                        return;
					}
					UpdateOneValueWhere(connection, "Provider", oldProvider, newProvider, true);
					break;
				case '3':
					Console.Write("\nUpdate types from: ");
					string? oldType = Console.ReadLine();
					Console.Write("To: ");
					string? newType = Console.ReadLine();
					if (oldType is null || newType is null) {
						Console.WriteLine("cant update with nulls");
						return;
					}
					UpdateOneValueWhere(connection, "Type", oldType, newType, true);
					break;
			}
		}

		private static void UpdateAllValuesWhere(SqlConnection connection, StorageValue value, int id) {
			using SqlCommand command = new SqlCommand(@$"USE Storehouse

UPDATE Storage
SET Name = '{value.Name}',
	Type = '{value.Type}',
	Provider = '{value.Provider}',
	Quantity = '{value.Quantity}',
	PrimeCost = '{value.PrimeCost}',
	OrderDate = '{value.OrderDate}'
WHERE Id = {id}

", connection);

			command.ExecuteNonQuery();
		}
		private static void UpdateOneValueWhere(SqlConnection connection, string name, string from, string to, bool isText) {
			using SqlCommand command = new SqlCommand(@$"USE Storehouse

UPDATE Storage
SET {name} = {(isText ? $"'{to}'" : to)}
WHERE {name} = {(isText ? $"'{from}'" : from)}

", connection);

			command.ExecuteNonQuery();
		}
	}
}