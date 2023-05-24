using Backend.Context;
using BlazorFrontend.Services;

// using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SubjectMemberService>();
builder.Services.AddSqlite<OnlineSchoolStoreContext>("Data Source=db.sqlite");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
