using MinimalApis.Controllers;
using MinimalApis.Data;
using MinimalApis.Models;

var builder = WebApplication.CreateBuilder(args);

string CORS = "My CORS";
ApiContext context = new(builder.Configuration.GetConnectionString("MySQLConnection"));
UsersController controller = new(context);

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

app.MapGet("/users/{userId}", async (int userId) => {
    var user = await controller.Get(userId);
    return user is null ? new User() : user;
});

app.MapGet("/users", async () => {
    var users = await controller.GetAll();
    return users;
});

app.MapPost("/users", async (User user) => {
    User added = await controller.Add(user);
    return Results.Created($"/users/{ added.Id }", added);
});

app.MapPut("/users", async (User user) => {
    bool userWasUpdated = await controller.Update(user);
    return userWasUpdated ? Results.NoContent() : Results.BadRequest();
});

app.MapDelete("/users/{userId}", async (int userId) => {
    bool userWasDeleted = await controller.Delete(userId);
    return userWasDeleted ? Results.Ok() : Results.BadRequest();
});

app.Run();