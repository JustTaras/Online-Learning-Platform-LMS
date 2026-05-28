using LmsPlatform.Domain;
using LmsPlatform.App;

// Точка входу застосунку LMS
Console.WriteLine("Ласкаво просимо в платформу LMS");
Console.WriteLine("=================================\n");

// Приклад використання Repository
Console.WriteLine("--- Repository<T> Демонстрація ---");
var courseRepository = new Repository<Course>();
var course1 = new Course("Основи C#", "Навчання основам мови програмування C#");
var course2 = new Course("Асинхронне програмування", "Робота з async/await");
courseRepository.Add(course1);
courseRepository.Add(course2);
Console.WriteLine($"Курсів у репозиторію: {courseRepository.GetAll().Count}\n");

// Приклад використання CourseManager
Console.WriteLine("--- CourseManager Демонстрація ---");
var manager = new CourseManager();
var lesson1 = new Lesson("Вступ до C#", "Базові поняття мови C#", 1);
var lesson2 = new Lesson("Змінні та типи", "Робота зі змінними та типами даних", 2);
var lesson3 = new Lesson("Цикли", "Робота з циклами", 3);

course1 += lesson1;
course1 += lesson2;
course1 += lesson3;

course2 += new Lesson("Tasks та Threads", "Паралельне програмування", 1);

manager.AddCourse(course1);
manager.AddCourse(course2);

Console.WriteLine($"Курс: {course1.Title}");
Console.WriteLine($"Кількість уроків: {course1.Lessons.Count}");
Console.WriteLine($"Курс валідний: {course1.Validate()}\n");

// Демонстрація Extension Method
Console.WriteLine("--- Extension Method GetTotalLessonsCount ---");
var allCourses = manager.GetAllCourses();
int totalLessons = allCourses.GetTotalLessonsCount();
Console.WriteLine($"Загальна кількість уроків у всіх курсах: {totalLessons}\n");

// Демонстрація LINQ запитів
Console.WriteLine("--- LINQ Запит 1: GetLessonsByCourseDifficulty ---");
var difficultyCourses = manager.GetLessonsByCourseDifficulty();
foreach (var (course, lessonCount, difficulty) in difficultyCourses)
{
    Console.WriteLine($"Курс: {course.Title}, Уроків: {lessonCount}, Складність: {difficulty}");
}
Console.WriteLine();

// Демонстрація другого LINQ запиту
Console.WriteLine("--- LINQ Запит 2: GetStudentProgressAggregate ---");
var student1Progress = new StudentProgress(Guid.NewGuid(), course1.Id, 3);
var student2Progress = new StudentProgress(Guid.NewGuid(), course1.Id, 3);
var student3Progress = new StudentProgress(Guid.NewGuid(), course2.Id, 1);

student1Progress.UpdateProgress(2);
student2Progress.UpdateProgress(3);
student3Progress.UpdateProgress(1);

var progressList = new List<StudentProgress> { student1Progress, student2Progress, student3Progress };
var aggregatedProgress = manager.GetStudentProgressAggregate(progressList);

foreach (var (courseId, totalStudents, avgProgress, maxCompleted) in aggregatedProgress)
{
    var courseTitle = manager.GetCourseById(courseId).Title;
    Console.WriteLine($"Курс: {courseTitle}");
    Console.WriteLine($"  Студентів: {totalStudents}, Середній прогрес: {avgProgress:F2}%, Макс завершено: {maxCompleted}");
}
Console.WriteLine();

// Демонстрація SimulationHelper з Retry Policy
Console.WriteLine("--- SimulationHelper з Retry Policy ---");
try
{
    SimulationHelper.SimulateDbConnect();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Помилка після всіх спроб: {ex.Message}");
}
