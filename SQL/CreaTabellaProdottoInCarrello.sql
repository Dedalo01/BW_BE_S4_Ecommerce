USE [BW_BE_S3_Ecommerce]
GO

/****** Object:  Table [dbo].[ProdottoInCarrello]    Script Date: 19/02/2024 12:18:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProdottoInCarrello](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarrelloId] [int] NULL,
	[ProdottoId] [int] NULL,
	[Quantita] [int] NULL,
 CONSTRAINT [PK_ProdottoInCarrello] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProdottoInCarrello] ADD  CONSTRAINT [DF_ProdottoInCarrello_Quantita]  DEFAULT ((1)) FOR [Quantita]
GO

ALTER TABLE [dbo].[ProdottoInCarrello]  WITH CHECK ADD  CONSTRAINT [FK_ProdottoInCarrello_Carrello] FOREIGN KEY([CarrelloId])
REFERENCES [dbo].[Carrello] ([Id])
GO

ALTER TABLE [dbo].[ProdottoInCarrello] CHECK CONSTRAINT [FK_ProdottoInCarrello_Carrello]
GO

ALTER TABLE [dbo].[ProdottoInCarrello]  WITH CHECK ADD  CONSTRAINT [FK_ProdottoInCarrello_Prodotto] FOREIGN KEY([ProdottoId])
REFERENCES [dbo].[Prodotto] ([Id])
GO

ALTER TABLE [dbo].[ProdottoInCarrello] CHECK CONSTRAINT [FK_ProdottoInCarrello_Prodotto]
GO

