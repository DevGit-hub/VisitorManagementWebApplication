CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50),
    Password NVARCHAR(50),
    Role NVARCHAR(20)
);

INSERT INTO Users (Username, Password, Role)
VALUES ('admin', 'admin123', 'Admin');