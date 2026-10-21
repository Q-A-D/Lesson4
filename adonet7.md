<img width="467" height="336" alt="image" src="https://github.com/user-attachments/assets/144a1b28-b52c-46d9-afa2-f59a34fb606d" />

**Models/Student.cs**
```csharp
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }
    
    public override string ToString()
    {
        return $"Студент: {FirstName} {LastName}, Возраст: {Age}, Группа: {GroupId}";
    }
}
```

**Data/StudentRepository.cs**
```csharp
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class StudentRepository
{
    private readonly string _connectionString;
    
    public StudentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES (@FirstName, @LastName, @Age, @GroupId)", student);
    }
    
    public IEnumerable<Student> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Student>("SELECT * FROM Students");
    }
    
    public Student GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Student>("SELECT * FROM Students WHERE Id = @Id", new { Id = id });
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
        string connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";
        var repo = new StudentRepository(connStr);
        
        Console.WriteLine("--- Все студенты ---");
        var students = repo.GetAll();
        foreach (var s in students)
        {
            Console.WriteLine(s.ToString());
        }
        
        Console.WriteLine("\n--- Добавление студента ---");
        repo.Add(new Student { FirstName = "Тест", LastName = "Тестов", Age = 25, GroupId = 1 });
        Console.WriteLine("Студент добавлен!");
        
        Console.WriteLine("\nНажми любую клавишу...");
        Console.ReadKey();
    }
}
```
