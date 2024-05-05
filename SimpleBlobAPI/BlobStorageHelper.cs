using Azure.Storage;
using Azure.Storage.Blobs;

namespace SimpleBlobAPI;

public static class BlobStorageHelper
{
    public static async Task DownloadBlobAsync()
    {
        var blobClient = new BlobClient(
            new Uri(""),
            new StorageSharedKeyCredential("", ""));

        var localFilePath = "C:\\Temp\\GR2Historical_Test_Files\\";
        var fileToSave = "downloadedBlob.json";

        // Check if the blob exists
        if (await blobClient.ExistsAsync())
        {
            // Download the blob to a local file
            await blobClient.DownloadToAsync(Path.Combine(localFilePath, fileToSave));
            Console.WriteLine($"Blob downloaded to {localFilePath}.");
        }
    }

    public static async Task UploadBlobAsync(string containerName)
    {
        var pathToUploadFile = "C:\\Temp\\GR2Historical_Test_Files\\downloadedBlob.json";

        BlobServiceClient blobServiceClient = new BlobServiceClient("");
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Create the container if it doesn't exist
        await containerClient.CreateIfNotExistsAsync();

        // Get a reference to a blob; the name given is what will be created and stored
        BlobClient blobClient = containerClient.GetBlobClient("test-blob");

        await blobClient.UploadAsync(pathToUploadFile, true);
        Console.WriteLine($"Blob uploaded from {pathToUploadFile}.");
    }
}
