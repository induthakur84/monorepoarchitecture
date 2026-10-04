using Microsoft.Data.SqlClient;

namespace Store.SeedData.Utils
{
    //sql server helper is the utility class that will contain helper methods to work with sql server database,
    //such as executing sql commands, inserting or updating data, etc.


    // why we need this class?
    //1 to avoid repeating the connection string and sql command execution code in multiple places in the seed services.
    //2 keed the code clean and reuseable (Dry Principle-- Don't Repeat Yourself)
    //3.Centralize the sql server related logic in one place, making it easier to maintain and update if needed.

    //Instead of writing this in every servie:
    // var conn= new SqlConnection(appConfigSettings.Store_DbConnectionString);

    // we can write a helper method in this class to execute sql commands and pass the connection string as a parameter, and then call this helper method in the seed services to execute the sql commands.
    public class SqlServerHelper
    {
        public static async Task<SqlConnection> GetSqlConnectionAsync(string connectionString)
        {

            //create sql connection using the connection string from the app config settings and open the connection and return it to the caller
            var conn = new SqlConnection(connectionString);
            //Open the connection asynchronously to avoid blocking the calling thread, especially if the connection takes time to establish. This is particularly important in scenarios where multiple connections might be opened concurrently, such as in a web application or during a seeding process that involves multiple database operations.
            await conn.OpenAsync();

            //By opening the connection asynchronously, we can improve the responsiveness of the application and allow other tasks to continue executing while waiting for the connection to be established. This is a best practice when working with database connections in modern applications.
            // return opened connetion to calling service to execute sql commands using it
            return conn;
        }
    }
}
