using SQLite;

namespace GymLifts.Services;

public class DatabaseService
{
    public const string DatabaseFilename = "GymLiftsSQLite.db3";
    private readonly SQLiteAsyncConnection _connection;

    public DatabaseService()
    {
        _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename));
    }
}
