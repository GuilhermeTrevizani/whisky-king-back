﻿namespace WhiskyKing.Core.Models.Responses;

public class ErrorsResponse
{
    public required IEnumerable<string> Errors { get; set; }
}