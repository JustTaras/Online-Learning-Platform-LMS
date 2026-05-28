namespace LmsPlatform.Domain;

/// <summary>
/// Розширюючі методи для курсів
/// </summary>
public static class CourseExtensions
{
    /// <summary>
    /// Отримує загальну кількість уроків для колекції курсів
    /// </summary>
    public static int GetTotalLessonsCount(this IEnumerable<Course> courses)
    {
        if (courses == null)
            throw new ArgumentNullException(nameof(courses));
        
        return courses.Sum(c => c.Lessons.Count);
    }
}
