using Newtonsoft.Json;

namespace AssetDownloader.Models
{
    public class AssetFile
    {
        [JsonProperty("sha")] public string Sha { get; set; }
        [JsonProperty("file")] public string File { get; set; }
    }
}