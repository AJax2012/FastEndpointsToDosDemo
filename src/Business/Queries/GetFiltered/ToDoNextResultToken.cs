using System.Text.Json;

namespace ToDosFE.Business.Queries.GetFiltered;

internal sealed record ToDoNextResultToken(Ulid PreviousLastId, string PreviousLastTitle)
{
    internal static ToDoNextResultToken? DecodeToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }
        
        using var stream = new MemoryStream(Convert.FromBase64String(token));
        return JsonSerializer.Deserialize<ToDoNextResultToken>(stream)!;
    }

    internal static string EncodeToken(ToDoNextResultToken? token)
    {
        if (token is null)
        {
            return string.Empty;
        }
        
        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, token);
        return Convert.ToBase64String(stream.ToArray());
    }
}