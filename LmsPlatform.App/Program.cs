using LmsPlatform.Domain;

// Точка входу застосунку LMS
Console.WriteLine("Ласкаво просимо в платформу LMS");

// Приклад використання
var course = new Course("Основи C#", "Навчання основам мови програмування C#");
var lesson1 = new Lesson("Вступ до C#", "Базові поняття мови C#", 1);
var lesson2 = new Lesson("Змінні та типи", "Робота зі змінними та типами даних", 2);

course += lesson1;
course += lesson2;

Console.WriteLine($"Курс: {course.Title}");
Console.WriteLine($"Кількість уроків: {course.Lessons.Count}");
Console.WriteLine($"Курс валідний: {course.Validate()}");
