#  Student Management System

### *C# WinForms + SQL Server Desktop Application*

This project is a complete **Student Management System** built using **C# (.NET WinForms)** and **SQL Server**.
It was developed as part of a **certificate course project** and demonstrates real-world system design, including role-based authentication, attendance tracking, marks management, CRUD operations, and reporting.

---

##  Key Features

###  **Role-Based Login System**

Users can log in with Email + Password and are directed to dashboards:

#### **Admin Dashboard**

* Manage Students
* Manage Staff
* Manage Courses
* Attendance Management
* Marks Management
* Reports
* Change Password
* Logout

#### **Staff Dashboard**

* Record Attendance
* Enter Marks
* Manage Students
* Generate Reports
* Logout

#### **Student Dashboard**

* View Profile
* View Attendance
* View Marks
* Logout

---

##  Student Features

### ✔ *Profile Page*

Shows student’s:

* Name
* Email
* DOB
* Gender
* Phone
* Address
* Course
* Course Code

### ✔ *Student Attendance Viewer*

Students can view:

* Attendance history
* Present/Absent status by date

### ✔ *Student Marks Viewer*

Students can view:

* Subjects
* Marks Obtained
* Grades
* Course & Course Code filter options

---

##  Staff Features

### ✔ Attendance Management

* Load students by Course + Course Code
* Mark Present or Absent
* Save/update attendance
* Mark All Present shortcut

### ✔ Marks Management

* Enter marks per student
* Auto-grade calculation
* Save marks to SQL database
* Clear/reset form

---

##  Admin Features

### ✔ Student CRUD

* Add (auto-creates user account)
* Edit student details
* Delete (clears attendance, marks, user record)
* Search students

### ✔ Staff CRUD

* Add staff (auto-creates user account)
* Edit staff information
* Delete staff
* Search staff

### ✔ Course Management

* Add, edit, delete courses
* Search course by name/code/duration

### ✔ Reporting System

Admins & staff can generate attendance reports filtered by:

* Course
* Course Code
* Date range

---

#  Database Schema

### **Users**

* `UserID` (PK)
* `FullName`
* `Email`
* `Password`
* `Role` — Admin, Staff, Student
* `CreatedDate`

### **Students**

* Linked to `Users.UserID`
* Contains full student details
* Course enrollment info

### **Staff**

* Linked to `Users.UserID`
* Contains staff details

### **Courses**

* Course Name
* Course Code
* Duration

### **Attendance**

* StudentID
* Date
* Status (Present/Absent)

### **Marks**

* StudentID
* CourseID
* Course Code
* Subject
* Marks Obtained
* Grade

---

#  Project Structure

```
Student Management System/
│── Program.cs
│── DBConnection.cs
│
├── Authentication/
│   ├── frmHome.cs
│   ├── frmLogin.cs
│   ├── frmChangePassword.cs
│
├── Dashboards/
│   ├── frmAdminDashboard.cs
│   ├── frmStaffDashboard.cs
│   ├── frmStudentDashboard.cs
│
├── Management/
│   ├── frmManageStudents.cs
│   ├── frmManageStaff.cs
│   ├── frmManageCourses.cs
│
├── Modules/
│   ├── frmAttendance.cs
│   ├── frmMarks.cs
│   ├── frmReports.cs
│   ├── frmProfile.cs
│   ├── frmStudentAttendance.cs
│   ├── frmStudentMarks.cs
│
└── UI Resources (.Designer.cs, .resx)
```

---

# ⚙️ Installation Guide

### Clone the Repository

```bash
git clone https://github.com/yourusername/StudentManagementSystem.git
```

###  Create & Import SQL Database

```sql
CREATE DATABASE StudentManagementSystem;
```

Then run table creation scripts.

###  Configure Connection String

Update **App.config**:

```xml
<connectionStrings>
  <add name="DBConnection"
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=StudentManagementSystem;Integrated Security=True"/>
</connectionStrings>
```

### Run the Application

Open project in Visual Studio → Press **F5**

---

# Auto Grade Calculation

Grades are assigned automatically based on marks:

```
A = 75+
B = 65–74
C = 55–64
D = 45–54
F = below 45
```

---

# 🎯 Learning Outcomes

Through building this project, I gained experience in:

* C# WinForms desktop development
* SQL Server database design
* ADO.NET for SQL operations
* CRUD operations
* DataGridView binding
* Multi-role authentication
* Exception handling & validations
* Designing modular UI systems

---

##Contribution - GACSE252F-001
