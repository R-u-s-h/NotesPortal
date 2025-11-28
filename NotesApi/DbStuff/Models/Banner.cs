namespace NotesApi.DbStuff.Models;

public class Banner : BaseModel
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string ImageUrl { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}