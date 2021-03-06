USE [master]
GO
/****** Object:  Database [MemberManagementSystem]    Script Date: 07/03/2021 7:17:56 pm ******/
CREATE DATABASE [MemberManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MemberManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MemberManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MemberManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MemberManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MemberManagementSystem] SET COMPATIBILITY_LEVEL = 150
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
ALTER DATABASE [MemberManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MemberManagementSystem', N'ON'
GO
ALTER DATABASE [MemberManagementSystem] SET QUERY_STORE = OFF
GO
USE [MemberManagementSystem]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 07/03/2021 7:17:56 pm ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comapnies]    Script Date: 07/03/2021 7:17:56 pm ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 07/03/2021 7:17:56 pm ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/03/2021 7:17:56 pm ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
