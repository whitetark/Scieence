using Database;
using ScieenceAPI.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SpringerNatureClient>();
builder.Services.AddSingleton<SdSearchClient>();
builder.Services.AddSingleton<CoreClient>();
builder.Services.AddSingleton<DoajClient>();
builder.Services.AddSingleton<EricClient>();
builder.Services.AddSingleton<SemanticScholarClient>();

builder.Services.AddSingleton<DbClient>();
builder.Services.AddTransient<PubServices>();
builder.Services.Configure<DbConfig>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
