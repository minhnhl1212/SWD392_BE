namespace Core.GCS;

using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

public interface IGCSService
{
  Task<string?> UploadFileAsync(Stream fileStream, string destinationFileName, string contentType);
}

public class GCSService : IGCSService
{
  private const string PROJECT_ID = "parabolic-rhino-426704-h1";
  private const string FOLDER = "media/";
  private readonly string BucketName;
  private readonly StorageClient StorageClient;

  private readonly string FilePrefix;

  public GCSService()
  {
    // Initialize the Google Cloud Storage client
    string credentialsPath = "firebase.json"; // Replace with your Service Account Key path

    var credentials = GoogleCredential.FromFile(credentialsPath);
    StorageClient = StorageClient.Create(credentials);
    BucketName = PROJECT_ID + ".appspot.com"; // Default Firebase Storage bucket
    FilePrefix = "https://storage.googleapis.com/" + BucketName + "/";
  }

  public async Task<string?> UploadFileAsync(Stream fileStream, string destinationFileName, string contentType)
  {
    try
    {
      var objectName = FOLDER + destinationFileName;

      // Upload the file to Firebase Storage
      var options = new UploadObjectOptions
      {
        PredefinedAcl = PredefinedObjectAcl.PublicRead // Set the PredefinedAcl to PublicRead
      };
      await StorageClient.UploadObjectAsync(BucketName, objectName, contentType, fileStream, options);

      // Generate a URL to the uploaded content
      var fileUrl = FilePrefix + objectName;
      return fileUrl;
    }
    catch (Exception ex)
    {
      // Handle any exceptions that may occur during the upload
      Console.WriteLine($"Error uploading file: {ex.Message}");
      return null;
    }
  }
}
