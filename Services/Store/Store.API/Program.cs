var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


var deployDatabaseChanges = app.Configuration.GetValue<bool>("DeployDatabaseChanges");

if (deployDatabaseChanges)
{
  //  UpdateDatabase(app);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//void UpdateDatabase(IApplicationBuilder app)
//{
//    Task.Run(() =>
//    {
//        using var serviceScope = app.ApplicationServices
//        .GetRequiredService<IServiceScopeFactory>()
//        .CreateScope();
//        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
//        context.Database.SetCommandTimeout(1800);
//        context.Database.Migrate();
//        //context.AddTemporalTableSupport("dbo", "History");
//    });
//}
