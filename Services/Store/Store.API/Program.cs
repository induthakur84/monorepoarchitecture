using ApiUtility.ActionFilters;
using ApiUtility.Middleware;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));


//we need to register the response filter attribute as a service so
//that it can be injected into the controllers

builder.Services.AddScoped(typeof(ResponseFilterAttribute<>));



// Register IUserData and its implementation UserData

//1000
builder.Services.RegisterServices(typeof(UserData).Assembly.FullName);


//Register AutoMapper profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


//Global Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();


var deployDatabaseChanges = app.Configuration.GetValue<bool>("DeployDatabaseChanges");

if (deployDatabaseChanges)
{
    UpdateDatabase(app);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// middleware
app.UseAuthorization();

app.MapControllers();

app.Run();


void UpdateDatabase(IApplicationBuilder app)
{
    Task.Run(() =>
    {
        using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<StoreDbContext>();
        context.Database.SetCommandTimeout(1800);
        context.Database.Migrate();
        //context.AddTemporalTableSupport("dbo", "History");
    });
}
