namespace KeyGenerator.Classes.Database;

public interface IDatabase<TDatabaseType>
{
    public TDatabaseType DatabaseType { get; set; }

    public bool ConnectToDatabase();
    
    public string ConnectionString { get; set; }
    public string InsertKeyQuery { get; set; }
    public string Name { get; set; }
}

public enum DatabaseType
{
    MySQL
}