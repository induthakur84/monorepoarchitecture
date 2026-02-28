using Microsoft.Extensions.Configuration;
using Store.SeedData.Model;

Console.WriteLine("Store Seed Data");

// here we can build configuration to read the appsettings.json file and get the connection string and other settings


// Why?

//To read the connection string and flags without hardcoding them in the code, and to make it easier to change the settings without changing the code
var configuration = new ConfigurationBuilder()

    // here we can set current directory so appsettings.json can be found and read by the configuration builder
    .SetBasePath(Directory.GetCurrentDirectory())

    //Load the appsettings.json file to read the settings from it
    .AddJsonFile("appsettings.json")
    .Build();



// here we can bind configuartion settings to a strongly typed class to make it easier to access the settings in the code

var appConfigSettings = configuration.Get<AppConfigSettings>();

// for the safety check we can add here check null
if(appConfigSettings == null)
{
    throw new Exception("AppConfigSettings is null. Please check the appsettings.json file and make sure it has the correct settings.");
}

//another safety check to ensure connection is existing in the appsettings.json file and not empty
if (string.IsNullOrWhiteSpace(appConfigSettings.Store_DbConnectionString))
{
    throw new Exception("Store_DbConnectionString is null or empty. Please check the appsettings.json file and make sure it has the correct connection string.");
}


