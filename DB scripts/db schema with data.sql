USE [master]
GO
/****** Object:  Database [MemberManagementSystem]    Script Date: 07/03/2021 7:20:12 pm ******/
CREATE DATABASE [MemberManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MemberManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MemberManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MemberManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MemberManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MemberManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MemberManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MemberManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MemberManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MemberManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET TRUSTWORTHY ON 
GO
ALTER DATABASE [MemberManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MemberManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [MemberManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [MemberManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MemberManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MemberManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MemberManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MemberManagementSystem', N'ON'
GO
USE [MemberManagementSystem]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 07/03/2021 7:20:13 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[MemberId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[Balance] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_CompaniesAccounts] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC,
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comapnies]    Script Date: 07/03/2021 7:20:13 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comapnies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Comapnies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 07/03/2021 7:20:13 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/03/2021 7:20:13 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[LoginName] [nvarchar](200) NOT NULL,
	[LoginPassword] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (14, 5, 10, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (14, 6, 150, 1)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (15, 6, 88, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (15, 7, 19, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (16, 5, 20, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (16, 6, 17, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (16, 7, 0, 0)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (17, 5, 188, 1)
INSERT [dbo].[Accounts] ([MemberId], [CompanyId], [Balance], [Status]) VALUES (17, 7, 10, 0)
GO
SET IDENTITY_INSERT [dbo].[Comapnies] ON 

INSERT [dbo].[Comapnies] ([Id], [Name]) VALUES (5, N'Burger King')
INSERT [dbo].[Comapnies] ([Id], [Name]) VALUES (6, N'Fitness First')
INSERT [dbo].[Comapnies] ([Id], [Name]) VALUES (7, N'Lufthansa')
SET IDENTITY_INSERT [dbo].[Comapnies] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([Id], [Name], [Address], [UserId]) VALUES (14, N'Anakin Skywalker', N'Landsberger Straße 110', 1)
INSERT [dbo].[Members] ([Id], [Name], [Address], [UserId]) VALUES (15, N'Darth Vader', N'Landsberger Straße 112', 1)
INSERT [dbo].[Members] ([Id], [Name], [Address], [UserId]) VALUES (16, N'Obi-Wan Kenobi', N'Landsberger Straße 114', 1)
INSERT [dbo].[Members] ([Id], [Name], [Address], [UserId]) VALUES (17, N'Yoda', N'Landsberger Straße 114', 1)
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Email], [LoginName], [LoginPassword]) VALUES (1, N'Qais', N'qais-mail@email.com', N'loginN', N'loginP')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_CompaniesAccounts_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Comapnies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Comapnies] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Comapnies]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Members]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Users]
GO
USE [master]
GO
ALTER DATABASE [MemberManagementSystem] SET  READ_WRITE 
GO
