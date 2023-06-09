USE [CrudAPI]
GO
/****** Object:  Table [dbo].[Claves]    Script Date: 05/04/2023 05:25:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claves](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](255) NOT NULL,
	[contraseña] [varchar](255) NOT NULL,
	[IdInfo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Informacion]    Script Date: 05/04/2023 05:25:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Informacion](
	[id] [int] NOT NULL,
	[nombre] [varchar](255) NULL,
	[apellido] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trabajo]    Script Date: 05/04/2023 05:25:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trabajo](
	[IdTrabajo] [int] IDENTITY(1,1) NOT NULL,
	[Empresa] [varchar](55) NULL,
	[Turno] [varchar](50) NULL,
	[Puesto] [varchar](100) NULL,
	[IdInformacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTrabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Claves]  WITH CHECK ADD  CONSTRAINT [FK_IdInfo] FOREIGN KEY([IdInfo])
REFERENCES [dbo].[Informacion] ([id])
GO
ALTER TABLE [dbo].[Claves] CHECK CONSTRAINT [FK_IdInfo]
GO
ALTER TABLE [dbo].[Trabajo]  WITH CHECK ADD  CONSTRAINT [FK_IdInformacion] FOREIGN KEY([IdInformacion])
REFERENCES [dbo].[Informacion] ([id])
GO
ALTER TABLE [dbo].[Trabajo] CHECK CONSTRAINT [FK_IdInformacion]
GO
