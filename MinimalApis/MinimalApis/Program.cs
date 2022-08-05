using MinimalApis.Data;

var builder = WebApplication.CreateBuilder(args);

string CORS = "My CORS";
ApiContext context = new(builder.Configuration.GetConnectionString("MySQLConnection"));

builder.Services.AddCors(options => {
    options.AddPolicy(name: CORS, builderCors => {
        builderCors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors(CORS);

app.MapGet("/", () => "Hello World!");

/*----- ENDPOINTS -----*/

app.Run();