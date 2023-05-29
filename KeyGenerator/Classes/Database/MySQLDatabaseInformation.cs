using MySql.Data.MySqlClient;

namespace KeyGenerator.Classes.Database;

public class MySqlDatabaseInformation : IDatabase<DatabaseType>
{
    public DatabaseType DatabaseType { get; set; } = DatabaseType.MySQL;
    public bool ConnectToDatabase()
    {
        var connection = new MySqlConnection(ConnectionString);
        try
        {
            connection.Open();
            connection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error connecting to your database! Error: " + e);
            return false;
        }
    }

    public string ConnectionString { get; set; }
    public string InsertKeyQuery { get; set; }
    public string Name { get; set; }
}