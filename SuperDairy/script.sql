USE [master]
GO
/****** Object:  Database [SuperDairy]    Script Date: 9/11/2022 7:46:11 PM ******/
CREATE DATABASE [SuperDairy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SuperDairy', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SuperDairy.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SuperDairy_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SuperDairy_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SuperDairy] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SuperDairy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SuperDairy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SuperDairy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SuperDairy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SuperDairy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SuperDairy] SET ARITHABORT OFF 
GO
ALTER DATABASE [SuperDairy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SuperDairy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SuperDairy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SuperDairy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SuperDairy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SuperDairy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SuperDairy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SuperDairy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SuperDairy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SuperDairy] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SuperDairy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SuperDairy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SuperDairy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SuperDairy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SuperDairy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SuperDairy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SuperDairy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SuperDairy] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SuperDairy] SET  MULTI_USER 
GO
ALTER DATABASE [SuperDairy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SuperDairy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SuperDairy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SuperDairy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SuperDairy] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SuperDairy] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SuperDairy] SET QUERY_STORE = OFF
GO
USE [SuperDairy]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [uniqueidentifier] NOT NULL,
	[LandMark] [varchar](255) NULL,
	[Street] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[Country] [varchar](255) NULL,
	[ZipCode] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Quantity] [float] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[IsPaid] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[LastModified] [datetime] NULL,
	[LastModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DairyInfo]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DairyInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](255) NULL,
	[OwnerName] [varchar](50) NULL,
	[ConnectionString] [varchar](255) NULL,
	[LastModified] [datetime] NULL,
	[PhoneNo] [varchar](13) NULL,
	[Email] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MilkInventory]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MilkInventory](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Batch] [int] NOT NULL,
	[MilkType] [int] NOT NULL,
	[Fat] [float] NOT NULL,
	[Price] [float] NULL,
	[Quantity] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[Comment] [varchar](255) NULL,
	[Status] [int] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rate]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MilkType] [int] NOT NULL,
	[Fat] [float] NULL,
	[Price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[ContactNo] [varchar](20) NULL,
	[Password] [varchar](255) NULL,
	[AddressId] [uniqueidentifier] NULL,
	[Role] [int] NULL,
	[Created] [datetime] NULL,
	[LastModified] [datetime] NULL,
	[IsActive] [bit] NULL,
UNIQUE NONCLUSTERED 
(
	[ContactNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0.0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[MilkInventory] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[MilkInventory] ADD  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[MilkInventory] ADD  DEFAULT ((4.0)) FOR [Fat]
GO
ALTER TABLE [dbo].[MilkInventory] ADD  DEFAULT ((0.0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[MilkInventory] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Rate] ADD  DEFAULT ((0.0)) FOR [Price]
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 9/11/2022 7:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_login]

(
        @Username varchar(50),
        @Password varchar(50),
		@UserId uniqueidentifier OUTPUT,
		@Role int OUTPUT,
		@Name varchar(50) OUTPUT
		
)
as
declare @status int
if exists(select * from Users where UserName=@Username and Password=@Password)
 
	select @status=1 ,@UserId=ID,@Role=Role,@Name=Name from Users where UserName=@Username and Password=@Password
       
else
       select @status=0
GO
USE [master]
GO
ALTER DATABASE [SuperDairy] SET  READ_WRITE 
GO
