using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AssetDownloader.Models;
using Newtonsoft.Json;

namespace AssetDownloader
{
    public class Program
    {
        private const string CdnHost =
            "http://b46f744d64acd2191eda-3720c0374d47e9a0dd52be4d281c260f.r11.cf2.rackcdn.com/";

        private const string OutputFolder = "Assets/";
        public static HttpClient Client = new HttpClient();

        private static void Main()
        {
            Console.Title = "AssetDownloader 1.0";

            if (File.Exists("fingerprint.json"))
            {
                var json = File.ReadAllText("fingerprint.json");
                var fingerprint = JsonConvert.DeserializeObject<Fingerprint>(json);

                Console.WriteLine($"Version: {fingerprint.Version}");
                Console.WriteLine($"SHA:     {fingerprint.Sha}\n");
                Console.WriteLine($"Downloading {fingerprint.Files.Count} files...");

                Parallel.ForEach(fingerprint.Files,
                    async asset => await DownloadFileAsync(fingerprint.Sha, asset.File));
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            Console.ReadKey(true);
        }

        public static async Task DownloadFileAsync(string fingerprintSha, string file)
        {
            var url = new Uri(CdnHost + fingerprintSha + "/" + file);
            var fileName = Path.GetFileName(file);
            var folder = OutputFolder + Path.GetDirectoryName(file).Replace(@"\", "/");

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var bytes = await Client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(folder + "/" + fileName, bytes);

            Console.WriteLine($"Downloaded {file}");
        }
    }
}