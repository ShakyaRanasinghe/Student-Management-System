-- ============================================
-- USERS TABLE
-- ============================================
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(150) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    [Password] VARCHAR(255) NOT NULL,
    [Role] VARCHAR(50) NOT NULL,   -- Admin, Student, Staff
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- ============================================
-- COURSES TABLE
-- ============================================
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    CourseName VARCHAR(150) NOT NULL,
    CourseCode VARCHAR(50) UNIQUE NOT NULL,
    Duration VARCHAR(50)
);

-- ============================================
-- STUDENTS TABLE
-- ============================================
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    FullName VARCHAR(150) NOT NULL,
    DateOfBirth DATE,
    Gender VARCHAR(20),
    Email VARCHAR(150),
    Phone VARCHAR(20),
    Address VARCHAR(255),
    CourseID INT,
    CourseCode VARCHAR(50),

    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- ============================================
-- STAFF TABLE
-- ============================================
CREATE TABLE Staff (
    StaffID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    FullName VARCHAR(150),
    Email VARCHAR(150),
    Phone VARCHAR(20),
    Department VARCHAR(100),

    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- ============================================
-- ATTENDANCE TABLE
-- ============================================
CREATE TABLE Attendance (
    AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    [Date] DATE NOT NULL,
    [Status] VARCHAR(20) CHECK ([Status] IN ('Present', 'Absent', 'Late')),

    FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

-- ============================================
-- MARKS TABLE
-- ============================================
CREATE TABLE Marks (
    MarksID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    Subject VARCHAR(100),
    MarksObtained INT,
    Grade VARCHAR(5),
    CourseCode VARCHAR(50),

    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
