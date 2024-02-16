using Newtonsoft.Json;

namespace ConsolePhotoAlbumApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var photos = await GetAllPhotosAsync();
            GroupAndDisplayPhotos(photos);
        }

        private static async Task<List<Photo>> GetAllPhotosAsync()
        {
            using var httpClient = new HttpClient();

            string apiUrl = "https://jsonplaceholder.typicode.com/photos";
            var response = await httpClient.GetStringAsync(apiUrl);
            var photos = JsonConvert.DeserializeObject<List<Photo>>(response);

            return photos ?? new List<Photo>();
        }

        private static void GroupAndDisplayPhotos(List<Photo> photos)
        {
            var groupedPhotos = new Dictionary<int, List<Photo>>();

            foreach (var photo in photos)
            {
                if (!groupedPhotos.ContainsKey(photo.AlbumId))
                    groupedPhotos[photo.AlbumId] = new List<Photo>();

                groupedPhotos[photo.AlbumId].Add(photo);
            }

            foreach (var album in groupedPhotos)
            {
                foreach (var photo in album.Value)
                {
                    Console.WriteLine($"photo-album {album.Key} [{photo.Id}] {photo.Title}");
                }

                Console.WriteLine();
            }
        }
    }
}
