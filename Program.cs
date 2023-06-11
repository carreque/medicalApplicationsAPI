using AutoMapper;
using medicalAppointmentsAPI;
using medicalAppointmentsAPI.Repositories;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsAPI.Services;
using medicalAppointmentsApplication.ContextApplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IMapper mapper = MappingDTO.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper); //Unique instance of mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Repositorios
builder.Services.AddScoped<IUserRepository, UsuarioRepositorio>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:keyToken").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
builder.Services.AddSwaggerGen(conf =>
{
    conf.OperationFilter<SecurityRequirementsOperationFilter>();
    conf.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization Standard, you must use JWT bearer. Example \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

builder.Services.AddCors();
//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<DiagnosticoService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(conf => conf.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
app.MapControllers();

app.Run();
