﻿using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors;

/// <summary>
///     Represents a discord Json error response property.
/// </summary>
public record PropertyErrorData
{
    /// <summary>
    ///     The sub errors of the error.
    /// </summary>
    public IReadOnlyDictionary<string, PropertyErrorData>? SubErrors { get; init; }

    /// <summary>
    ///     The error infos explaining what went wrong.
    /// </summary>
    public IEnumerable<PropertyErrorInfoData>? Errors { get; init; }
}