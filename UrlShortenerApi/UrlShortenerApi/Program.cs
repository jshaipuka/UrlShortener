var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/url/{urlHash}",  () => "Return original URL");

app.Run();