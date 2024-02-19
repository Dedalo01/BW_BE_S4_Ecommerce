USE [BW_BE_S3_Ecommerce]
GO

/****** Object:  Table [dbo].[Carrello]    Script Date: 19/02/2024 12:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Carrello](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UtenteId] [int] NOT NULL,
	[Totale] [decimal](30, 2) NULL,
 CONSTRAINT [PK_Carrello] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Carrello] ADD  CONSTRAINT [DF_Carrello_Totale]  DEFAULT ((0)) FOR [Totale]
GO

ALTER TABLE [dbo].[Carrello]  WITH CHECK ADD  CONSTRAINT [FK_Carrello_Utente] FOREIGN KEY([UtenteId])
REFERENCES [dbo].[Utente] ([Id])
GO

ALTER TABLE [dbo].[Carrello] CHECK CONSTRAINT [FK_Carrello_Utente]
GO

