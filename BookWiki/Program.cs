using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using BookWiki.EndPoints;
using BookWiki.Shared.Models;
using BookWiki.Shared.Data.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<BookWikiContext>();
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Author>>();
builder.Services.AddTransient<DAL<Genre>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddIdentityApiEndpoints<AccessUser>()
    .AddEntityFrameworkStores<BookWikiContext>();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthorization();
app.AddEndPointsBook();
app.AddEndPointsAuthor();
app.AddEndPointsGenre();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGroup("auth").MapIdentityApi<AccessUser>().WithTags("Authorization");

app.MapPost("auth/logout", async ([FromServices] SignInManager<AccessUser> signInManager)
=> {
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Authorization");

app.Run();
