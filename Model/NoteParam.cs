﻿namespace Pellucid.Core.Api.Model
{
    public class NoteParam
    {
        public string Id { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
    }
}
