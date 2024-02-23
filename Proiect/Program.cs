global using Proiect.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Proiect.Repositories.ProjectRepository;
using Proiect.Data;
using Proiect.Repositories.TaskRepository;
using Proiect.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));



builder.Services.AddControllers();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authorization header using the Bearer Scheme ( \"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
			.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});
// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


