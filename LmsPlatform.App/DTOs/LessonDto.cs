namespace LmsPlatform.App.DTOs;

public class LessonDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
}
