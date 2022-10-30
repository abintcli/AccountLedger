using System.Globalization;
using AccountLedger.Services;
using AccountLedger.Web.Services;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var apiUri = new Uri("https://localhost:7170/");
builder.Services.AddHttpClient<ITransactionService, TransactionService>(c =>
    c.BaseAddress = apiUri
    );
builder.Services.AddHttpClient<IAccountService, AccountService>(c =>
    c.BaseAddress = apiUri
    );

//builder.Services.AddHttpClient<IAccountService, AccountService>();

var app = builder.Build();

// var culture = CultureInfo.CreateSpecificCulture("en-ZA");
var cultures = new List<CultureInfo>{
    new CultureInfo("en-za")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("en-za")),
    // DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-ZA"),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
