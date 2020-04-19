using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssetDownloader.Models
{
    public class Fingerprint
    {
        [JsonProperty("files")] public List<AssetFile> Files = new List<AssetFile>();
        [JsonProperty("sha")] public string Sha { get; set; }
        [JsonProperty("version")] public string Version { get; set; }
    }
}