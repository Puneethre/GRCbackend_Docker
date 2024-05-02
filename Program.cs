using GRCServices;
using GRCServices.Data;
using GRCServices.Interfaces;
using GRCServices.Services;
using LicenseManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
System.AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddDbContext<GRCDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IRoleMaster, RoleMasterService>();
builder.Services.AddScoped<IActivityMaster, ActivityMasterService>();
builder.Services.AddScoped<LoginInterface, LoginService>();
builder.Services.AddScoped<IUserMaster, UserMasterService>();
builder.Services.AddScoped<IAssignmentMaster, AssignmentMasterService>();
builder.Services.AddScoped<ICustomerMaster, CustomerMasterService>();
builder.Services.AddScoped<ILicenseManagement, LicenseManagementService>();

var app = builder.Build();
ConfigurationHelper.Initialize(configuration: app.Services.GetRequiredService<IConfiguration>());
EmailUtility.Email(configuration: app.Services.GetRequiredService<IConfiguration>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
