using System;

namespace NotesApp.Services;

public interface INoteHtmlSanitizer
{
    string Sanitize(string? html);
}


