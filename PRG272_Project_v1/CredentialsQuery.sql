CREATE DATABASE StudentAdministration

USE StudentAdministration
GO

CREATE TABLE Users
(UserID INT IDENTITY(100,1) PRIMARY KEY,
Username varchar(50) NOT NULL,
Password varchar(50) NOT NULL
);