using BachelorOppgaveBackend;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add cors
var AllowAllOrigin = "AllowOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllOrigin, pol => pol.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(options=> 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(AllowAllOrigin);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//"Server=c.sg6222938976534e0d862d260256f19fda.postgres.database.azure.com; Database=citus; Port=5432; User Id=citus; Password=Mosabjonn1814;"


