using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MyNewProject.src.Core.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PolicyType
    {
        Life = 0,
        Travel = 1,
        Health = 2
    }
}
