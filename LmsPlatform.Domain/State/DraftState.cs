namespace LmsPlatform.Domain.State;

public class DraftState : ICourseState
{
    public string StateName => "Draft";

    public void PublishCourse(CourseStateContext context)
    {
        context.SetState(new PublishedState());
    }

    public void ArchiveCourse(CourseStateContext context)
    {
        throw new InvalidCourseOperationException("Можна архівувати тільки опубліковані курси.");
    }

    public void RevertToDraft(CourseStateContext context)
    {
        throw new InvalidCourseOperationException("Курс вже у стану Draft.");
    }

    public bool CanAddLessons() => true;

    public bool CanModify() => true;
}
