using Azure.Storage;
using Azure.Storage.Blobs;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    var blobClient = new BlobClient(
        new Uri("https://devstorageclimavision.blob.core.windows.net/backup-v002/weather.json"),
        new StorageSharedKeyCredential("<STORAGE-ACCOUNT-NAME>", "<STORAGE-ACCOUNT-KEY>"));

    var localFilePath = "C:\\Temp\\GR2Historical_Test_Files\\";
    var fileToSave = "downloadedBlob.json";

    // Check if the blob exists
    if (await blobClient.ExistsAsync())
    {
        // Download the blob to a local file
        await blobClient.DownloadToAsync(Path.Combine(localFilePath, fileToSave));
        Console.WriteLine($"Blob downloaded to {localFilePath}.");
    }
})
.WithName("GetBlob")
.WithOpenApi();

app.Run();