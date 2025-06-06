namespace BasicBlazorLibrary.Helpers;
public static class ImageUtilities
{
    public static string GetMimeTypeFromExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".svg" => "image/svg+xml",
            _ => throw new InvalidOperationException($"Unsupported file type: {ext}")
        };
    }
    public static string ConvertToBase64Image(string filePath)
    {
        if (ff1.FileExists(filePath))
        {
            byte[] imageBytes = ff1.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(imageBytes);
            string mime = GetMimeTypeFromExtension(filePath);
            return $"data:{mime};base64,{base64}";
        }
        return string.Empty;
    }
    public static async Task<string> ConvertToBase64ImageAsync(string filePath)
    {
        if (ff1.FileExists(filePath))
        {
            byte[] imageBytes = await ff1.ReadAllBytesAsync(filePath);
            string base64 = Convert.ToBase64String(imageBytes);
            string mime = GetMimeTypeFromExtension(filePath);
            return $"data:{mime};base64,{base64}";
        }
        return string.Empty;
    }
}