using SimpleBlobAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

app.MapGet("/blob", async () =>
{
    await BlobStorageHelper.DownloadBlobAsync();
})
.WithName("GetBlob")
.WithOpenApi();

app.MapPost("/submitBlob", async (string containerName) =>
{
    // Pass in gr2-historical-test-container for containerName
    await BlobStorageHelper.UploadBlobAsync(containerName);
})
.WithName("PostSubmitBlob")
.WithOpenApi();

app.Run();