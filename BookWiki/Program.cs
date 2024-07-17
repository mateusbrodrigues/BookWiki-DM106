using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using BookWiki.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<BookWikiContext>();
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Author>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndPointsBook();
app.AddEndPointsAuthor();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
