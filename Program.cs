var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*Dependency injection (DI) container*/
builder.Services.AddDbContext<AppDbContext>(options => 
options.Sqlite("Data Source=fityou.db"));

app.MapGet("/", () => "Hello World!");

app.Run();
