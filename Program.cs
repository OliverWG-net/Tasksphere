using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
namespace TaskSphere
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDo;Integrated Security=True;Trust Server Certificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Databasen är öppen");
                    string query = "SELECT * FROM Categories";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Hämta alla kategorier");
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.WriteLine($"{reader[i]}");
                                }
                                Console.WriteLine();
                            }
                        }
 

                    }
                    Console.WriteLine("Vänligen skirv in uppgiten du vill lägga till");
                    Console.WriteLine("Vänligen skriv in Vilken kategorie uppgiften tillhör. 1 är Cleaning, 2 är Work, 3 är Study, 4 är Pray");
                    Int32.TryParse(Console.ReadLine(), out int catid);
                    Console.WriteLine("Skirv in namn på uppgiften max 30 karaktärer.");
                    string inputname = Console.ReadLine();
                    Console.WriteLine("Vänligen skriv in Datumet uppgiften ska utföras som yyyy-mm-dd");
                    string inputdate = Console.ReadLine();


                    string query1 = $"INSERT INTO Tasks(FkCategoryId,TaskName,TaskDate)VALUES({catid},'{inputname}','{inputdate}')";

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {

                        Console.WriteLine("Uppgifter har uppdaterats");
                        command.ExecuteNonQuery();
                    }
                    string query2 = "SELECT * FROM Tasks";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Hämta alla tasks");
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.WriteLine($"{reader[i]}");
                                }
                                Console.WriteLine();
                            }
                        }


                    }
                    Console.WriteLine("Vänligen skriv in Id för uppgiften du vill markera som klar.");
                    int.TryParse(Console.ReadLine(), out int userinput);
                    string query3 = $"UPDATE Tasks SET TaskStatus = 1 WHERE TaskId = {userinput}";
                    using (SqlCommand command = new SqlCommand(query3, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                   



                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}