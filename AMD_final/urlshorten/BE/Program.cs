using URLShortener.API.Services;
using URLShortener.API.Models;

var builder = WebApplication.CreateBuilder(args);

// =======================
// ENABLE CORS
// =======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// =======================
// LOAD MONGO SETTINGS
// =======================
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

// =======================
// REGISTER SERVICES
// =======================
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<UrlService>();

// 🟢 THÊM 3 SERVICE CHO LOGIN Ở ĐÂY
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================
// BUILD APP
// =======================

var app = builder.Build();

app.UseCors("AllowAll");

// luôn bật swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
