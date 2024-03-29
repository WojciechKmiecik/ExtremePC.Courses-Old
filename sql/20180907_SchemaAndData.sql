USE [master]
GO
/****** Object:  Database [ExtremePC.CoursesDb]    Script Date: 2018-09-07 02:36:56 AM ******/
CREATE DATABASE [ExtremePC.CoursesDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ExtremePC.CoursesDb', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExtremePC.CoursesDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ExtremePC.CoursesDb_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExtremePC.CoursesDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExtremePC.CoursesDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET  MULTI_USER 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET QUERY_STORE = OFF
GO
USE [ExtremePC.CoursesDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ExtremePC.CoursesDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2018-09-07 02:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2018-09-07 02:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](max) NULL,
	[MaxCapacity] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseStudents]    Script Date: 2018-09-07 02:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseStudents](
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_CourseStudents] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2018-09-07 02:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Age] [tinyint] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 2018-09-07 02:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180902223947_InitialCreate', N'2.1.2-rtm-30932')
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 
GO
INSERT [dbo].[Courses] ([CourseId], [Topic], [MaxCapacity], [TeacherId]) VALUES (1, N'quantum physics', 20, 1)
GO
INSERT [dbo].[Courses] ([CourseId], [Topic], [MaxCapacity], [TeacherId]) VALUES (2, N'''Your Brutto salary will become netto'' - and other creative tax ideas', 5, 3)
GO
INSERT [dbo].[Courses] ([CourseId], [Topic], [MaxCapacity], [TeacherId]) VALUES (3, N'PHP Programmer as a example of different faith', 10, 2)
GO
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (1, 1)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (1, 2)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (1, 3)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (2, 4)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (2, 5)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (2, 6)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (3, 7)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (3, 8)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (3, 9)
GO
INSERT [dbo].[CourseStudents] ([CourseId], [StudentId]) VALUES (1, 10)
GO
SET IDENTITY_INSERT [dbo].[Students] ON 
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (1, N'Dzong Un', N'Kim', 34)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (2, N'Joseph', N'Stalin', 140)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (3, N'James', N'Bond', 65)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (4, N'Mariusz', N'Stokowy', 44)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (5, N'Frank', N'Abagnale', 72)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (6, N'Jacek', N'Silber', 86)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (7, N'Max', N'Małecki', 30)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (8, N'Marek', N'Karmelski', 30)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (9, N'Dawid', N'Lorek', 28)
GO
INSERT [dbo].[Students] ([StudentId], [FirstName], [LastName], [Age]) VALUES (10, N'Andrzej', N'Kmiecik', 48)
GO
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON 
GO
INSERT [dbo].[Teachers] ([TeacherId], [FirstName], [LastName]) VALUES (1, N'Albert', N'Einstein')
GO
INSERT [dbo].[Teachers] ([TeacherId], [FirstName], [LastName]) VALUES (2, N'Mark', N'Zuckerberg')
GO
INSERT [dbo].[Teachers] ([TeacherId], [FirstName], [LastName]) VALUES (3, N'Wojciech', N'Kmiecik')
GO
SET IDENTITY_INSERT [dbo].[Teachers] OFF
GO
/****** Object:  Index [IX_Courses_TeacherId]    Script Date: 2018-09-07 02:36:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_Courses_TeacherId] ON [dbo].[Courses]
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CourseStudents_StudentId]    Script Date: 2018-09-07 02:36:56 AM ******/
CREATE NONCLUSTERED INDEX [IX_CourseStudents_StudentId] ON [dbo].[CourseStudents]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Teachers_TeacherId] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([TeacherId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Teachers_TeacherId]
GO
ALTER TABLE [dbo].[CourseStudents]  WITH CHECK ADD  CONSTRAINT [FK_CourseStudents_Courses_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseStudents] CHECK CONSTRAINT [FK_CourseStudents_Courses_CourseId]
GO
ALTER TABLE [dbo].[CourseStudents]  WITH CHECK ADD  CONSTRAINT [FK_CourseStudents_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseStudents] CHECK CONSTRAINT [FK_CourseStudents_Students_StudentId]
GO
USE [master]
GO
ALTER DATABASE [ExtremePC.CoursesDb] SET  READ_WRITE 
GO
