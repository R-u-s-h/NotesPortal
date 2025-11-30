using Microsoft.EntityFrameworkCore;
using NotesApp.Controllers;
using NotesApp.CustomMiddleware;
using NotesApp.DbStuff;
using NotesApp.Hubs;
using NotesApp.Services;
using NotesApp.Services.AutoRegistrationInDI;
using NotesApp.Services.Permissions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services
    .AddAuthentication(AuthNotesController.AUTH_KEY)
    .AddCookie(AuthNotesController.AUTH_KEY, o =>
    {
        o.LoginPath = "/AuthNotes/Login";
        o.AccessDeniedPath = "/AuthNotes/AccessDenied";
    });

// Register db context
builder.Services.AddDbContext<NotesDbContext>(
    x => x.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultDbConnection"))
    );

// Register Services
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<IAuthNotesService, AuthNotesService>();
builder.Services.AddScoped<INotePermission, NotePermission>();

var autoRegisterService = new AutoRegisterService();
autoRegisterService.RegisterAllNotesRepositories(builder.Services);
autoRegisterService.RegisterAllByAttribute(builder.Services);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Notes/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Who am I?
app.UseAuthentication();
// What can I do?
app.UseAuthorization();

app.UseMiddleware<CustomNotesLocalizationMiddleware>();

app.MapHub<NotificationNotesHub>("/hubs/notification-notes");

// Enable attribute-routed API controllers
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Index}/{id?}");

await app.RunAsync();


