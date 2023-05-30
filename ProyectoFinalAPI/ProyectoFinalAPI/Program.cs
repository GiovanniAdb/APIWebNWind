using Microsoft.EntityFrameworkCore;
using ProyectoFinalAPI.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("NorthwindDB"))
);

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(politica =>
        politica.WithOrigins("http://localhost:127")
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