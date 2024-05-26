global using Secim2028.Models;
global using Secim2028.Data;
global using Secim2028.Services.SehirService;
global using Secim2028.Services.IlceService;
global using Secim2028.Services.SiyasiPartiService;
global using Secim2028.Services.IttifakService;
global using Secim2028.Dtos;
using System.Text.Json.Serialization;
using System.Text.Json;
using Secim2028.Services.AdayService;
using Secim2028.Services.SandikService;
using Secim2028.Services.OyServices;
using Secim2028.Services.OyOranService;
using Secim2028.Helper.RandomOyHelper;
using Secim2028.Helper.FirstAdayHelper;
using Secim2028.Helper.FirstPartiHelper;
using Secim2028.Services.AuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.MaxDepth = 64; // Increase the depth if needed
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<ISecimIlService, SecimIlService>();
builder.Services.AddScoped<ISecimIlceService, SecimIlceService>();
builder.Services.AddScoped<ISiyasiPartiService, SiyasiPartiService>();
builder.Services.AddScoped<IIttifakService, IttifakService>();
builder.Services.AddScoped<ISecimAdayService, SecimAdayService>();
builder.Services.AddScoped<ISecimSandikService,SecimSandikService>();
builder.Services.AddScoped<IOyAdayService, OyAdayService>();
builder.Services.AddScoped<IOyPartiService, OyPartiService>();
builder.Services.AddScoped<IOyOranService, OyOranService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IFirstAndLastSandikHelper, FirstAndLastSandikHelper>();
builder.Services.AddScoped<ITurkiyeFirstAdayEachIlHelper, TurkiyeFirstAdayEachIlHelper>();
builder.Services.AddScoped<ITurkiyeFirstPartiEachIlHelper, TurkiyeFirstPartiEachIlHelper>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("jwt:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
