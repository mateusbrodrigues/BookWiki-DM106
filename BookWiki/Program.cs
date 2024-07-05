using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

app.MapGet("/", () =>
{
    var dal = new DAL<Book>(new BookWikiContext());
    return dal.Read();

});
app.Run();
