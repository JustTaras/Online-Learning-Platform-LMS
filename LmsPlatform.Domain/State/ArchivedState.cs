namespace LmsPlatform.Domain.State;

public class ArchivedState : ICourseState
{
    public string StateName => "Archived";

    public void PublishCourse(CourseStateContext context)
    {
        throw new InvalidCourseOperationException("Архівовані курси не можна публікувати.");
    }

    public void ArchiveCourse(CourseStateContext context)
    {
        throw new InvalidCourseOperationException("Курс вже архівований.");
    }

    public void RevertToDraft(CourseStateContext context)
    {
        context.SetState(new DraftState());
    }

    public bool CanAddLessons() => false;

    public bool CanModify() => false;
}
