USE [BW_BE_S3_Ecommerce]
GO

/****** Object:  Table [dbo].[Utente]    Script Date: 19/02/2024 12:19:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Utente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Cognome] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[RuoloId] [int] NULL,
 CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Utente] ADD  CONSTRAINT [DF_Utente_RuoloId]  DEFAULT ((2)) FOR [RuoloId]
GO

ALTER TABLE [dbo].[Utente]  WITH CHECK ADD  CONSTRAINT [FK_Utente_Ruolo] FOREIGN KEY([RuoloId])
REFERENCES [dbo].[Ruolo] ([Id])
GO

ALTER TABLE [dbo].[Utente] CHECK CONSTRAINT [FK_Utente_Ruolo]
GO

