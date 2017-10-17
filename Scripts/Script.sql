IF db_id('Globons') IS NULL
BEGIN
	CREATE DATABASE [Globons]
	USE [Globons]
	
	IF OBJECT_ID('dbo.Direccion', 'U') IS NULL 
	BEGIN --CREAR TABLA Direccion
		CREATE TABLE [dbo].[Direccion](
		[idDireccion] [int] IDENTITY(1,1) NOT NULL,
		[calle] [nvarchar](100) NOT NULL,
		[numero] [int] NOT NULL,
		 CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
		(
			[idDireccion] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
	END

	IF OBJECT_ID('dbo.Persona', 'U') IS NULL 
	BEGIN --CREAR TABLA Persona
		CREATE TABLE [dbo].[Persona](
			[idPersona] [int] IDENTITY(1,1) NOT NULL,
			[nombre] [nvarchar](50) NOT NULL,
			[apellido] [nvarchar](50) NOT NULL,
			[numeroDocumento] [int] NOT NULL,
			[fechaNacimiento] [date] NOT NULL,
			[direccion] [int] NOT NULL,
		 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
		(
			[idPersona] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Persona_Direccion] FOREIGN KEY([direccion])
		REFERENCES [dbo].[Direccion] ([idDireccion])

		ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Persona_Direccion]
	END
END


