using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NotesApp.Services;

public class NoteHtmlSanitizer : INoteHtmlSanitizer
{
    private static readonly Regex ScriptStyleRegex = new(@"<\s*(script|style)[^>]*>.*?<\s*/\s*\1\s*>",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    private static readonly Regex TagRegex = new(@"</?([a-zA-Z0-9]+)([^>]*)>",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    private static readonly Regex ClassAttrRegex = new(@"\bclass\s*=\s*[""']([^""']+)[""']",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly HashSet<string> AllowedTags = new(StringComparer.OrdinalIgnoreCase)
    {
        "pre",
        "code",
        "b",
        "strong",
        "i",
        "em",
        "ul",
        "ol",
        "li",
        "p",
        "br"
    };

    public string Sanitize(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        var withoutScripts = ScriptStyleRegex.Replace(html, string.Empty);

        var sanitized = TagRegex.Replace(withoutScripts, m =>
        {
            var tagName = m.Groups[1].Value;
            if (!AllowedTags.Contains(tagName))
            {
                return string.Empty;
            }

            var isClosing = m.Value[1] == '/';
            var lowerName = tagName.ToLowerInvariant();

            if (isClosing)
            {
                return $"</{lowerName}>";
            }

            if (string.Equals(lowerName, "code", StringComparison.Ordinal))
            {
                var attrs = m.Groups[2].Value;
                var classMatch = ClassAttrRegex.Match(attrs);
                if (classMatch.Success)
                {
                    var classValue = classMatch.Groups[1].Value;
                    var allowedClasses = classValue
                        .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(c => c.StartsWith("language-", StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    if (allowedClasses.Length > 0)
                    {
                        var classAttr = string.Join(" ", allowedClasses);
                        return $"<code class=\"{classAttr}\">";
                    }
                }

                return "<code>";
            }

            if (string.Equals(lowerName, "br", StringComparison.Ordinal))
            {
                return "<br>";
            }

            return $"<{lowerName}>";
        });

        return sanitized;
    }
}


