<img width="515" height="108" alt="image" src="https://github.com/user-attachments/assets/57e660d2-f6fb-422c-bd91-b30156915156" />

```
import pyodbc
conn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=CollegeDB;Trusted_Connection=yes;')
cursor = conn.cursor()
cursor.execute("SELECT * FROM Students")
for row in cursor:
    print(f"ID: {row.Id}, Имя: {row.FirstName}")
cursor.close()
conn.close()
```
