<img width="418" height="339" alt="image" src="https://github.com/user-attachments/assets/d592e5d6-f967-4e9a-b9cd-553799cda4f8" />

**Models/Student.cs**
```csharp
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }

    public override string ToString() => $"Студент: {FirstName} {LastName}";
}
```

**Models/Group.cs**
```csharp
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString() => $"Группа: {Name}";
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

    public IEnumerable<Student> GetAll()
    {
        using var conn = new SqlConnection(_connStr);
        return conn.Query<Student>("SELECT * FROM Students");
    }

    public Student GetById(int id)
    {
        using var conn = new SqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Student>("SELECT * FROM Students WHERE Id = @Id", new { Id = id });
    }
    
    public void Add(Student s)
    {
        using var conn = new SqlConnection(_connStr);
        conn.Execute("INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES (@FirstName, @LastName, @Age, @GroupId)", s);
    }
}
```

**Data/GroupRepository.cs**
```csharp
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class GroupRepository
{
    private readonly string _connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public IEnumerable<Group> GetAll()
    {
        using var conn = new SqlConnection(_connStr);
        return conn.Query<Group>("SELECT * FROM Groups");
    }

    public Group GetById(int id)
    {
        using var conn = new SqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Group>("SELECT * FROM Groups WHERE Id = @Id", new { Id = id });
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
        var studentRepo = new StudentRepository();
        var groupRepo = new GroupRepository();

        Console.WriteLine("--- Студенты (через Dapper Query) ---");
        foreach (var s in studentRepo.GetAll())
            Console.WriteLine(s);

        Console.WriteLine("\n--- Группы (через Dapper Query) ---");
        foreach (var g in groupRepo.GetAll())
            Console.WriteLine(g);

        Console.WriteLine("\nНажми любую клавишу...");
        Console.ReadKey();
    }
}
```
