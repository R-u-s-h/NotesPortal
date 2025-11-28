namespace NotesApp.Controllers.CustomNotesAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ApiGroupAttribute : Attribute
{
    public string Name { get; }

    public ApiGroupAttribute(string name)
    {
        Name = name;
    }
}