CREATE TABLE [dbo].[Customer]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Cpf] NVARCHAR(14) NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [RG] NVARCHAR(15) NOT NULL, 
    [DispatchOrgan] NVARCHAR(10) NOT NULL, 
    [DispatchDate] DATE NOT NULL, 
    [DispatchState] NVARCHAR(2) NOT NULL, 
    [Gender] CHAR(1) NOT NULL, 
    [MaritalStatus] NVARCHAR(20) NOT NULL, 
    [AddressId] INT NOT NULL, 
    [CreatedBy] NVARCHAR(10) NOT NULL, 
    [CreatedAt] DATE NOT NULL, 
    [ModifiedBy] NVARCHAR(10) NOT NULL, 
    [ModifiedAt] DATE NOT NULL
)
