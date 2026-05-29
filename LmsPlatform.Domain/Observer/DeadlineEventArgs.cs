namespace LmsPlatform.Domain.Observer;

public class DeadlineEventArgs : EventArgs
{
    public Guid CourseId { get; }
    public string CourseName { get; }
    public DateTime Deadline { get; }
    public int DaysRemaining { get; }

    public DeadlineEventArgs(Guid courseId, string courseName, DateTime deadline)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentException("ID курсу не може бути порожніст.", nameof(courseId));
        
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Назва курсу не може бути порожною.", nameof(courseName));
        
        CourseId = courseId;
        CourseName = courseName;
        Deadline = deadline;
        DaysRemaining = (int)(deadline - DateTime.UtcNow).TotalDays;
    }
}
