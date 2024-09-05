CREATE TABLE [dbo].[Address]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [ZipCode] NVARCHAR(10) NOT NULL, 
    [Street] NVARCHAR(255) NOT NULL, 
    [StreetNumber] NVARCHAR(10) NOT NULL, 
    [Complement] NVARCHAR(100) NULL, 
    [Neighborhood] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(100) NOT NULL, 
    [State] NVARCHAR(2) NULL,
	[CreatedBy] NVARCHAR(10) NOT NULL, 
    [CreatedAt] DATE NOT NULL, 
    [ModifiedBy] NVARCHAR(10) NOT NULL, 
    [ModifiedAt] DATE NOT NULL
)
