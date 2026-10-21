<img width="427" height="298" alt="image" src="https://github.com/user-attachments/assets/764858fe-0a92-4f13-867b-db204c6181e3" />

**Models/Student.cs**
```csharp
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }

    public override string ToString() => $"Студент ID:{Id} - {FirstName} {LastName}";
}
```

**Data/StudentRepository.cs**
```csharp
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class StudentRepository
{
    private readonly string _connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    // Метод создания с возвратом нового ID через QuerySingle<int>
    public int CreateStudent(Student student)
    {
        using var conn = new SqlConnection(_connStr);
        
        // SQL запрос: вставляем данные и сразу выбираем новый ID (SCOPE_IDENTITY)
        string sql = @"INSERT INTO Students (FirstName, LastName, Age, GroupId) 
                       VALUES (@FirstName, @LastName, @Age, @GroupId); 
                       SELECT CAST(SCOPE_IDENTITY() as int);";
        
        // QuerySingle<int> вернет именно одно целое число (новый ID)
        int newId = conn.QuerySingle<int>(sql, student);
        
        return newId;
    }

    public IEnumerable<Student> GetAll()
    {
        using var conn = new SqlConnection(_connStr);
        return conn.Query<Student>("SELECT * FROM Students");
    }
}
```

**Program.cs**
```csharp
using System;

class Program
{
    static void Main()
    {
        var repo = new StudentRepository();

        Console.WriteLine("--- Создание студента ---");
        
        // Создаем объект модели
        var newStudent = new Student 
        { 
            FirstName = "Дмитрий", 
            LastName = "Дмитриев", 
            Age = 22, 
            GroupId = 1 
        };

        // Вызываем метод и получаем новый ID
        int generatedId = repo.CreateStudent(newStudent);
        
        Console.WriteLine($"Студент успешно создан! Присвоенный ID: {generatedId}");

        Console.WriteLine("\n--- Проверка списка всех студентов ---");
        foreach (var s in repo.GetAll())
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nНажми любую клавишу...");
        Console.ReadKey();
    }
}
```
