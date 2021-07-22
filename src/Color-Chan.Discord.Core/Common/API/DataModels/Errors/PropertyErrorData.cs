using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors
{
    public record PropertyErrorData
    {
        public IReadOnlyDictionary<string, PropertyErrorData>? SubErrors { get; init; }

        public IEnumerable<PropertyErrorInfoData>? Errors { get; set; }
    }
}