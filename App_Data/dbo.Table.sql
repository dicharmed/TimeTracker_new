CREATE TABLE [dbo].[Employee]
(
	[Id_employee] INT NOT NULL PRIMARY KEY, 
    [login] NCHAR(10) NOT NULL, 
    [password] NVARCHAR(50) NOT NULL, 
    [first_name] NVARCHAR(50) NOT NULL, 
    [last_name] NVARCHAR(50) NOT NULL, 
    [role] NCHAR(10) NOT NULL, 
    [position] NCHAR(10) NOT NULL
)
