namespace LmsPlatform.Domain.Factory;

public abstract class CourseFactory
{
    public abstract Course CreateCourse();

    public static BeginnerCourseFactory CreateBeginnerCourseFactory() => new();
    public static IntermediateCourseFactory CreateIntermediateCourseFactory() => new();
    public static AdvancedCourseFactory CreateAdvancedCourseFactory() => new();
}

public class BeginnerCourseFactory : CourseFactory
{
    public override Course CreateCourse()
    {
        var course = new Course(
            "Вступний курс",
            "Базовий курс для новачків"
        );

        course.AddLesson(new Lesson("Введення", "Основні концепції", 1));
        course.AddLesson(new Lesson("Перші кроки", "Практичні основи", 2));

        return course;
    }
}

public class IntermediateCourseFactory : CourseFactory
{
    public override Course CreateCourse()
    {
        var course = new Course(
            "Проміжний курс",
            "Курс для учнів з базовими знаннями"
        );

        course.AddLesson(new Lesson("Поглиблене вивчення", "Складніші концепції", 1));
        course.AddLesson(new Lesson("Практичні навички", "Прикладні завдання", 2));
        course.AddLesson(new Lesson("Проекти", "Реальні проекти", 3));

        return course;
    }
}

public class AdvancedCourseFactory : CourseFactory
{
    public override Course CreateCourse()
    {
        var course = new Course(
            "Продвинутий курс",
            "Курс для експертів та спеціалістів"
        );

        course.AddLesson(new Lesson("Експертні знання", "Передові техніки", 1));
        course.AddLesson(new Lesson("Архітектура систем", "Системний дизайн", 2));
        course.AddLesson(new Lesson("Оптимізація", "Продвинута оптимізація", 3));
        course.AddLesson(new Lesson("Дослідження", "Новітні дослідження", 4));

        return course;
    }
}
