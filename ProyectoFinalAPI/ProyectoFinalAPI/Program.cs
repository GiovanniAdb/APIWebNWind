using Microsoft.EntityFrameworkCore;
using ProyectoFinalAPI.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("NorthwindDB"))
);

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(politica =>
        politica.WithOrigins("http://localhost:84", "http://192.168.1.68:84", "http://127.0.0.1:5500", "http://192.168.155.17:84")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
);
});

builder.Services.AddControllers();
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

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();