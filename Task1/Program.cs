using System.Data.SqlClient;

namespace Task1
{
	internal class StorageValue {
		public string Name;
		public string Type;
		public string Provider;
		public int Quantity;
		public double PrimeCost;
		public DateOnly OrderDate;
	}

	internal class Program
	{
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

			StorageValue value = new StorageValue();
			Console.Write("Name: ");
			value.Name = Console.ReadLine() ?? "Default";
			Console.Write("Type: ");
			value.Type = Console.ReadLine() ?? "Default";
			Console.Write("Provider: ");
			value.Provider = Console.ReadLine() ?? "Default";
			Console.Write("Quantity: ");
			value.Quantity = int.Parse(Console.ReadLine() ?? "1");
			Console.Write("PrimeCost: ");
			value.PrimeCost = double.Parse(Console.ReadLine() ?? "1");
			Console.Write("OrderDate: ");
			value.OrderDate = DateOnly.Parse(Console.ReadLine() ?? "");

			using SqlCommand command = new SqlCommand(@$"USE Storehouse

INSERT INTO Storage (Name, Type, Provider, Quantity, PrimeCost, OrderDate)
VALUES ('{value.Name}', '{value.Type}', '{value.Provider}', '{value.Quantity}', '{value.PrimeCost}', '{value.OrderDate}')

", connection);

			try {
				command.ExecuteNonQuery();
			}
			catch {
                Console.WriteLine("Cant add that value to database");
				return;
            }

            Console.WriteLine("Value added to database");
        }
	}
}
