using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MovieShopMVC.Filters;
using MovieShopMVC.Middleware;
using Serilog;
using Serilog.Formatting.Compact;

// Configure Serilog BEFORE building the app
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "MovieShopMVC")
    .WriteTo.File(
        new CompactJsonFormatter(),
        "Logs/exceptions-.json",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
    )
    .WriteTo.File(
        new CompactJsonFormatter(),
        "Logs/filter-logs-.json",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .WriteTo.Console()
    .CreateLogger();

try {
    Log.Information("Starting MovieShopMVC application");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped<IMovieService, MovieService>();
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
    builder.Services.AddScoped<IPurchaseService, PurchaseService>();
    builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
    builder.Services.AddScoped<IFavoriteService, FavoriteService>();
    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<IReviewService, ReviewService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ICastService, CastService>();
    builder.Services.AddScoped<ICastRepository, CastRepository>();
    builder.Services.AddScoped<IAdminService, AdminService>();

    builder.Services.AddDbContext<MovieShopDbContext>(options => {
        options.UseLazyLoadingProxies()
               .UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
    });

    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    builder.Services.AddScoped<CreateMovieLoggingFilter>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    // Add Exception Handling Middleware EARLY in the pipeline
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseSession();
    app.UseAuthorization();

    // Enable Serilog request logging
    app.UseSerilogRequestLogging();

    app.MapStaticAssets();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    Log.Information("MovieShopMVC application started successfully");

    app.Run();
} catch (Exception ex) {
    Log.Fatal(ex, "Application terminated unexpectedly");
} finally {
    Log.CloseAndFlush();
}