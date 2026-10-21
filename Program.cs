using System;
using Microsoft.Data.SqlClient;

class Program {
    static string connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";
    static void Main() {
        bool exit = false;
        while (!exit) {
            Console.WriteLine("1. Показать все 2. Добавить 3. По ID 4. Выход");
            string choice = Console.ReadLine();
            switch (choice) {
                case "1": showAllProducts(); break;
                case "2": addProduct(); break;
                case "3": showProductById(); break;
                case "4": exit = true; break;
            }
        }
    }
    static void showAllProducts() {
        using var conn = new SqlConnection(connStr);
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Products", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) Console.WriteLine($"{reader["Id"]} - {reader["Name"]}");
    }
    static void addProduct() {
        Console.Write("Имя продукта: ");
        string name = Console.ReadLine();
        using var conn = new SqlConnection(connStr);
        conn.Open();
        using var cmd = new SqlCommand("INSERT INTO Products (Name) VALUES (@name)", conn);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.ExecuteNonQuery();
    }
    static void showProductById() {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        using var conn = new SqlConnection(connStr);
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read()) Console.WriteLine($"{reader["Id"]} - {reader["Name"]}");
    }
}
