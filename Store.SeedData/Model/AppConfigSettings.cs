namespace Store.SeedData.Model
{
    public class AppConfigSettings
    {

        // database connection string

        //used by the seed services to connect to the database and seed data or insert/update data
        public string Store_DbConnectionString { get; set; }


        //here we can make master switch to enable or disable seeding sql data to the database
        // if false => no seeding will be done to the database

        public bool ProcessSqlData { get;set; }

        //here we will add specfice flag to control user seed data in the database
        // if true => user seed data will be inserted or updated in the database
        // if false => no user seed data will be inserted or updated in the database

         public bool ProcessUserToSqlData { get; set; }
    }
}

//
//{
 //"Store_DbConnectionString": "Server=localhost;Database=StoreDb;Trusted_Connection=True;MultipleActiveResultSets=true",
 //   "ProcessSqlData": true,
 //   "ProcessUserToSqlData": true