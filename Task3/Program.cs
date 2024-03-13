using System.Data.SqlClient;

namespace Task3
{
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

            Console.Write("Id of value to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Id must be seelcted");
                return;
			}

			using SqlCommand command = new SqlCommand(@$"USE Storehouse
DELETE FROM Storage WHERE Id = {id}
", connection);

			try {
				command.ExecuteNonQuery();
			}
			catch {
                Console.WriteLine("Cant delete that value");
                return;
			}

            Console.WriteLine("Value deleted");
        }
	}
}