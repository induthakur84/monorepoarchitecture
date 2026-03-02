namespace Store.SeedData
{
    public class SqlConstants
    {

        //update or insert the user


        //Logic for updating or inserting the user
        //if the user exists, update the user
        //if the user does not exist, insert the user


        // why we used her merge?
        //1 to prevent duplicate data in the database
        //2 to improve performance by reducing the number of database calls
        //3 single query to handle both insert and update operations    

        internal const string UpsertUser =
            @"
           --Merge allow Upsert operation (update or insert) in a single query
            MERGE INTO [Users] AS Target   --Target = Main Users table where we want to insert or update data
            USING (VALUES (@Name, @Email)) AS Source (Name, Email)  --Source = the new data we want to insert or update
                 
              --Match Condition : Find User by Email
             ON Target.[Email]=Source.[Email]
             
              --if Matching email found, then update the user name
                WHEN MATCHED THEN
                    UPDATE SET Target.[Name] = Source.[Name]
             
              --if Email not Found--> Insert new Record
               
               WHEN NOT MATCHED THEN
                    INSERT ([Name],[Email])
                    VALUES(Source.[Name], Source.[Email]);
             ";
    }
}
