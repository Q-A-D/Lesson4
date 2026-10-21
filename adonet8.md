<img width="258" height="249" alt="image" src="https://github.com/user-attachments/assets/b9641331-2d8b-48d0-86d2-17e62a8c0ce1" />

**Models/Group.cs**
```csharp
public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public override string ToString()
    {
        return $"Группа: {Name} (ID: {Id})";
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
    private readonly string _connectionString;
    
    public GroupRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(Group group)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("INSERT INTO Groups (Name) VALUES (@Name)", group);
    }
    
    public IEnumerable<Group> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Group>("SELECT * FROM Groups");
    }
    
    public Group GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Group>("SELECT * FROM Groups WHERE Id = @Id", new { Id = id });
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
        var repo = new GroupRepository(connStr);
        
        Console.WriteLine("--- Все группы ---");
        var groups = repo.GetAll();
        foreach (var g in groups)
        {
            Console.WriteLine(g.ToString());
        }
        
        Console.WriteLine("\n--- Добавление группы ---");
        repo.Add(new Group { Name = "Новая Группа-99" });
        Console.WriteLine("Группа добавлена!");
        
        Console.WriteLine("\nНажми любую клавишу...");
        Console.ReadKey();
    }
}
```
