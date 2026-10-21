<img width="261" height="81" alt="cmd1" src="https://github.com/user-attachments/assets/d03172bc-8d1f-451b-a86f-f02766c64ab0" />

```
using Microsoft.Data.SqlClient;
class Program {
    static string conn = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";
    static void Main() {
        AddGroup("Группа-101");
        AddStudent("Иван", "Иванов", "20", "1");
    }
    static void AddGroup(string name) {
        using var c = new SqlConnection(conn); c.Open();
        using var cmd = new SqlCommand("INSERT INTO Groups (Name) VALUES (@n)", c);
        cmd.Parameters.AddWithValue("@n", name); cmd.ExecuteNonQuery();
    }
    static void AddStudent(string f, string l, string a, string g) {
        using var c = new SqlConnection(conn); c.Open();
        using var cmd = new SqlCommand("INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES (@f,@l,@a,@g)", c);
        cmd.Parameters.AddWithValue("@f", f); cmd.Parameters.AddWithValue("@l", l);
        cmd.Parameters.AddWithValue("@a", a); cmd.Parameters.AddWithValue("@g", g);
        cmd.ExecuteNonQuery();
    }
}
```
