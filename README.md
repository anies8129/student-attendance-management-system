📘 College Attendance Management System
Built with ASP.NET Core MVC + SQL Server

This project is a Role-Based Attendance Management System that supports:

Admin Module

Faculty Module

Student Module (view only)
🧑‍💼 Admin Module
✔ Authentication

Admin can log in using shared login system.

✔ Dashboard

Displays:

Admin Name

Quick Links to Manage Modules

✔ Manage Faculty

Add Faculty

Edit Faculty

Delete Faculty

View All Faculty

✔ Manage Students

Add Student

Edit Student

Delete Student

View All Students

✔ Attendance Overview

View all attendance records

Filter by:

Student

Faculty

Department

Date

✔ Admin Profile

View Profile

Update Profile

👨‍🏫 Faculty Module
✔ Login

Faculty logs in through shared login page.

✔ Dashboard

Displays:

Faculty Name

Actions available

✔ Mark Attendance

Faculty can:

View list of assigned students (by department)

Mark each student Present or Absent

✔ View Attendance

Faculty can view:

Attendance records they marked

✔ Profile

View Profile

Edit Profile

🧾 Database Structure
Tables:

admintable

facultytable

studenttable

login

attendancetable

Important Notes:

IDs are not auto-incremented
(AdminId, FacultyId, StudentId generated manually using MAX + 1)

Login table stores:

roleid (AdminId/FacultyId)

logtype ("admin" or "faculty")

🛠 Technologies Used
Technology	Description
ASP.NET Core MVC	Main Web Framework
SQL Server	Database
ADO.NET	Database Connectivity
Session	User Authentication
HTML/CSS/Bootstrap	UI Design
