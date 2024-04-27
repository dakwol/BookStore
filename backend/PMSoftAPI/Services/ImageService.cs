namespace PMSoftAPI.Services
{
    public class ImageService()
    {
        private const string MediaFolder = @"C:\RiderProjects\PMSoftAPI\media";

        public async Task<string> Upload(IFormFile file)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(MediaFolder, uniqueFileName);
            if (!Directory.Exists(MediaFolder))
            {
                Directory.CreateDirectory(MediaFolder);
            }

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("media", uniqueFileName);
        }
    }
}