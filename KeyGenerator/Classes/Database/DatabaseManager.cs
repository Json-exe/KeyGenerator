using Newtonsoft.Json;

namespace KeyGenerator.Classes.Database;

public class DatabaseManager
{
    [JsonIgnore] public List<IDatabase<DatabaseType>> Databases { get; set; } = new();

    [JsonProperty] private List<Dictionary<DatabaseType, object>> DatabaseData { get; set; } = new();
    
    public void ConvertKeyModulesToData()
    {
        DatabaseData.Clear();
        foreach (var database in Databases)
            DatabaseData.Add(new Dictionary<DatabaseType, object>
            {
                { database.DatabaseType, database }
            });
    }

    public void ConfigureKeyModulesFromData()
    {
        foreach (var databaseData in DatabaseData)
        foreach (var (key, value) in databaseData)
            try
            {
                // The value is still a JObject, so we need to convert it to the correct type.
                var valueString = value.ToString();
                if (valueString != null)
                {
                    var valueObject = JsonConvert.DeserializeObject(valueString, key switch
                    {
                        DatabaseType.MySQL => typeof(MySqlDatabaseInformation),
                        _ => throw new ArgumentOutOfRangeException()
                    });
                    if (valueObject != null)
                        Databases.Add((IDatabase<DatabaseType>)valueObject);
                    else
                        Console.WriteLine("A database could not be loaded! Database-Type: " + key);
                }
                else
                {
                    Console.WriteLine("A database could not be loaded! Database-Type: " + key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error loading your database! Error: " + e);
            }
    }
}