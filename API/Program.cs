using Business_Logic_Layer;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Games", () => clsGame.ListAsJSON());

app.MapGet("/Games/{Id}", (int Id) => clsGame.FindAsJson(Id));

app.MapGet("/Players", () => clsPlayer.ListAsJson());

app.MapGet("/Players/{Name,Password}", (string Name,string Password) => clsPlayer.FindAsJson(Name,Password));

app.Run();
