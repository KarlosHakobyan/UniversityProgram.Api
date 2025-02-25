using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UniversityProgram.Domain.Entities;

namespace UniversityProgram.LocalData;
public class JsonDataService : IJsonDataService
{
    const string filePath = "./students.json";
    private readonly List<StudentBase> Students = new List<StudentBase>();

    public void Add(StudentBase student)
    {
        var students = ReadDataAsync<StudentBase>().Result;
        var maxId = students.Max(e => e.Id);
        student.Id = maxId + 1;
        Students.Add(student);
    }

    public async Task<IEnumerable<StudentBase>> GetAllStudents()
    {
        var students = await ReadDataAsync<StudentBase>();
        return students;
    }

    private async Task WriteDataAsync<T>()
    {
        var students = ReadDataAsync<StudentBase>();
        Students.AddRange(await students);
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        using (StreamWriter streamWriter = new StreamWriter(filePath))
        {
            string json = JsonSerializer.Serialize(Students);
            await streamWriter.WriteAsync(json);
        };

    }
    private static async Task <IEnumerable<T>> ReadDataAsync<T>()
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }
        using (StreamReader streamReader = new StreamReader(filePath))
        {
            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize <List<T>>(json);
        };

    }

    public async Task SaveChangesAsync()
    {
        await WriteDataAsync<StudentBase>();
    }

}
