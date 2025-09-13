using FIT.JornalDoCondominio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JornalDoCondominioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("simpleAuth", new OpenApiSecurityScheme
    {
        Name = "X-User-Email",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "E-mail do usuário para autenticação"
    });

    c.AddSecurityDefinition("simplePassword", new OpenApiSecurityScheme
    {
        Name = "X-User-Password",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Senha do usuário para autenticação"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "X-User-Email",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "simpleAuth"
                }
            },
            new string[] {}
        },
        {
            new OpenApiSecurityScheme
            {
                Name = "X-User-Password",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "simplePassword"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
