using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
// using AccountLedger.API.DB;
using AccountLedger.API.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Transactions") ?? "Data Source=AccountLedger.API.db";

builder.Services.AddSqlite<TransactionDb>(connectionString);
// builder.Services.AddDbContext<TransactionDb>(options => options.UseInMemoryDatabase("items"));

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
// app.MapGet("/transactions/{id}", (int id) => TransactionDB.GetTransaction(id));
// app.MapGet("/transactions", () => TransactionDB.GetTransactions());
// app.MapPost("/transactions", (AccountTransaction transaction) => TransactionDB.CreateTransaction(transaction));
// app.MapPut("/transactions", (AccountTransaction transaction) => TransactionDB.UpdateTransaction(transaction));
// app.MapDelete("/transactions", (int id) => TransactionDB.RemoveTransaction(id));

app.MapGet("/transactions", async (TransactionDb db) => await db.Transactions.ToListAsync());
app.MapPost("/transaction", async (TransactionDb db, AccountTransaction transaction) =>
{
    await db.Transactions.AddAsync(transaction);
    await db.SaveChangesAsync();
    return Results.Created($"/transaction/{transaction.Id}", transaction);
});
app.MapGet("/transaction/{id}", async (TransactionDb db, int id) => await db.Transactions.FindAsync(id));
app.MapPut("/transaction/{id}", async (TransactionDb db, AccountTransaction update, int id) =>
{
    var transaction = await db.Transactions.FindAsync(id);
    if (transaction is null) return Results.NotFound();
    //yuck, need automapper
    transaction.Date = update.Date;
    transaction.Type = update.Type;
    transaction.Reference = update.Reference;
    transaction.Amount = update.Amount;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/transaction/{id}", async (TransactionDb db, int id) =>
{
    var transaction = await db.Transactions.FindAsync(id);
    if (transaction is null) return Results.NotFound();
    db.Transactions.Remove(transaction);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
