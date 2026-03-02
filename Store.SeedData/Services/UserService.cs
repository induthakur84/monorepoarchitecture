using Microsoft.Data.SqlClient;
using Store.SeedData.Model;
using Store.SeedData.Utils;

namespace Store.SeedData.Services
{



    // this class is responsibel for seeding (insert or update ) user data into sql server
    public class UserService
    {
        //it will hold sql database conneciton string read from appseting.json file

        private static string _storeConnectionSting;

        //here we can add hardcode seed dat

        public readonly List<(string Name, string Email)> _users = new()
        {
            ("User 1","user1@store.com"),
             ("User 2","user2@store.com"),
              ("User 3","user3@store.com"),
               ("User 4","use4@store.com"),
                ("User 5","user5@store.com"),
        };

        //Constructor receiver configuration setings
        // We can inject AppConfigSeting  to avoid hardcoding connection string


        public UserService(AppConfigSettings appConfigSettings) 
        {
            _storeConnectionSting=appConfigSettings.Store_DbConnectionString;
        }


        // main method that processes Sql Seed DAt

        //Open Sql Connection
        // Start Transaction
        // Loop insert the users list
        // Excute with the help merge


        //Traction ensure:

        ///All users insert/updated sucessfully or
        ///Everything rolled back if any error occurs


        public async Task ProcessUserSqlData()
        {
            Console.WriteLine("Start processing User Sql DAta");

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                //Open database connection using helper
                connection = await SqlServerHelper.GetSqlConnectionAsync(_storeConnectionSting);

                // here we begin the tranction to main the operation
                transaction = (SqlTransaction)await connection.BeginTransactionAsync();
                foreach (var (name, email) in _users)
                {
                    //Create Sql Command using merge upset query

                    using var cmd= new SqlCommand(SqlConstants.UpsertUser, connection, transaction);

                    //here we can easily increase timeout for large seed operations

                    cmd.CommandTimeout = 1000;

                    //Add parameter to prevent sql operations

                    cmd.Parameters.AddWithValue("@Name", name);

                    cmd.Parameters.AddWithValue("@Email", email);

                    //Excute query (inser or update depending on mathcj)

                    await cmd.ExecuteNonQueryAsync();
                }
                await transaction.CommitAsync();   // ✅ THIS WAS MISSING

            }
            catch (Exception ex)
            {
                // If any error occurs → rollback all changes
                if (transaction != null)
                {
                    try { await transaction.RollbackAsync(); } catch { /* ignore rollback errors */ }
                }

                Console.WriteLine(ex.Message);

                // Throw again so Program.cs knows seeding failed
                throw;
            }
            finally
            {
                // Always close and dispose connection to avoid memory leaks
                if (connection != null)
                {
                    try { await connection.CloseAsync(); } catch { }
                    await connection.DisposeAsync();
                }
            }
        }
    }
}
