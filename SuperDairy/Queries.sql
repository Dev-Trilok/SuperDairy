CREATE TABLE Users (
    ID int IDENTITY(100,1),
    Name varchar(255),
    ContactNo varchar(20) UNIQUE,
	Password varchar(255),
    AddressId uniqueidentifier,
	Role int,
    Created datetime,
    LastModified datetime,
    IsActive bit
);



CREATE TABLE Address (
	ID uniqueidentifier DEFAULT NEWID() primary key,
	LandMark varchar(255),
	Street varchar(255),
	City varchar(255),
	State varchar(255),
	Country varchar(255),
	ZipCode varchar(255)
);

CREATE TABLE MilkInventory(
	Id uniqueidentifier DEFAULT NEWID() primary key,
	UserId int NOT NULL,
	Date DateTime NOT NULL DEFAULT getdate(),
	Batch int NOT NULL,
	MilkType int NOT NULL,
	Fat float NOT NULL DEFAULT 4.0,
	Price float,
	Quantity float NOT NULL DEFAULT 0.0,
	Amount float NOT NULL,
	Comment varchar(255),
	Status int,
	CreatedOn datetime NOT NULL DEFAULT getdate(),
	CreatedBy int,
	UpdatedOn datetime NULL,
	UpdatedBy int NULL,
);


CREATE TABLE Rate(
	Id int IDENTITY(1,1) primary key,
	MilkType int NOT NULL,
	Fat float,
	Price float NOT NULL DEFAULT 0.0,
);


CREATE TABLE Bill(
	Id uniqueidentifier DEFAULT NEWID() primary key,
	UserId int NOT NULL,
	StartDate DateTime NOT NULL,
	EndDate DateTime NOT NULL,
	MilkType int NOT NULL,
	Quantity float NOT NULL DEFAULT 0.0,
	TotalAmount float NOT NULL,
	IsPaid bit,
	CreatedOn datetime NOT NULL DEFAULT getdate(),
	CreatedBy int,
	LastModified datetime NULL,
	LastModifiedBy int NULL,
);
