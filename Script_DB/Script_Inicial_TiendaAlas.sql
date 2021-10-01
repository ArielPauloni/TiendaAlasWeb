USE [master]
GO

IF EXISTS(SELECT 1 FROM sys.databases WHERE [name]='TiendaAlasWeb')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'TiendaAlasWeb'
	ALTER DATABASE [TiendaAlasWeb] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [TiendaAlasWeb]
END
CREATE DATABASE [TiendaAlasWeb]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
	EXEC [TiendaAlasWeb].[dbo].[sp_fulltext_database] @action = 'enable'
END
GO
ALTER DATABASE [TiendaAlasWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TiendaAlasWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TiendaAlasWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TiendaAlasWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TiendaAlasWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [TiendaAlasWeb] SET  MULTI_USER 
GO
ALTER DATABASE [TiendaAlasWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TiendaAlasWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TiendaAlasWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TiendaAlasWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TiendaAlasWeb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TiendaAlasWeb', N'ON'
GO
USE [TiendaAlasWeb]
GO
PRINT 'Tablas'
GO
SET NOCOUNT ON
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[Cod_Bitacora] [int] IDENTITY(1,1) NOT NULL,
	[Cod_Usuario] [int] NOT NULL,
	[Cod_Evento] [tinyint] NOT NULL,
	[FechaEvento] [datetime] NOT NULL,
	[Criticidad] [tinyint] NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[Cod_Bitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitoVerificador](
	[Tabla] [varchar](50) NOT NULL,
	[SumaDVH] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[Cod_Evento] [tinyint] IDENTITY(1,1) NOT NULL,
	[DescripcionEvento] [varchar](50) NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[Cod_Evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma](
	[IdIdioma] [int] IDENTITY(1,1) NOT NULL,
	[CodIdioma] [varchar](2) NULL,
	[DescripcionIdioma] [varchar](50) NULL,
 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 
(
	[IdIdioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdiomaTraduccion]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdiomaTraduccion](
	[IdIdiomaOriginal] [int] NOT NULL,
	[IdIdiomaTraduccion] [int] NOT NULL,
	[DescripcionIdiomaTraducido] [varchar](50) NULL,
 CONSTRAINT [PK_IdiomaTraduccion] PRIMARY KEY CLUSTERED 
(
	[IdIdiomaOriginal] ASC,
	[IdIdiomaTraduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[Cod_Permiso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_permiso] PRIMARY KEY CLUSTERED 
(
	[Cod_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso_Permiso]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso_Permiso](
	[Cod_Permiso_Padre] [int] NULL,
	[Cod_Permiso_Hijo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Texto]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Texto](
	[IdIdioma] [int] NOT NULL,
	[IdFrase] [int] NOT NULL,
	[Texto] [varchar](2500) NULL,
 CONSTRAINT [PK_Texto] PRIMARY KEY CLUSTERED 
(
	[IdIdioma] ASC,
	[IdFrase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[Cod_Tipo] [tinyint] NOT NULL,
	[DescripcionTipo] [varchar](20) NULL,
 CONSTRAINT [PK_Tipo] PRIMARY KEY CLUSTERED 
(
	[Cod_Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Cod_Usuario] [int] NOT NULL,
	[Apellido] [varchar](70) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Alias] [varchar](70) NOT NULL,
	[Contraseña] [varchar](max) NULL,
	[Cod_Tipo] [tinyint] NOT NULL,
	[DVH] [bigint] NOT NULL,
	[Telefono] [varchar](30) NULL,
	[Mail] [varchar](50) NULL,
	[FechaNacimiento] [date] NULL,
	[Inactivo] [bit] NOT NULL,
	[IntentosEquivocados] [smallint] NOT NULL,
	[UltimoLogin] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Cod_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Permiso]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Permiso](
	[Cod_TipoUsuario] [tinyint] NOT NULL,
	[Cod_Permiso] [int] NOT NULL,
 CONSTRAINT [PK_usuarios_permisos] PRIMARY KEY CLUSTERED 
(
	[Cod_TipoUsuario] ASC,
	[Cod_Permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioIdioma]    Script Date: 01/10/2021 2:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioIdioma](
	[Cod_Usuario] [int] NOT NULL,
	[IdIdioma] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioIdioma] PRIMARY KEY CLUSTERED 
(
	[Cod_Usuario] ASC,
	[IdIdioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT 'Insertando datos'
GO

SET IDENTITY_INSERT [dbo].[Bitacora] ON 
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (1, 1, 1, CAST(N'2021-09-24T01:37:21.257' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (2, 1, 14, CAST(N'2021-09-24T01:37:37.723' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (3, 1, 14, CAST(N'2021-09-24T01:37:42.550' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (4, 1, 2, CAST(N'2021-09-24T01:38:03.567' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (5, 1, 1, CAST(N'2021-09-24T01:41:08.820' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (6, 1, 6, CAST(N'2021-09-24T01:41:25.440' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (7, 1, 6, CAST(N'2021-09-24T01:41:29.297' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (8, 1, 2, CAST(N'2021-09-24T01:41:40.900' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (9, 1, 1, CAST(N'2021-09-24T01:45:00.433' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (10, 1, 1, CAST(N'2021-09-24T02:09:37.047' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (11, 1, 1, CAST(N'2021-09-26T12:13:35.207' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (12, 1, 2, CAST(N'2021-09-26T12:21:36.417' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (13, 1, 1, CAST(N'2021-09-26T12:44:07.977' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (14, 1, 1, CAST(N'2021-09-26T12:59:09.850' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (15, 1, 2, CAST(N'2021-09-26T12:59:24.387' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (16, 1, 1, CAST(N'2021-09-26T13:31:36.380' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (17, 1, 1, CAST(N'2021-09-26T13:34:53.027' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (18, 1, 6, CAST(N'2021-09-26T13:35:33.710' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (19, 1, 6, CAST(N'2021-09-26T13:35:48.190' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (20, 1, 2, CAST(N'2021-09-26T13:35:53.070' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (25, 6, 3, CAST(N'2021-09-26T16:36:27.183' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (26, 6, 1, CAST(N'2021-09-26T16:36:27.190' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (27, 6, 2, CAST(N'2021-09-26T16:36:35.757' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (28, 1, 1, CAST(N'2021-09-26T16:36:49.497' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (29, 1, 2, CAST(N'2021-09-26T16:37:16.453' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (30, 1, 1, CAST(N'2021-09-26T19:01:47.117' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (31, 1, 2, CAST(N'2021-09-26T19:03:14.997' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (32, 1, 1, CAST(N'2021-09-26T19:08:31.547' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (33, 1, 1, CAST(N'2021-09-26T19:10:31.990' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (34, 1, 2, CAST(N'2021-09-26T19:18:44.753' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (35, 1, 1, CAST(N'2021-09-27T01:11:48.260' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (36, 1, 3, CAST(N'2021-09-27T01:16:52.900' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (37, 1, 2, CAST(N'2021-09-27T01:20:10.797' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (38, 1, 1, CAST(N'2021-09-27T01:59:52.787' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (39, 1, 2, CAST(N'2021-09-27T02:05:00.120' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (40, 1, 1, CAST(N'2021-09-28T01:27:13.037' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (41, 1, 2, CAST(N'2021-09-28T01:28:14.077' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (42, 1, 1, CAST(N'2021-09-28T01:47:24.627' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (43, 1, 2, CAST(N'2021-09-28T01:50:52.760' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (44, 1, 1, CAST(N'2021-09-28T01:52:08.937' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (45, 1, 1, CAST(N'2021-09-28T01:54:08.727' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (46, 1, 1, CAST(N'2021-09-28T02:23:35.970' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (47, 1, 1, CAST(N'2021-09-28T02:38:20.157' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (48, 1, 1, CAST(N'2021-09-28T02:59:04.990' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (49, 1, 1, CAST(N'2021-09-28T03:07:22.813' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (50, 1, 1, CAST(N'2021-09-28T20:37:53.367' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (51, 1, 1, CAST(N'2021-09-28T20:42:04.190' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (52, 1, 4, CAST(N'2021-09-28T20:43:06.857' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (53, 1, 2, CAST(N'2021-09-28T20:45:37.010' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (54, 1, 1, CAST(N'2021-09-28T20:47:06.730' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (55, 1, 2, CAST(N'2021-09-28T20:48:05.757' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (56, 1, 1, CAST(N'2021-09-28T20:52:17.377' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (57, 1, 4, CAST(N'2021-09-28T20:52:48.083' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (58, 1, 4, CAST(N'2021-09-28T20:53:43.310' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (59, 1, 2, CAST(N'2021-09-28T20:54:29.400' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (60, 1, 1, CAST(N'2021-09-28T22:21:41.193' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (61, 1, 4, CAST(N'2021-09-28T22:24:43.160' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (62, 1, 1, CAST(N'2021-09-28T22:38:08.373' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (63, 1, 4, CAST(N'2021-09-28T22:39:28.670' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (64, 1, 4, CAST(N'2021-09-28T22:40:19.353' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (65, 1, 4, CAST(N'2021-09-28T22:40:54.740' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (66, 1, 2, CAST(N'2021-09-28T22:41:01.397' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (67, 1, 1, CAST(N'2021-09-28T22:43:23.153' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (68, 1, 4, CAST(N'2021-09-28T22:43:36.990' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (69, 1, 4, CAST(N'2021-09-28T22:45:39.530' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (70, 1, 2, CAST(N'2021-09-28T22:45:49.603' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (71, 1, 1, CAST(N'2021-09-28T22:55:50.300' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (72, 1, 2, CAST(N'2021-09-28T22:58:28.987' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (73, 1, 1, CAST(N'2021-09-28T23:02:51.073' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (74, 1, 9, CAST(N'2021-09-28T23:17:56.480' AS DateTime), 3)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (75, 1, 5, CAST(N'2021-09-28T23:19:46.053' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (76, 1, 1, CAST(N'2021-09-28T23:26:30.003' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (77, 1, 5, CAST(N'2021-09-28T23:26:57.980' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (78, 1, 2, CAST(N'2021-09-28T23:33:04.913' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (79, 1, 1, CAST(N'2021-09-28T23:38:05.983' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (80, 1, 5, CAST(N'2021-09-28T23:39:27.990' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (81, 1, 3, CAST(N'2021-09-28T23:41:49.897' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (82, 1, 2, CAST(N'2021-09-28T23:42:52.763' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (83, 1, 1, CAST(N'2021-09-28T23:58:34.523' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (84, 1, 2, CAST(N'2021-09-29T00:01:07.993' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (85, 1, 1, CAST(N'2021-09-29T00:01:59.813' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (86, 1, 4, CAST(N'2021-09-29T00:02:12.660' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (87, 1, 4, CAST(N'2021-09-29T00:02:20.550' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (88, 1, 4, CAST(N'2021-09-29T00:03:05.780' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (89, 1, 4, CAST(N'2021-09-29T00:03:30.730' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (90, 1, 2, CAST(N'2021-09-29T00:03:59.893' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (91, 1, 1, CAST(N'2021-09-29T00:30:37.680' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (92, 1, 2, CAST(N'2021-09-29T00:33:33.713' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (93, 1, 1, CAST(N'2021-09-29T00:46:00.700' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (94, 1, 2, CAST(N'2021-09-29T00:52:42.033' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (95, 1, 1, CAST(N'2021-09-29T00:56:31.557' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (96, 1, 2, CAST(N'2021-09-29T00:57:02.483' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (97, 1, 1, CAST(N'2021-09-29T00:57:29.263' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (98, 1, 2, CAST(N'2021-09-29T00:57:54.897' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (99, 1, 1, CAST(N'2021-09-29T01:18:49.157' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (100, 1, 4, CAST(N'2021-09-29T01:19:51.053' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (101, 1, 4, CAST(N'2021-09-29T01:20:17.097' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (102, 1, 2, CAST(N'2021-09-29T01:20:31.143' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (103, 1, 1, CAST(N'2021-09-29T01:25:46.967' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (104, 1, 2, CAST(N'2021-09-29T01:25:59.260' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (105, 6, 1, CAST(N'2021-09-29T01:26:08.027' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (106, 6, 2, CAST(N'2021-09-29T01:26:13.937' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (107, 1, 1, CAST(N'2021-09-29T01:26:31.483' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (108, 1, 4, CAST(N'2021-09-29T01:26:57.403' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (109, 1, 2, CAST(N'2021-09-29T01:27:25.623' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (110, 6, 1, CAST(N'2021-09-29T01:27:56.223' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (111, 6, 2, CAST(N'2021-09-29T01:28:08.170' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (112, 1, 1, CAST(N'2021-09-29T01:29:27.467' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (113, 1, 4, CAST(N'2021-09-29T01:29:51.610' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (114, 1, 2, CAST(N'2021-09-29T01:29:59.877' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (115, 1, 1, CAST(N'2021-09-29T01:39:07.840' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (116, 1, 4, CAST(N'2021-09-29T01:39:19.110' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (117, 1, 2, CAST(N'2021-09-29T01:39:26.500' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (118, 5, 1, CAST(N'2021-09-29T01:39:33.620' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (119, 5, 2, CAST(N'2021-09-29T01:39:40.797' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (120, 5, 1, CAST(N'2021-09-29T02:16:16.140' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (121, 5, 2, CAST(N'2021-09-29T02:20:47.317' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (122, 5, 1, CAST(N'2021-09-29T02:22:58.550' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (123, 5, 2, CAST(N'2021-09-29T02:23:33.220' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (124, 5, 1, CAST(N'2021-09-29T02:25:13.037' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (127, 5, 1, CAST(N'2021-09-29T02:27:18.533' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (130, 1, 1, CAST(N'2021-09-29T02:30:54.737' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (131, 1, 2, CAST(N'2021-09-29T02:31:31.673' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (132, 5, 1, CAST(N'2021-09-29T02:31:36.610' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (135, 1, 1, CAST(N'2021-09-29T02:32:12.123' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (136, 1, 2, CAST(N'2021-09-29T02:33:06.413' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (137, 5, 1, CAST(N'2021-09-29T02:33:26.773' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (140, 5, 1, CAST(N'2021-09-29T02:37:55.410' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (141, 5, 3, CAST(N'2021-09-29T02:38:10.663' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (142, 5, 2, CAST(N'2021-09-29T02:38:24.157' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (143, 5, 1, CAST(N'2021-09-29T02:38:31.693' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (144, 5, 3, CAST(N'2021-09-29T02:39:04.170' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (145, 5, 2, CAST(N'2021-09-29T02:40:38.710' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (146, 1, 1, CAST(N'2021-09-29T02:43:03.563' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (147, 1, 4, CAST(N'2021-09-29T02:44:25.480' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (148, 1, 2, CAST(N'2021-09-29T02:44:34.103' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (149, 1, 1, CAST(N'2021-09-29T02:45:19.253' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (150, 1, 4, CAST(N'2021-09-29T02:45:40.507' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (151, 1, 4, CAST(N'2021-09-29T02:45:55.890' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (152, 1, 3, CAST(N'2021-09-29T02:46:23.140' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (153, 1, 2, CAST(N'2021-09-29T02:46:29.700' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (154, 1, 1, CAST(N'2021-09-29T02:46:32.573' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (155, 1, 2, CAST(N'2021-09-29T02:46:43.440' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (156, 1, 1, CAST(N'2021-09-30T00:09:56.963' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (157, 1, 1, CAST(N'2021-09-30T00:13:06.433' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (158, 1, 2, CAST(N'2021-09-30T00:15:02.810' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (159, 1, 1, CAST(N'2021-09-30T00:17:30.463' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (160, 1, 1, CAST(N'2021-09-30T00:30:15.537' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (161, 1, 1, CAST(N'2021-09-30T00:31:52.467' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (162, 1, 2, CAST(N'2021-09-30T00:34:17.503' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (163, 1, 1, CAST(N'2021-09-30T00:40:39.360' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (164, 1, 2, CAST(N'2021-09-30T00:44:08.890' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (165, 1, 1, CAST(N'2021-09-30T00:59:51.260' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (166, 1, 2, CAST(N'2021-09-30T01:00:16.710' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (167, 1, 1, CAST(N'2021-09-30T01:01:19.950' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (168, 1, 1, CAST(N'2021-09-30T01:05:47.507' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (169, 1, 1, CAST(N'2021-09-30T01:07:16.450' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (170, 1, 2, CAST(N'2021-09-30T01:09:25.473' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (171, 1, 1, CAST(N'2021-09-30T01:37:56.363' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (172, 1, 11, CAST(N'2021-09-30T01:38:27.747' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (173, 1, 8, CAST(N'2021-09-30T01:38:39.070' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (174, 1, 8, CAST(N'2021-09-30T01:40:47.190' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (175, 1, 2, CAST(N'2021-09-30T01:41:02.673' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (176, 1, 1, CAST(N'2021-09-30T02:22:46.270' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (177, 1, 2, CAST(N'2021-09-30T02:25:07.833' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (178, 1, 1, CAST(N'2021-09-30T02:25:13.247' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (179, 1, 1, CAST(N'2021-09-30T02:26:23.740' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (180, 1, 2, CAST(N'2021-09-30T02:26:41.280' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (181, 1, 1, CAST(N'2021-09-30T02:57:31.587' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (182, 1, 11, CAST(N'2021-09-30T02:57:59.280' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (183, 1, 8, CAST(N'2021-09-30T02:58:19.273' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (184, 1, 8, CAST(N'2021-09-30T02:59:25.483' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (185, 1, 2, CAST(N'2021-09-30T02:59:43.930' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (186, 1, 1, CAST(N'2021-09-30T02:59:46.343' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (187, 1, 8, CAST(N'2021-09-30T02:59:49.487' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (188, 1, 8, CAST(N'2021-09-30T03:01:14.380' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (189, 1, 11, CAST(N'2021-09-30T03:01:33.083' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (190, 1, 2, CAST(N'2021-09-30T03:02:17.247' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (191, 1, 1, CAST(N'2021-09-30T03:07:30.123' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (192, 1, 11, CAST(N'2021-09-30T03:07:35.770' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (193, 1, 1, CAST(N'2021-09-30T03:08:54.500' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (194, 1, 11, CAST(N'2021-09-30T03:08:58.510' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (195, 1, 6, CAST(N'2021-09-30T03:09:05.117' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (196, 1, 11, CAST(N'2021-09-30T03:09:06.660' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (197, 1, 6, CAST(N'2021-09-30T03:09:10.397' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (198, 1, 11, CAST(N'2021-09-30T03:09:11.940' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (199, 1, 8, CAST(N'2021-09-30T03:09:21.090' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (200, 1, 11, CAST(N'2021-09-30T03:09:36.863' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (201, 1, 1, CAST(N'2021-09-30T03:16:45.887' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (202, 1, 11, CAST(N'2021-09-30T03:16:50.913' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (203, 1, 8, CAST(N'2021-09-30T03:17:00.703' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (204, 1, 2, CAST(N'2021-09-30T03:17:36.530' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (205, 1, 1, CAST(N'2021-09-30T03:25:54.987' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (206, 1, 11, CAST(N'2021-09-30T03:25:58.443' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (207, 1, 8, CAST(N'2021-09-30T03:26:21.017' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (208, 1, 2, CAST(N'2021-09-30T03:28:25.530' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (209, 1, 1, CAST(N'2021-09-30T03:29:34.680' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (210, 1, 8, CAST(N'2021-09-30T03:29:38.733' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (211, 1, 8, CAST(N'2021-09-30T03:29:58.600' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (212, 1, 11, CAST(N'2021-09-30T03:30:08.113' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (213, 1, 2, CAST(N'2021-09-30T03:30:40.773' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (214, 1, 1, CAST(N'2021-09-30T18:42:18.627' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (215, 1, 6, CAST(N'2021-09-30T18:42:26.513' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (216, 1, 6, CAST(N'2021-09-30T18:46:02.647' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (217, 1, 14, CAST(N'2021-09-30T18:46:34.823' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (218, 1, 6, CAST(N'2021-09-30T18:46:40.400' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (219, 1, 6, CAST(N'2021-09-30T18:46:42.413' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (220, 1, 14, CAST(N'2021-09-30T18:47:17.510' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (221, 1, 7, CAST(N'2021-09-30T18:47:56.437' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (222, 1, 7, CAST(N'2021-09-30T18:49:04.833' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (223, 1, 6, CAST(N'2021-09-30T18:49:38.007' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (224, 1, 6, CAST(N'2021-09-30T18:52:37.227' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (225, 1, 11, CAST(N'2021-09-30T18:57:05.100' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (226, 1, 11, CAST(N'2021-09-30T18:57:35.097' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (227, 1, 8, CAST(N'2021-09-30T18:58:04.913' AS DateTime), 4)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (228, 1, 11, CAST(N'2021-09-30T18:59:09.720' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (229, 1, 3, CAST(N'2021-09-30T19:00:37.930' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (230, 1, 3, CAST(N'2021-09-30T19:01:05.317' AS DateTime), 2)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (231, 1, 1, CAST(N'2021-09-30T21:29:46.443' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (232, 1, 1, CAST(N'2021-09-30T21:37:09.590' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (233, 1, 2, CAST(N'2021-09-30T21:40:03.313' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (234, 1, 1, CAST(N'2021-10-01T00:58:32.723' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (235, 1, 6, CAST(N'2021-10-01T00:58:43.763' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (236, 1, 6, CAST(N'2021-10-01T00:58:49.440' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (237, 1, 6, CAST(N'2021-10-01T00:59:06.073' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (238, 1, 11, CAST(N'2021-10-01T01:03:08.080' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (239, 1, 6, CAST(N'2021-10-01T01:03:15.320' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (240, 1, 2, CAST(N'2021-10-01T01:03:29.777' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (241, 1, 1, CAST(N'2021-10-01T01:32:01.523' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (242, 1, 6, CAST(N'2021-10-01T01:32:03.147' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (243, 1, 1, CAST(N'2021-10-01T01:39:26.610' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (244, 1, 1, CAST(N'2021-10-01T02:01:40.147' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (245, 1, 2, CAST(N'2021-10-01T02:02:29.997' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (246, 1, 1, CAST(N'2021-10-01T02:07:12.500' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (247, 1, 6, CAST(N'2021-10-01T02:07:30.160' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (248, 1, 1, CAST(N'2021-10-01T02:13:19.163' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (249, 1, 1, CAST(N'2021-10-01T02:23:36.247' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (250, 1, 1, CAST(N'2021-10-01T02:35:39.130' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (251, 1, 1, CAST(N'2021-10-01T02:41:46.370' AS DateTime), 1)
GO
INSERT [dbo].[Bitacora] ([Cod_Bitacora], [Cod_Usuario], [Cod_Evento], [FechaEvento], [Criticidad]) VALUES (252, 1, 2, CAST(N'2021-10-01T02:42:56.433' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
GO
INSERT [dbo].[DigitoVerificador] ([Tabla], [SumaDVH]) VALUES (N'dbo.Usuario', 2191956)
GO
SET IDENTITY_INSERT [dbo].[Evento] ON 
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (1, N'Login')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (2, N'Logout')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (3, N'AltaDeUsuario')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (4, N'ModificaciónUsuario')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (5, N'BajaDeUsuario')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (6, N'CambioDeIdioma')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (7, N'CreaciónDeIdioma')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (8, N'ErrorEnIntegridadUsuario')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (9, N'GenerarBackup')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (10, N'RestaurarBackup')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (11, N'ChequeoIntegridadExitoso')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (12, N'NuevoPermisoCreado')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (13, N'RecuperoDePass')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (14, N'CambioTextoIdioma')
GO
INSERT [dbo].[Evento] ([Cod_Evento], [DescripcionEvento]) VALUES (15, N'PermisoEliminado')
GO
SET IDENTITY_INSERT [dbo].[Evento] OFF
GO
SET IDENTITY_INSERT [dbo].[Idioma] ON 
GO
INSERT [dbo].[Idioma] ([IdIdioma], [CodIdioma], [DescripcionIdioma]) VALUES (1, N'es', N'Español')
GO
INSERT [dbo].[Idioma] ([IdIdioma], [CodIdioma], [DescripcionIdioma]) VALUES (2, N'en', N'Inglés')
GO
SET IDENTITY_INSERT [dbo].[Idioma] OFF
GO
INSERT [dbo].[IdiomaTraduccion] ([IdIdiomaOriginal], [IdIdiomaTraduccion], [DescripcionIdiomaTraducido]) VALUES (1, 1, N'Español')
GO
INSERT [dbo].[IdiomaTraduccion] ([IdIdiomaOriginal], [IdIdiomaTraduccion], [DescripcionIdiomaTraducido]) VALUES (1, 2, N'Spanish')
GO
INSERT [dbo].[IdiomaTraduccion] ([IdIdiomaOriginal], [IdIdiomaTraduccion], [DescripcionIdiomaTraducido]) VALUES (2, 1, N'Inglés')
GO
INSERT [dbo].[IdiomaTraduccion] ([IdIdiomaOriginal], [IdIdiomaTraduccion], [DescripcionIdiomaTraducido]) VALUES (2, 2, N'English')
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (1, N'Gestionar Permisos')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (2, N'Asignar Permisos')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (3, N'Crear Permisos')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (4, N'Quitar permisos')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (5, N'Permisos Usuario')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (6, N'Eliminar Permisos')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (7, N'Crear Idioma')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (8, N'Editar Idioma')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (9, N'Gestionar Idiomas')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (13, N'Ver Bitacora')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (14, N'ABM Administrador')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (15, N'ABM Usuarios')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (16, N'BackUps')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (17, N'Generar Backup')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (18, N'Restaurar Backup')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (19, N'Crear TipoUsuario')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (20, N'Crear Usuario')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (22, N'Editar Usuario')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (23, N'Eliminar Usuario')
GO
INSERT [dbo].[Permiso] ([Cod_Permiso], [Nombre]) VALUES (24, N'Ver Integridad')
GO
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (1, 3)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (1, 6)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (5, 2)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (9, 8)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (15, 20)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (15, 22)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (15, 23)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (5, 4)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (16, 17)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (16, 18)
GO
INSERT [dbo].[Permiso_Permiso] ([Cod_Permiso_Padre], [Cod_Permiso_Hijo]) VALUES (9, 7)
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 1, N'Error')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 2, N'Aceptar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 3, N'Bitácora')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 4, N'Inicio')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 5, N'Seguridad')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 6, N'Permisos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 7, N'Resguardar/Restablecer')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 8, N'Idiomas')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 9, N'Registrarse')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 10, N'Ingresar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 11, N'Alerta')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 12, N'Datos Salvados correctamente')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 13, N'Salir')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 14, N'Acerca De')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 15, N'Contacto')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 16, N'Backup')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 17, N'Generar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 18, N'Restaurar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 19, N'Guardar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 20, N'Cancelar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 21, N'Nombre Archivo')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 22, N'Codigo ISO 639-1')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 23, N'Descripción Idioma')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 24, N'Traducir')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 25, N'Procesando...')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 26, N'Textos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 27, N'Editar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 28, N'Confirmar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 29, N'Crear nuevo idioma')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 30, N'Deshacer')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 31, N'Datos salvados correctamente')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 32, N'Información')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 33, N'Texto')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 34, N'Contiene')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 35, N'Comienza con')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 36, N'Igual a')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 37, N'Termina con')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 38, N'Filtros')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 39, N'Mostrar filtros')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 40, N'Filtrar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 41, N'Limpiar filtros')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 42, N'Ocultar filtros')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 43, N'Chequear Integridad')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 44, N'Por favor contacte a un administrador')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 45, N'Operación exitosa')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 46, N'Integridad')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 47, N'Formato de mail incorrecto')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 48, N'Usuarios')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 49, N'ABM Usuario')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 50, N'Nuevo Usuario')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 51, N'Tipo Usuario')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 52, N'Información')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 53, N'Advertencia')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 54, N'Cantidad de frases traducidas')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 55, N'Datos Incorrectos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 56, N'Mis Datos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 57, N'Usted no posee permisos para esta acción')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 58, N'No puede grabar un registro con datos vacíos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 59, N'No se pudieron guardar los datos')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 60, N'Contraseñas no coinciden')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 61, N'Contraseña debe contener mayúscula, número y longitud mayor a seis')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 62, N'Enviar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 63, N'Nombre')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 64, N'Apellido')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 65, N'Empresa')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 66, N'Teléfono')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 67, N'Email')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 68, N'Mensaje')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 69, N'¡Contactanos! Estamos trabajando de manera remota, envianos un mensaje y responderemos a la brevedad.')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 70, N'Ingrese un valor correcto de la suma propuesta')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 71, N'Usuario')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 72, N'Usuario Bloqueado, solicite a un administrador su desbloqueo')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 73, N'Alias')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 74, N'Contraseña')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 75, N'Repita contraseña')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 76, N'Fecha de Nacimiento')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 77, N'Acción')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 78, N'Bloqueado')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 79, N'Mostrar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 80, N'Ocultar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 81, N'Página de inicio de sesión')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 82, N'Inicio de sesión')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 83, N'Recordarme')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 84, N'¿Olvidó su contraseña?')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 85, N'Recuperar Contraseña')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 86, N'¿Seguro desea cerrar la sesión?')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 87, N'Eliminar')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 88, N'Crear Nuevo Usuario')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 89, N'Criticidad')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 90, N'Descripción')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 91, N'Fecha Evento')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 92, N'Evento')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 93, N'Bitácora del sistema')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 94, N'Fecha Desde')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 95, N'Fecha Hasta')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 96, N'Exportar a PDF')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 97, N'Exportar a JSON')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 98, N'Exportar a XML')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (1, 99, N'Pág. {0} de {1}')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 1, N'Error')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 2, N'Accept')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 3, N'Binnacle')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 4, N'Home')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 5, N'Security')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 6, N'Permissions')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 7, N'Backup/Restore')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 8, N'Languages')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 9, N'Sign up')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 10, N'Login')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 11, N'Alert')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 12, N'Data saved successfully')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 13, N'Logout')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 14, N'About')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 15, N'Contact')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 16, N'Backup')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 17, N'Generate')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 18, N'Restore')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 19, N'Save')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 20, N'Cancel')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 21, N'File Name')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 22, N'ISO code 639-1')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 23, N'Description / languages')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 24, N'Translate')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 25, N'Processing...')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 26, N'Texts')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 27, N'Edit')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 28, N'Confirm')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 29, N'Create new language')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 30, N'Undo')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 31, N'Data saved successfully')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 32, N'Information')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 33, N'Text')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 34, N'Contains')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 35, N'Start with')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 36, N'Equal to')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 37, N'Ends with')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 38, N'Filters')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 39, N'Show filters')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 40, N'Filter')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 41, N'Clean filters')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 42, N'Hide filters')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 43, N'Check Integrity')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 44, N'Please contact an administrator')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 45, N'Successful operation')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 46, N'Integrity')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 47, N'Wrong mail format')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 48, N'Users')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 49, N'User CRUD')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 50, N'New User')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 51, N'User Type')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 52, N'Information')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 53, N'Warning')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 54, N'Number of phrases translated')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 55, N'Incorrect data')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 56, N'My data')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 57, N'You do not have permissions for this action')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 58, N'Cannot record a record with empty data')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 59, N'Data could not be saved')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 60, N'Password mismatch')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 61, N'Password must contain capital letter, number and length greater than six')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 62, N'Send')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 63, N'Name')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 64, N'Surname')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 65, N'Company')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 66, N'Phone')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 67, N'Email')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 68, N'Message')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 69, N'Contact us! We are working remotely, send us a message and we will respond as soon as possible')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 70, N'Enter a correct value of the proposed sum')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 71, N'User')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 72, N'Blocked user, ask an administrator to unblock it')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 73, N'Alias')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 74, N'Password')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 75, N'Repeat password')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 76, N'Birthdate')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 77, N'Action')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 78, N'Locked')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 79, N'Show')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 80, N'Hide')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 81, N'Login page')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 82, N'Login')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 83, N'Remember me')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 84, N'Forgot your password?')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 85, N'Recover password')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 86, N'Are you sure you want to logout?')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 87, N'Delete')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 88, N'Create New User')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 89, N'Criticality')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 90, N'Description')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 91, N'Event Date')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 92, N'Event')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 93, N'System log')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 94, N'Date from')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 95, N'Date to')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 96, N'Export to PDF')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 97, N'Export to JSON')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 98, N'Export to XML')
GO
INSERT [dbo].[Texto] ([IdIdioma], [IdFrase], [Texto]) VALUES (2, 99, N'Page. {0} of {1}')
GO
INSERT [dbo].[TipoUsuario] ([Cod_Tipo], [DescripcionTipo]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[TipoUsuario] ([Cod_Tipo], [DescripcionTipo]) VALUES (2, N'Paciente')
GO
INSERT [dbo].[TipoUsuario] ([Cod_Tipo], [DescripcionTipo]) VALUES (3, N'Profesional')
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (1, N'Dell''Agostino', N'Nadia', N'NadiaAlas', ENCRYPTBYPASSPHRASE('enigma', 'GwzYP7mLZfJ/uZrpzUPrLw=='), 1, 396252, N'115555-3333', N'arielpauloni@gmail.com', CAST(N'1985-06-18' AS Date), 0, 0, CAST(N'2021-10-01T02:41:46.323' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (2, N'Pellegrino', N'Luciana', N'Luchita', ENCRYPTBYPASSPHRASE('enigma', 'OBFLcDC1JkFeoBUtnDSDqw=='), 3, 313029, N'', N'pellegrino@mail.com.ar', CAST(N'1986-02-12' AS Date), 0, 0, NULL)
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (3, N'Perez', N'Juan', N'Paciente1', ENCRYPTBYPASSPHRASE('enigma', '4Bt0ACe7+5/boGrzvN7Ukw=='), 2, 230020, N'', N'paciente1@sumail.com', NULL, 0, 0, NULL)
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (4, N'Gomez', N'Martin', N'Paciente2', ENCRYPTBYPASSPHRASE('enigma', '4Bt0ACe7+5/boGrzvN7Ukw=='), 2, 299540, N'4255-3387', N'martingomez@mail.com.ar', NULL, 0, 0, NULL)
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (5, N'Torres', N'Luciano', N'Lucho', ENCRYPTBYPASSPHRASE('enigma', '4Bt0ACe7+5/boGrzvN7Ukw=='), 2, 403434, N'0421-7272-3993', N'arielpauloni@gmail.com', CAST(N'1985-12-13' AS Date), 0, 0, CAST(N'2021-09-29T02:38:31.000' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (6, N'Power', N'Maximiliano', N'MaxPower', ENCRYPTBYPASSPHRASE('enigma', '4Bt0ACe7+5/boGrzvN7Ukw=='), 2, 310229, N'543211233', N'maxpower@email.com', NULL, 0, 0, CAST(N'2021-09-29T01:27:56.190' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Cod_Usuario], [Apellido], [Nombre], [Alias], [Contraseña], [Cod_Tipo], [DVH], [Telefono], [Mail], [FechaNacimiento], [Inactivo], [IntentosEquivocados], [UltimoLogin]) VALUES (7, N'Becerra', N'Juan Manuel', N'juanBecerra', ENCRYPTBYPASSPHRASE('enigma', 'OBFLcDC1JkFeoBUtnDSDqw=='), 3, 239452, N'', N'juan@mail.com.ar', CAST(N'2000-01-13' AS Date), 0, 0, NULL)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 1)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 5)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 9)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 13)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 14)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 15)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 16)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 19)
GO
INSERT [dbo].[Usuario_Permiso] ([Cod_TipoUsuario], [Cod_Permiso]) VALUES (1, 24)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (1, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (2, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (3, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (4, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (5, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (6, 1)
GO
INSERT [dbo].[UsuarioIdioma] ([Cod_Usuario], [IdIdioma]) VALUES (7, 1)
GO
SET ANSI_PADDING ON
GO

PRINT 'Creando Indices'
GO

/****** Object:  Index [IX_TipoUsuario_Descripcion]    Script Date: 01/10/2021 2:45:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TipoUsuario_Descripcion] ON [dbo].[TipoUsuario]
(
	[DescripcionTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_UsuarioDVH]  DEFAULT ((0)) FOR [DVH]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_UsuarioInactivo]  DEFAULT ((0)) FOR [Inactivo]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_UsuarioIntentosEquivocados]  DEFAULT ((0)) FOR [IntentosEquivocados]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Evento] FOREIGN KEY([Cod_Evento])
REFERENCES [dbo].[Evento] ([Cod_Evento])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Evento]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Usuario] FOREIGN KEY([Cod_Usuario])
REFERENCES [dbo].[Usuario] ([Cod_Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Usuario]
GO
ALTER TABLE [dbo].[IdiomaTraduccion]  WITH CHECK ADD  CONSTRAINT [FK_IdiomaTraduccionIdiomaOriginal] FOREIGN KEY([IdIdiomaOriginal])
REFERENCES [dbo].[Idioma] ([IdIdioma])
GO
ALTER TABLE [dbo].[IdiomaTraduccion] CHECK CONSTRAINT [FK_IdiomaTraduccionIdiomaOriginal]
GO
ALTER TABLE [dbo].[IdiomaTraduccion]  WITH CHECK ADD  CONSTRAINT [FK_IdiomaTraduccionIdiomaTraduccion] FOREIGN KEY([IdIdiomaTraduccion])
REFERENCES [dbo].[Idioma] ([IdIdioma])
GO
ALTER TABLE [dbo].[IdiomaTraduccion] CHECK CONSTRAINT [FK_IdiomaTraduccionIdiomaTraduccion]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Permiso_Permiso] FOREIGN KEY([Cod_Permiso_Padre])
REFERENCES [dbo].[Permiso] ([Cod_Permiso])
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [FK_Permiso_Permiso_Permiso]
GO
ALTER TABLE [dbo].[Permiso_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Permiso_Permiso_Permiso1] FOREIGN KEY([Cod_Permiso_Hijo])
REFERENCES [dbo].[Permiso] ([Cod_Permiso])
GO
ALTER TABLE [dbo].[Permiso_Permiso] CHECK CONSTRAINT [FK_Permiso_Permiso_Permiso1]
GO
ALTER TABLE [dbo].[Texto]  WITH CHECK ADD  CONSTRAINT [FK_TextIdioma] FOREIGN KEY([IdIdioma])
REFERENCES [dbo].[Idioma] ([IdIdioma])
GO
ALTER TABLE [dbo].[Texto] CHECK CONSTRAINT [FK_TextIdioma]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_TipoUsuario] FOREIGN KEY([Cod_Tipo])
REFERENCES [dbo].[TipoUsuario] ([Cod_Tipo])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_TipoUsuario]
GO
ALTER TABLE [dbo].[Usuario_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Permisos_Permiso] FOREIGN KEY([Cod_Permiso])
REFERENCES [dbo].[Permiso] ([Cod_Permiso])
GO
ALTER TABLE [dbo].[Usuario_Permiso] CHECK CONSTRAINT [FK_Usuarios_Permisos_Permiso]
GO
ALTER TABLE [dbo].[Usuario_Permiso]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Permisos_Usuarios] FOREIGN KEY([Cod_TipoUsuario])
REFERENCES [dbo].[TipoUsuario] ([Cod_Tipo])
GO
ALTER TABLE [dbo].[Usuario_Permiso] CHECK CONSTRAINT [FK_Usuarios_Permisos_Usuarios]
GO
ALTER TABLE [dbo].[UsuarioIdioma]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioIdioma_Idioma] FOREIGN KEY([IdIdioma])
REFERENCES [dbo].[Idioma] ([IdIdioma])
GO
ALTER TABLE [dbo].[UsuarioIdioma] CHECK CONSTRAINT [FK_UsuarioIdioma_Idioma]
GO
ALTER TABLE [dbo].[UsuarioIdioma]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioIdioma_Usuario] FOREIGN KEY([Cod_Usuario])
REFERENCES [dbo].[Usuario] ([Cod_Usuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioIdioma] CHECK CONSTRAINT [FK_UsuarioIdioma_Usuario]
GO

PRINT 'Creacion de vistas'
GO
/****** Object:  View [dbo].[vPermisoPermiso]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vPermisoPermiso]
AS
SELECT	pp.Cod_Permiso_Padre, pPad.Nombre AS [Nombre_Padre], 
		pp.Cod_Permiso_Hijo, pHij.Nombre AS [Nombre_Hijo]
FROM	Permiso_Permiso pp
JOIN	Permiso pPad ON pp.Cod_Permiso_Padre = pPad.Cod_Permiso
JOIN	Permiso pHij ON pp.Cod_Permiso_Hijo =pHij.Cod_Permiso
GO
/****** Object:  View [dbo].[vTipoUsuarioPermiso]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vTipoUsuarioPermiso]
AS
SELECT	up.Cod_TipoUsuario, tu.DescripcionTipo, up.Cod_Permiso, p.Nombre 
FROM	Usuario_Permiso up
JOIN	TipoUsuario tu ON up.Cod_TipoUsuario = tu.Cod_Tipo
JOIN	Permiso p ON up.Cod_Permiso = p.Cod_Permiso
GO

PRINT 'Creacion de Stored Procedures'
GO

/****** Object:  StoredProcedure [dbo].[pr_Actualizar_DigitoVerificador]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Actualizar_DigitoVerificador]
@Tabla VARCHAR(50),
@SumaDVH BIGINT
AS
BEGIN
	UPDATE DigitoVerificador SET SumaDVH = @SumaDVH WHERE Tabla = @Tabla
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Actualizar_IdiomasNombresTraduccion]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Actualizar_IdiomasNombresTraduccion]
@IdIdiomaOriginal INT,
@IdIdiomaTraduccion INT,
@DescripcionIdiomaTraducido VARCHAR(50)
AS
UPDATE IdiomaTraduccion
SET DescripcionIdiomaTraducido = @DescripcionIdiomaTraducido
WHERE IdIdiomaOriginal = @IdIdiomaOriginal AND IdIdiomaTraduccion = @IdIdiomaTraduccion
GO
/****** Object:  StoredProcedure [dbo].[pr_Actualizar_IdiomaUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Actualizar_IdiomaUsuario]
@Cod_Usuario INT,
@IdIdioma INT
AS
UPDATE dbo.UsuarioIdioma SET IdIdioma = @IdIdioma WHERE Cod_Usuario = @Cod_Usuario
GO
/****** Object:  StoredProcedure [dbo].[pr_Actualizar_TextoIdioma]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Actualizar_TextoIdioma]
@IdIdioma INT,
@IdFrase INT,
@Texto VARCHAR(2500)
AS
UPDATE dbo.Texto SET  Texto = @Texto WHERE IdIdioma = @IdIdioma AND IdFrase = @IdFrase
GO
/****** Object:  StoredProcedure [dbo].[pr_Actualizar_Usuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Actualizar_Usuario]
@Cod_Usuario INT,
@Apellido VARCHAR(70),
@Nombre VARCHAR(70),
@Alias VARCHAR(70),
@Contrasenia VARCHAR(MAX),
@Cod_Tipo TINYINT,
@Telefono VARCHAR(30),
@Mail VARCHAR(50),
@FechaNacimiento DATETIME,
@Inactivo BIT,
@IntentosEquivocados SMALLINT,
@UltimoLogin DATETIME,
@DVH BIGINT
AS

UPDATE dbo.Usuario
SET Apellido = @Apellido, 
	Nombre = @Nombre, 
	Alias = @Alias, 
	[Contraseña] = ENCRYPTBYPASSPHRASE('enigma', @Contrasenia), 
	Cod_Tipo = @Cod_Tipo, 
	Telefono = @Telefono,
	Mail = @Mail,
	FechaNacimiento = @FechaNacimiento,
	Inactivo = @Inactivo,
	IntentosEquivocados = @IntentosEquivocados,
	UltimoLogin = @UltimoLogin,
	DVH = @DVH
WHERE Cod_Usuario = @Cod_Usuario

DECLARE @suma BIGINT
SELECT @suma = SUM(DVH) FROM Usuario
UPDATE DigitoVerificador SET SumaDVH = @suma WHERE Tabla = 'dbo.Usuario'

GO
/****** Object:  StoredProcedure [dbo].[pr_Eliminar_Idioma]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Eliminar_Idioma]
@CodIdioma VARCHAR(2)
AS
DECLARE @IdIdioma INT
SELECT @IdIdioma = IdIdioma FROM dbo.Idioma WHERE CodIdioma = @CodIdioma

DELETE FROM dbo.IdiomaTraduccion WHERE IdIdiomaOriginal = @IdIdioma
DELETE FROM dbo.IdiomaTraduccion WHERE IdIdiomaTraduccion = @IdIdioma

EXECUTE [dbo].[pr_Eliminar_TextosDeIdioma] @CodIdioma
DELETE FROM Idioma WHERE CodIdioma = @CodIdioma

EXECUTE [dbo].[pr_Reseed_Identity] 'Idioma'
GO
/****** Object:  StoredProcedure [dbo].[pr_Eliminar_Permiso]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_Eliminar_Permiso]
@Cod_Permiso INT
AS
BEGIN
	DELETE FROM Permiso_Permiso WHERE Cod_Permiso_Hijo = @Cod_Permiso
	DELETE FROM Permiso_Permiso WHERE Cod_Permiso_Padre = @Cod_Permiso
	DELETE FROM Usuario_Permiso WHERE Cod_Permiso = @Cod_Permiso
	DELETE FROM Permiso WHERE Cod_Permiso = @Cod_Permiso
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Eliminar_PermisoPorTipoUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Eliminar_PermisoPorTipoUsuario]
@Cod_TipoUsuario TINYINT,
@Cod_Permiso INT
AS

DELETE FROM Usuario_Permiso WHERE Cod_TipoUsuario = @Cod_TipoUsuario AND Cod_Permiso = @Cod_Permiso
GO
/****** Object:  StoredProcedure [dbo].[pr_Eliminar_RelacionPermisos]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Eliminar_RelacionPermisos]
@Cod_Permiso_Padre INT,
@Cod_Permiso_Hijo INT
AS

DELETE FROM Permiso_Permiso WHERE Cod_Permiso_Hijo = @Cod_Permiso_Hijo AND Cod_Permiso_Padre = @Cod_Permiso_Padre
GO
/****** Object:  StoredProcedure [dbo].[pr_Eliminar_TextosDeIdioma]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Eliminar_TextosDeIdioma]
@CodIdioma VARCHAR(2)
AS
DECLARE @IdIdioma INT
SELECT @IdIdioma = IdIdioma FROM dbo.Idioma WHERE CodIdioma = @CodIdioma
DELETE FROM dbo.Texto WHERE IdIdioma = @IdIdioma

--DELETE FROM dbo.IdiomaTraduccion WHERE IdIdiomaOriginal = @IdIdioma
--DELETE FROM dbo.IdiomaTraduccion WHERE IdIdiomaTraduccion = @IdIdioma
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_Bitacora]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_Bitacora]
@Cod_Usuario INT,
@Cod_Evento TINYINT,
@Criticidad TINYINT
AS

INSERT INTO dbo.Bitacora (Cod_Usuario,Cod_Evento,FechaEvento, Criticidad)
VALUES (@Cod_Usuario, @Cod_Evento, GETDATE(), @Criticidad)
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_Idioma]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_Idioma]
@CodIdioma VARCHAR(2),
@DescripcionIdioma VARCHAR(50)
AS
INSERT INTO dbo.Idioma(CodIdioma, DescripcionIdioma)
SELECT @CodIdioma, @DescripcionIdioma 
WHERE NOT EXISTS (SELECT 1 
				  FROM dbo.Idioma 
				  WHERE CodIdioma = @CodIdioma AND 
						DescripcionIdioma =@DescripcionIdioma)

INSERT INTO dbo.IdiomaTraduccion (IdIdiomaOriginal, IdIdiomaTraduccion, DescripcionIdiomaTraducido)
SELECT i.IdIdioma AS IdIdiomaOriginal, i2.IdIdioma AS IdIdiomaTraduccion, NULL AS DescripcionIdiomaTraducido
FROM dbo.Idioma i
JOIN dbo.Idioma i2 ON 1=1
LEFT JOIN dbo.IdiomaTraduccion it ON i.IdIdioma = it.IdIdiomaOriginal AND
									 i2.IdIdioma =it.IdIdiomaTraduccion
WHERE it.IdIdiomaOriginal IS NULL

UPDATE it
SET DescripcionIdiomaTraducido = @DescripcionIdioma
FROM dbo.IdiomaTraduccion it
JOIN dbo.Idioma i ON it.IdIdiomaOriginal = i.IdIdioma AND
					 i.CodIdioma = @CodIdioma
WHERE it.IdIdiomaTraduccion = 1 AND it.DescripcionIdiomaTraducido IS NULL
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_Permiso]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_Permiso]
@Nombre VARCHAR(100)
AS

INSERT INTO dbo.Permiso(Nombre) VALUES (@Nombre)
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_PermisoPorTipoUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_PermisoPorTipoUsuario]
@Cod_TipoUsuario TINYINT,
@Cod_Permiso INT
AS

DELETE FROM Usuario_Permiso WHERE Cod_TipoUsuario = @Cod_TipoUsuario AND Cod_Permiso = @Cod_Permiso

INSERT INTO dbo.Usuario_Permiso (Cod_TipoUsuario, Cod_Permiso)
VALUES (@Cod_TipoUsuario, @Cod_Permiso)
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_RelacionPermisos]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_RelacionPermisos]
@Cod_Permiso_Padre INT,
@Cod_Permiso_Hijo INT
AS
SET NOCOUNT ON
/**************************** Obsoleto: *****************************/
----Evito referencias cruzadas en que @Cod_Permiso_Padre sea hijo de @Cod_Permiso_Hijo
--DELETE FROM Permiso_Permiso WHERE Cod_Permiso_Padre = @Cod_Permiso_Hijo 
--							  AND Cod_Permiso_Hijo = @Cod_Permiso_Padre
----Quito al hijo de otra familia
--DELETE FROM Permiso_Permiso WHERE Cod_Permiso_Hijo = @Cod_Permiso_Hijo
--Asigno al hijo a la nueva familia
/********************************************************************/
CREATE TABLE #Ancestros (Cod_Permiso_Hijo INT, Cod_Permiso_Padre INT, NombreDelPadre VARCHAR(100))

BEGIN
--Para evitar las referencias cruzadas:
--Debo evitar que el nuevo hijo exista como padre o ancestro (subsiguientes padres) del padre
--Acá me traigo toda la rama de ancestros del padre para luego ver que no aparezca el hijo
--Recursivo: Me traigo el padre directo, luego el padre de este, 
--			 y así hasta que ya no hayan padres
WITH permisosPadres AS
    (
        SELECT TOP 100 pp.Cod_Permiso_Hijo,
					   pp.Cod_Permiso_Padre,
					   p.Nombre AS NombreDelPadre
		FROM Permiso p
		LEFT JOIN Permiso_Permiso pp ON p.Cod_Permiso = pp.Cod_Permiso_Padre
		WHERE pp.Cod_Permiso_Hijo = @Cod_Permiso_Padre
        UNION ALL
        SELECT pu.Cod_Permiso_Padre AS Cod_Permiso_Hijo,
			   p.Cod_Permiso AS Cod_Permiso_Padre,
			   p.Nombre AS NombreDelPadre
        FROM permisosPadres pu
		JOIN Permiso_Permiso pp ON pu.Cod_Permiso_Padre = pp.Cod_Permiso_Hijo
        JOIN dbo.Permiso p ON pp.Cod_Permiso_Padre = p.Cod_Permiso
		WHERE pp.Cod_Permiso_Hijo = pu.Cod_Permiso_Padre
    )
INSERT INTO #Ancestros (Cod_Permiso_Hijo, Cod_Permiso_Padre, NombreDelPadre)
SELECT Cod_Permiso_Hijo, Cod_Permiso_Padre, NombreDelPadre FROM permisosPadres
END
SET NOCOUNT OFF

--Cheque que el @Cod_Permiso_Hijo no exista en #Ancestros como padre
INSERT INTO dbo.Permiso_Permiso(Cod_Permiso_Padre, Cod_Permiso_Hijo)
SELECT DISTINCT @Cod_Permiso_Padre, @Cod_Permiso_Hijo 
WHERE NOT EXISTS (SELECT 1 FROM #Ancestros WHERE Cod_Permiso_Padre = @Cod_Permiso_Hijo)

DROP TABLE #Ancestros
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_Texto]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_Texto]
@CodIdioma VARCHAR(2),
@IdFrase INT,
@Texto VARCHAR(2500)
AS
INSERT INTO Texto (IdIdioma, IdFrase, Texto) 
SELECT IdIdioma, @IdFrase, @Texto FROM dbo.Idioma WHERE CodIdioma = @CodIdioma
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_TipoUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_TipoUsuario]
@DescripcionTipo VARCHAR(20)
AS

DECLARE @Cod_Tipo TINYINT
SELECT @Cod_Tipo = ISNULL(MAX(Cod_Tipo),0) + 1 FROM dbo.TipoUsuario

INSERT INTO dbo.TipoUsuario(Cod_Tipo, DescripcionTipo)
SELECT @Cod_Tipo, @DescripcionTipo 
WHERE NOT EXISTS (SELECT 1 
				  FROM dbo.TipoUsuario 
				  WHERE DescripcionTipo = @DescripcionTipo)
GO
/****** Object:  StoredProcedure [dbo].[pr_Insertar_Usuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insertar_Usuario]
@Apellido VARCHAR(70),
@Nombre VARCHAR(70),
@Alias VARCHAR(70),
@Contrasenia VARCHAR(MAX),
@Cod_Tipo TINYINT,
@Telefono VARCHAR(30) = NULL,
@Mail VARCHAR(50) = NULL,
@FechaNacimiento DATETIME,
@IntentosEquivocados SMALLINT,
@UltimoLogin DATETIME
AS
DECLARE @newCod INT

IF EXISTS (SELECT 1 FROM Usuario WHERE Alias = @Alias AND Inactivo = 1)
BEGIN --Lo reactivo
	SELECT @newCod = Cod_Usuario FROM Usuario WHERE Alias = @Alias
	UPDATE Usuario SET Apellido = @Apellido, Nombre = @Nombre, 
					   [Contraseña] = ENCRYPTBYPASSPHRASE('enigma', @Contrasenia),
					   Cod_Tipo = @Cod_Tipo, Telefono = @Telefono, Mail = @Mail,
					   FechaNacimiento = @FechaNacimiento, DVH = 0, Inactivo = 0,
					   IntentosEquivocados = @IntentosEquivocados, 	UltimoLogin = @UltimoLogin
	WHERE Alias = @Alias
END
ELSE
BEGIN --Lo inserto
	SELECT @newCod = ISNULL(MAX(Cod_Usuario),0) + 1 FROM dbo.USUARIO

	INSERT INTO dbo.Usuario(Cod_Usuario, Apellido, Nombre, Alias, [Contraseña],
							Cod_Tipo, Telefono, Mail, FechaNacimiento, IntentosEquivocados, UltimoLogin, DVH)
	VALUES (@newCod, @Apellido, @Nombre, @Alias, ENCRYPTBYPASSPHRASE('enigma', @Contrasenia), 
			@Cod_Tipo, @Telefono, @Mail, @FechaNacimiento, 0, NULL, 0)

	INSERT INTO dbo.UsuarioIdioma (IdIdioma, Cod_Usuario) VALUES (1, @newCod)
END

SELECT @newCod AS Cod_Usuario, @Apellido AS Apellido, @Nombre AS Nombre, @Alias AS Alias, 
	   @Contrasenia AS [Contraseña], @Cod_Tipo AS Cod_Tipo, @Telefono AS Telefono, 
	   @Mail AS Mail, @FechaNacimiento AS FechaNacimiento, 
	   @IntentosEquivocados AS IntentosEquivocados, @UltimoLogin AS UltimoLogin
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_Bitacora]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_Bitacora]
AS
SELECT b.Cod_Usuario,u.Apellido + ', ' + u.Nombre AS NombreUsuario, b.Cod_Evento, b.FechaEvento, b.Criticidad
FROM dbo.Bitacora b
	JOIN dbo.Usuario u ON b.Cod_Usuario = u.Cod_Usuario
ORDER BY FechaEvento DESC
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_BitacoraXUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_BitacoraXUsuario]
@Cod_Usuario INT
AS
SELECT b.Cod_Usuario,u.Apellido + ', ' + u.Nombre AS NombreUsuario, b.Cod_Evento, b.FechaEvento, b.Criticidad
FROM dbo.Bitacora b
	JOIN dbo.Usuario u ON b.Cod_Usuario = u.Cod_Usuario
WHERE b.Cod_Usuario = @Cod_Usuario
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_Idiomas]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_Idiomas]
@IdIdiomaTraduccion INT = 0
AS
BEGIN
	IF (@IdIdiomaTraduccion = 0)
	 BEGIN
	 	SELECT IdIdioma, CodIdioma, DescripcionIdioma
	 	FROM dbo.Idioma
	 	ORDER BY DescripcionIdioma ASC
	 END
	ELSE
	 BEGIN
		SELECT IdIdioma, CodIdioma, it.DescripcionIdiomaTraducido AS DescripcionIdioma
	 	FROM dbo.Idioma i
		LEFT JOIN dbo.IdiomaTraduccion it ON i.IdIdioma = it.IdIdiomaOriginal
		WHERE it.IdIdiomaTraduccion = @IdIdiomaTraduccion
	 	ORDER BY DescripcionIdioma ASC
	 END	
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_IdiomasNombresSinTraducir]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_IdiomasNombresSinTraducir]
AS
BEGIN
	SELECT orig.DescripcionIdioma AS Texto, dest.CodIdioma AS CodIdiomaDestino, 
		   it.IdIdiomaOriginal, it.IdIdiomaTraduccion
	FROM IdiomaTraduccion it
	JOIN Idioma orig ON it.IdIdiomaOriginal = orig.IdIdioma
	JOIN Idioma dest ON it.IdIdiomaTraduccion = dest.IdIdioma
	WHERE DescripcionIdiomaTraducido IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_Permisos]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_Permisos]
AS
BEGIN
	SELECT DISTINCT pp.Cod_Permiso_Hijo AS Cod_Permiso,ph.Nombre
	FROM Permiso p
	JOIN Permiso_Permiso pp ON p.Cod_Permiso = pp.Cod_Permiso_Padre
	JOIN Permiso ph ON pp.Cod_Permiso_Hijo = ph.Cod_Permiso
	UNION
	SELECT DISTINCT p.Cod_Permiso,p.Nombre
	FROM Permiso p
	LEFT JOIN Permiso_Permiso pp ON p.Cod_Permiso = pp.Cod_Permiso_Padre
	ORDER BY Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_PermisosPorPadre]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ariel Pauloni
-- Create date: 30/05/2020
-- Description:	Si Cod_permiso es cero traigo los nodos que NO tienen padres.
--				Sino, trae los hijos directos del permiso Cod_permiso
-- =============================================
CREATE PROCEDURE [dbo].[pr_Listar_PermisosPorPadre]
@Cod_permiso int
AS
BEGIN
	IF (@Cod_permiso = 0)
	BEGIN
		--Permisos sin padres.
		SELECT p.Cod_Permiso, p.Nombre
		FROM [dbo].[Permiso] p
		LEFT JOIN [dbo].[Permiso_Permiso] pp ON p.Cod_Permiso = pp.Cod_Permiso_Hijo
		WHERE Cod_Permiso_Padre IS NULL
		ORDER BY p.Nombre ASC
	END
	ELSE
	BEGIN
		--Hijos directos del permiso.
		SELECT p.Cod_Permiso, p.Nombre
		FROM [dbo].[Permiso] p
		JOIN [dbo].[Permiso_Permiso] pp ON p.Cod_Permiso = pp.Cod_Permiso_Hijo
		WHERE pp.Cod_Permiso_Padre = @Cod_permiso
		ORDER BY p.Nombre ASC
	END
END


GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_PermisosPorTipoUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_PermisosPorTipoUsuario]
@TipoUsuario tinyint
AS
BEGIN
    WITH permisosPorUsuario AS
    (
        SELECT TOP 100 p.Cod_Permiso AS Cod_Permiso, NULL AS Cod_Permiso_Padre,p.Nombre
		FROM Permiso p
		LEFT JOIN Permiso_Permiso pp ON p.Cod_Permiso = pp.Cod_Permiso_Padre
		JOIN Usuario_Permiso up ON p.Cod_Permiso = up.Cod_Permiso
		JOIN TipoUsuario tu ON up.Cod_TipoUsuario = tu.Cod_Tipo
		WHERE tu.Cod_Tipo = @TipoUsuario
        UNION ALL
        SELECT h.Cod_Permiso, pu.Cod_Permiso, h.Nombre
        FROM permisosPorUsuario pu
		JOIN Permiso_Permiso pp ON pu.Cod_Permiso = pp.Cod_Permiso_Padre
        JOIN dbo.Permiso h ON pp.Cod_Permiso_Hijo = h.Cod_Permiso
    )
	SELECT DISTINCT TOP 100 Cod_Permiso, Cod_Permiso_Padre, Nombre
    FROM permisosPorUsuario
    ORDER BY Nombre
END





	    

GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_TextosPorIdioma]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_TextosPorIdioma]
@IdIdioma SMALLINT
AS
BEGIN
	SELECT IdFrase, Texto
	FROM dbo.Texto
	WHERE IdIdioma = @IdIdioma
	ORDER BY IdFrase ASC
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_TiposUsuario]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_TiposUsuario]
AS
SELECT Cod_Tipo, DescripcionTipo
FROM dbo.TipoUsuario
ORDER BY Cod_Tipo
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_UsuarioLogin]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_UsuarioLogin]
@Alias VARCHAR(70),
@Contrasenia VARCHAR(MAX)
AS
SELECT u.Cod_Usuario, u.Apellido, u.Nombre, u.Alias, CAST(DECRYPTBYPASSPHRASE('enigma',[Contraseña]) AS VARCHAR(100)) AS [Contraseña], 
	   u.Cod_Tipo, tu.DescripcionTipo, u.DVH, u.Telefono, u.Mail,
	   u.FechaNacimiento, u.Inactivo, u.IntentosEquivocados, u.UltimoLogin,
	   i.IdIdioma, i.CodIdioma, i.DescripcionIdioma
FROM dbo.Usuario u
	JOIN dbo.TipoUsuario tu ON u.Cod_Tipo = tu.Cod_Tipo
	LEFT JOIN UsuarioIdioma ui ON u.Cod_Usuario = ui.Cod_Usuario
	LEFT JOIN Idioma i ON ui.IdIdioma = i.IdIdioma
WHERE u.Alias = @Alias AND 
	  CAST(DECRYPTBYPASSPHRASE('enigma',[Contraseña]) AS VARCHAR(100)) = @Contrasenia
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_UsuarioPorAlias]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_UsuarioPorAlias]
@Alias VARCHAR(70)
AS
SELECT u.Cod_Usuario, u.Apellido, u.Nombre, u.Alias, CAST(DECRYPTBYPASSPHRASE('enigma',[Contraseña]) AS VARCHAR(100)) AS [Contraseña], 
	   u.Cod_Tipo, tu.DescripcionTipo, u.DVH, u.Telefono, u.Mail,
	   u.FechaNacimiento, u.Inactivo, u.IntentosEquivocados, u.UltimoLogin,
	   i.IdIdioma, i.CodIdioma, i.DescripcionIdioma
FROM dbo.Usuario u
	JOIN dbo.TipoUsuario tu ON u.Cod_Tipo = tu.Cod_Tipo
	LEFT JOIN UsuarioIdioma ui ON u.Cod_Usuario = ui.Cod_Usuario
	LEFT JOIN Idioma i ON ui.IdIdioma = i.IdIdioma
WHERE u.Alias = @Alias
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_UsuarioPorCod]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_UsuarioPorCod]
@Cod_Usuario INT
AS
SELECT u.Cod_Usuario, u.Apellido, u.Nombre, u.Alias, CAST(DECRYPTBYPASSPHRASE('enigma',[Contraseña]) AS VARCHAR(100)) AS [Contraseña], 
	   u.Cod_Tipo, tu.DescripcionTipo, u.DVH, u.Telefono, u.Mail,
	   u.FechaNacimiento, u.Inactivo, u.IntentosEquivocados, u.UltimoLogin,
	   i.IdIdioma, i.CodIdioma, i.DescripcionIdioma
FROM dbo.Usuario u
	JOIN dbo.TipoUsuario tu ON u.Cod_Tipo = tu.Cod_Tipo
	LEFT JOIN UsuarioIdioma ui ON u.Cod_Usuario = ui.Cod_Usuario
	LEFT JOIN Idioma i ON ui.IdIdioma = i.IdIdioma
WHERE u.Cod_Usuario = @Cod_Usuario
GO
/****** Object:  StoredProcedure [dbo].[pr_Listar_Usuarios]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Listar_Usuarios]
AS
SELECT u.Cod_Usuario, u.Apellido, u.Nombre, u.Alias, CAST(DECRYPTBYPASSPHRASE('enigma',[Contraseña]) AS VARCHAR(100)) AS [Contraseña], 
	   u.Cod_Tipo, tu.DescripcionTipo, u.DVH, u.Telefono, u.Mail,
	   u.FechaNacimiento, u.Inactivo, u.IntentosEquivocados, u.UltimoLogin,
	   i.IdIdioma, i.CodIdioma, i.DescripcionIdioma
FROM dbo.Usuario u
	JOIN dbo.TipoUsuario tu ON u.Cod_Tipo = tu.Cod_Tipo
	LEFT JOIN UsuarioIdioma ui ON u.Cod_Usuario = ui.Cod_Usuario
	LEFT JOIN Idioma i ON ui.IdIdioma = i.IdIdioma
GO
/****** Object:  StoredProcedure [dbo].[pr_Obtener_DigitoVerificador]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Obtener_DigitoVerificador]
@Tabla VARCHAR(50)
AS
BEGIN
	SELECT SumaDVH FROM DigitoVerificador WHERE Tabla = @Tabla
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Reseed_Identity]    Script Date: 01/10/2021 2:52:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Reseed_Identity]
@tblName VARCHAR(100)
AS
BEGIN
	DECLARE @columnName VARCHAR(70) = ''
	DECLARE @exec VARCHAR(250) = ''
	DECLARE @maxId INT = 0

	SELECT @columnName = [name] FROM sys.columns 
	WHERE [object_id] = OBJECT_ID('dbo.' + @tblName) 
	  AND is_identity = 1;
	
	SELECT @exec = 'SELECT ISNULL(MAX(' + @columnName + '),0) FROM dbo.' + @tblName
	
	CREATE TABLE #result (maxRow INT)
	INSERT INTO #result (maxRow)
	EXEC(@exec)
	SELECT @maxId = maxRow FROM #result
	
	DBCC CHECKIDENT (@tblName, RESEED, @maxId)
	
	DROP TABLE #result
END
GO

PRINT 'Fin de script'
GO
