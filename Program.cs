using design_pattern_case_1.Data;
using design_pattern_case_1.ThirdParty;
using design_pattern_case_1.ThirdParty.As_Sunnah;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
builder.Services.AddEnyimMemcached(options =>
{
    options.AddServer("127.0.0.1", 11211);
});
builder.Services.AddSingleton<IHadithService, HadithService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEnyimMemcached();

// Comment out HTTPS redirection in development
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
