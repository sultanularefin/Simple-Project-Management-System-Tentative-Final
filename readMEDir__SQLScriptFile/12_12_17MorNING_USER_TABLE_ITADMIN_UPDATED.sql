USE [master]
GO
/****** Object:  Database [ProjMngmntsystm]    Script Date: 12/12/2017 9:15:58 AM ******/
CREATE DATABASE [ProjMngmntsystm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjMngmntsystm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ProjMngmntsystm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjMngmntsystm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ProjMngmntsystm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProjMngmntsystm] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjMngmntsystm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjMngmntsystm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjMngmntsystm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjMngmntsystm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjMngmntsystm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjMngmntsystm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjMngmntsystm] SET  MULTI_USER 
GO
ALTER DATABASE [ProjMngmntsystm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjMngmntsystm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjMngmntsystm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjMngmntsystm] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjMngmntsystm] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjMngmntsystm] SET QUERY_STORE = OFF
GO
USE [ProjMngmntsystm]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ProjMngmntsystm]
GO
/****** Object:  Table [dbo].[AllFiles]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllFiles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[projectId] [int] NOT NULL,
	[fname] [nvarchar](200) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[projectId] [int] NOT NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taskId] [int] NOT NULL,
	[comment] [text] NOT NULL,
	[commentDate] [datetime] NOT NULL,
	[commenterid] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Designations]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priorities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[priorityName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](300) NOT NULL,
	[codeName] [nvarchar](200) NOT NULL,
	[description] [text] NOT NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NOT NULL,
	[duration] [int] NOT NULL,
	[filesId] [int] NULL,
	[statusId] [int] NULL,
	[shortName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectStatuses]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStatuses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK_ProjectStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](100) NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[projectId] [int] NOT NULL,
	[taskHeading] [nvarchar](100) NOT NULL,
	[assigneeid] [int] NOT NULL,
	[priorityId] [int] NOT NULL,
	[assignedbyId] [int] NOT NULL,
	[dueDate] [datetime] NOT NULL,
	[description] [text] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/12/2017 9:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
	[designationId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Designations] ON 

INSERT [dbo].[Designations] ([id], [name]) VALUES (1, N'It Admin')
INSERT [dbo].[Designations] ([id], [name]) VALUES (2, N'Project Manager')
INSERT [dbo].[Designations] ([id], [name]) VALUES (3, N'Team Lead')
INSERT [dbo].[Designations] ([id], [name]) VALUES (4, N'Developer')
INSERT [dbo].[Designations] ([id], [name]) VALUES (5, N'QA Engineer')
INSERT [dbo].[Designations] ([id], [name]) VALUES (6, N'UX Engineer')
INSERT [dbo].[Designations] ([id], [name]) VALUES (7, NULL)
SET IDENTITY_INSERT [dbo].[Designations] OFF
SET IDENTITY_INSERT [dbo].[Priorities] ON 

INSERT [dbo].[Priorities] ([id], [priorityName]) VALUES (1, N'Low')
INSERT [dbo].[Priorities] ([id], [priorityName]) VALUES (2, N'Medium')
INSERT [dbo].[Priorities] ([id], [priorityName]) VALUES (3, N'High')
SET IDENTITY_INSERT [dbo].[Priorities] OFF
SET IDENTITY_INSERT [dbo].[ProjectStatuses] ON 

INSERT [dbo].[ProjectStatuses] ([id], [name]) VALUES (1, N'Not Started')
INSERT [dbo].[ProjectStatuses] ([id], [name]) VALUES (2, N'Started')
INSERT [dbo].[ProjectStatuses] ([id], [name]) VALUES (3, N'Completed')
INSERT [dbo].[ProjectStatuses] ([id], [name]) VALUES (4, N'Cancelled')
SET IDENTITY_INSERT [dbo].[ProjectStatuses] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (1, N'IT Admin', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (2, N'Add/Edit/View User', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (3, N'Add Project/Edit', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (4, N'Assign User', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (5, N'Add Task', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (6, N'View Projects', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (7, N'View Projects Detail', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (8, N'Add Comment', NULL)
INSERT [dbo].[Roles] ([id], [Role], [userId]) VALUES (9, N'View Comment', NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [name], [email], [password], [status], [designationId]) VALUES (1, N'Arefin', N'htistroke@mail.com', N'htistroke@mail.com123', 1, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[AllFiles]  WITH CHECK ADD  CONSTRAINT [FK_Files_Projects] FOREIGN KEY([projectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[AllFiles] CHECK CONSTRAINT [FK_Files_Projects]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Projects] FOREIGN KEY([projectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Projects]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Users]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tasks] FOREIGN KEY([taskId])
REFERENCES [dbo].[Tasks] ([id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tasks]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([commenterid])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectStatuses] FOREIGN KEY([statusId])
REFERENCES [dbo].[ProjectStatuses] ([id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectStatuses]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Role_User] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Role_User]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Priorities] FOREIGN KEY([priorityId])
REFERENCES [dbo].[Priorities] ([id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Priorities]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([projectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users] FOREIGN KEY([assigneeid])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Designation] FOREIGN KEY([designationId])
REFERENCES [dbo].[Designations] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Designation]
GO
USE [master]
GO
ALTER DATABASE [ProjMngmntsystm] SET  READ_WRITE 
GO
