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

//Account
app.MapGet("/accounts", async (TransactionDb db) => await db.Accounts.ToListAsync());
app.MapGet("/account/{id}", async (TransactionDb db, int id) => await db.Accounts.FindAsync(id));
app.MapPost("/account", async (TransactionDb db, Account account) =>
{
    await db.Accounts.AddAsync(account);
    await db.SaveChangesAsync();
    return Results.Created($"/account/{account.Id}", account);
});
app.MapPut("/account/{id}", async (TransactionDb db, Account update, int id) =>
{
    var account = await db.Accounts.FindAsync(id);
    if (account is null) return Results.NotFound();
    account.Name = update.Name;
    // need to check that i do not needs to loop here 
    account.Transactions = update.Transactions;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/account/{id}", async (TransactionDb db, int id) =>
{
    var account = await db.Accounts.FindAsync(id);
    if (account is null) return Results.NotFound();
    db.Accounts.Remove(account);
    //need to delete all transactions linked to account
    var transactions = await db.Transactions.Where(t => t.AccountId == id).ToListAsync();
    if (transactions is not null && transactions.Count != 0) 
        db.Transactions.RemoveRange(transactions);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//Transactions
app.MapGet("/account/{accountId}/transactions/", async (TransactionDb db, int accountId) => await db.Transactions.Where(t => t.AccountId == accountId).ToListAsync());
app.MapGet("/account/{accountId}/transactions/{type}", async (TransactionDb db, int accountId, TransactionType type) => await db.Transactions.Where(t => t.AccountId == accountId && t.Type == type).ToListAsync());
app.MapGet("/transaction/{id}", async (TransactionDb db, int id) => await db.Transactions.FindAsync(id));
app.MapPost("/transaction", async (TransactionDb db, AccountTransaction transaction) =>
{
    await db.Transactions.AddAsync(transaction);
    await db.SaveChangesAsync();
    return Results.Created($"/transaction/{transaction.Id}", transaction);
});
app.MapPut("/transaction/{id}", async (TransactionDb db, AccountTransaction update, int id) =>
{
    var transaction = await db.Transactions.FindAsync(id);
    if (transaction is null) return Results.NotFound();
    //yuck, need automapper
    transaction.Date = update.Date;
    transaction.Type = update.Type;
    transaction.Reference = update.Reference;
    transaction.Amount = update.Amount;
    transaction.AccountId = update.AccountId;
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

app.Run("https://localhost:7170");
