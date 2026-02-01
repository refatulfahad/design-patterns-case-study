using design_pattern_case_1.Features.Comments.Domain.Observers;
using design_pattern_case_1.Features.Comments.Services;
using design_pattern_case_1.Features.Reports.Factories;
using design_pattern_case_1.Features.Reports.Services;
using design_pattern_case_1.Infrastructure.Data;
using design_pattern_case_1.Infrastructure.Notifications;
using design_pattern_case_1.Shared.Configurations;
using design_pattern_case_1.ThirdParty;
using design_pattern_case_1.ThirdParty.As_Sunnah;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddEnyimMemcached(options =>
{
    options.AddServer("127.0.0.1", 11211);
});

// Register Report Services (Template Method + Factory Pattern)
builder.Services.AddSingleton<IReportFactory, ReportFactory>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<ConfigService>();
builder.Services.AddSingleton<IHadithService, HadithService>();

// Register Observer Pattern (Comment Moderation)
builder.Services.AddSingleton<CommentSubject>(provider =>
{
    var subject = new CommentSubject();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    
    using var scope = scopeFactory.CreateScope();
    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
    
    // Attach only email notification and user ban observers
    subject.Attach(new EmailNotificationObserver(notificationService));
    subject.Attach(new UserBanObserver(scopeFactory));
    
    return subject;
});

builder.Services.AddHostedService<CommentBackGroundService>();
builder.Services.AddScoped<INotificationService, DaprNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEnyimMemcached();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
