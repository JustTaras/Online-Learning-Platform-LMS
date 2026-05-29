using System.Text.Json;
using System.Text.Json.Serialization;
using LmsPlatform.App.DTOs;
using LmsPlatform.App.Mapping;
using LmsPlatform.Domain;

namespace LmsPlatform.App.Persistence;

public class JsonPersistence
{
    private readonly JsonSerializerOptions _options;

    public JsonPersistence()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public void SaveCourses(IEnumerable<Course> courses, string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        var courseDtos = courses.Select(DtoMapper.ToCourseDto).ToList();
        var json = JsonSerializer.Serialize(courseDtos, _options);
        File.WriteAllText(filePath, json);
    }

    public List<Course> LoadCourses(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var json = File.ReadAllText(filePath);
        var courseDtos = JsonSerializer.Deserialize<List<CourseDto>>(json, _options)
            ?? new List<CourseDto>();

        return courseDtos.Select(DtoMapper.ToCourse).ToList();
    }

    public void SaveStudentProgress(IEnumerable<StudentProgress> progressList, string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        var progressDtos = progressList.Select(DtoMapper.ToStudentProgressDto).ToList();
        var json = JsonSerializer.Serialize(progressDtos, _options);
        File.WriteAllText(filePath, json);
    }

    public List<StudentProgress> LoadStudentProgress(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var json = File.ReadAllText(filePath);
        var progressDtos = JsonSerializer.Deserialize<List<StudentProgressDto>>(json, _options)
            ?? new List<StudentProgressDto>();

        return progressDtos.Select(DtoMapper.ToStudentProgress).ToList();
    }
}
