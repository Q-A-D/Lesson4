<img width="1113" height="626" alt="image" src="https://github.com/user-attachments/assets/9549586c-7fed-4aae-a9b5-4b747ca70a48" />

```
using System;
using System.Data.SqlClient;
class Program {
    static void Main() {
        string conn = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";
        using (SqlConnection c = new SqlConnection(conn)) {
            c.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", c)) {
                using (SqlDataReader r = cmd.ExecuteReader()) {
                    while (r.Read()) Console.WriteLine($"ID: {r["Id"]}, Имя: {r["FirstName"]}");
                }
            }
        }
    }
}
```
