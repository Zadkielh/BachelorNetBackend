var AllowAllOrigin = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllOrigin, pol => pol.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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


app.UseCors(AllowAllOrigin);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
