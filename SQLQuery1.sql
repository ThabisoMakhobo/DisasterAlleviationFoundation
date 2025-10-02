CREATE DATABASE GiftOfTheGiversDB; -- =============================== -- Users Table -- =============================== 
CREATE TABLE Users ( 
UserID INT IDENTITY(1,1) PRIMARY KEY, 
    Name NVARCHAR(100) NOT NULL, 
    Email NVARCHAR(100) UNIQUE NOT NULL, 
    Role NVARCHAR(50) NOT NULL, 
    PasswordHash NVARCHAR(256) NOT NULL, 
    Phone NVARCHAR(20), 
    Skills NVARCHAR(200) NULL 
); 
 -- Volunteers Table 
CREATE TABLE Volunteers ( 
    VolunteerID INT IDENTITY(1,1) PRIMARY KEY, 
    UserID INT NOT NULL UNIQUE, 
    Availability NVARCHAR(100), 
    Location NVARCHAR(100), 
    HoursServed INT DEFAULT 0, 
    CONSTRAINT FK_Volunteers_Users FOREIGN KEY (UserID) 
REFERENCES Users(UserID) 
); 
 -- Crisis Table 
CREATE TABLE Crisis ( 
    CrisisID INT IDENTITY(1,1) PRIMARY KEY, 
    Type NVARCHAR(50) NOT NULL, 
    Location NVARCHAR(100) NOT NULL, 
    Date DATETIME NOT NULL DEFAULT GETDATE(), 
    Status NVARCHAR(50) NOT NULL 
); 
 -- Tasks Table 
CREATE TABLE Tasks ( 
    TaskID INT IDENTITY(1,1) PRIMARY KEY, 
    CrisisID INT NOT NULL, 
    VolunteerID INT NOT NULL, 
    Description NVARCHAR(200) NOT NULL, 
    Status NVARCHAR(50) NOT NULL, 
    CONSTRAINT FK_Tasks_Crisis FOREIGN KEY (CrisisID) 
REFERENCES Crisis(CrisisID), 
    CONSTRAINT FK_Tasks_Volunteers FOREIGN KEY 
(VolunteerID) REFERENCES Volunteers(VolunteerID) 
); 
 -- Resources Table 
CREATE TABLE Resources ( 
    ResourceID INT IDENTITY(1,1) PRIMARY KEY, 
    Name NVARCHAR(100) NOT NULL, 
    Quantity INT NOT NULL, 
    Location NVARCHAR(100) NOT NULL 
); 
 -- Donations Table 
CREATE TABLE Donations ( 
    DonationID INT IDENTITY(1,1) PRIMARY KEY, 
    DonorName NVARCHAR(100) NOT NULL, 
    Amount DECIMAL(10,2) NULL, 
    ResourceID INT NULL, 
    UserID INT NOT NULL, 
    Date DATETIME NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT FK_Donations_Users FOREIGN KEY (UserID) 
REFERENCES Users(UserID), 
    CONSTRAINT FK_Donations_Resources FOREIGN KEY 
(ResourceID) REFERENCES Resources(ResourceID) 
); 
 -- Distributions Table 
CREATE TABLE Distributions ( 
    DistributionID INT IDENTITY(1,1) PRIMARY KEY, 
    ResourceID INT NOT NULL, 
    CrisisID INT NOT NULL, 
    Quantity INT NOT NULL, 
Date DATETIME NOT NULL DEFAULT GETDATE(), 
CONSTRAINT FK_Distributions_Resources FOREIGN KEY 
(ResourceID) REFERENCES Resources(ResourceID), 
CONSTRAINT FK_Distributions_Crisis FOREIGN KEY (CrisisID) 
REFERENCES Crisis(CrisisID) 
);