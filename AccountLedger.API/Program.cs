using Microsoft.OpenApi.Models;
using AccountLedger.API.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });

app.MapGet("/", () => "Hello World!");

//My new Routes for Transactions
app.MapGet("/transactions/{id}", (int id) => TransactionDB.GetTransaction(id));
app.MapGet("/transactions", () => TransactionDB.GetTransactions());
app.MapPost("/transactions", (AccountTransaction transaction) => TransactionDB.CreateTransaction(transaction));
app.MapPut("/transactions", (AccountTransaction transaction) => TransactionDB.UpdateTransaction(transaction));
app.MapDelete("/transactions", (int id) => TransactionDB.RemoveTransaction(id));

app.Run();
