<img width="319" height="145" alt="cmd1" src="https://github.com/user-attachments/assets/81747e60-3557-4583-8769-b78dd13c3031" />

```
using System;
using Microsoft.Data.SqlClient;
class Program {
    static string conn = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";
    static void Main() {
        ShowAllStudents();
        Console.Write("Введите имя для поиска: ");
        FindStudentByName(Console.ReadLine());
    }
    static void ShowAllStudents() {
        using var c = new SqlConnection(conn); c.Open();
        using var cmd = new SqlCommand("SELECT * FROM Students", c);
        using var r = cmd.ExecuteReader();
        while(r.Read()) Console.WriteLine($"{r["Id"]} {r["FirstName"]}");
    }
    static void FindStudentByName(string name) {
        using var c = new SqlConnection(conn); c.Open();
        using var cmd = new SqlCommand("SELECT * FROM Students WHERE FirstName LIKE @n", c);
        cmd.Parameters.AddWithValue("@n", "%" + name + "%");
        using var r = cmd.ExecuteReader();
        while(r.Read()) Console.WriteLine($"Найден: {r["Id"]} {r["FirstName"]}");
    }
}
```
