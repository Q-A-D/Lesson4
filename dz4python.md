<img width="296" height="76" alt="image" src="https://github.com/user-attachments/assets/e250c0ff-04ad-4c43-9065-6514ea8da53e" />

```
import pymssql
conn = pymssql.connect(server='localhost', database='CollegeDB', user='sa', password='123')
cursor = conn.cursor()
cursor.execute('SELECT * FROM Students')
for row in cursor:
    print(f"ID: {row[0]}, Имя: {row[1]}")
conn.close()
```
