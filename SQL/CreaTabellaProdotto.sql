USE [BW_BE_S3_Ecommerce]
GO

/****** Object:  Table [dbo].[Prodotto]    Script Date: 19/02/2024 12:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prodotto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Descrizione] [nvarchar](300) NULL,
	[Prezzo] [decimal](12, 2) NOT NULL,
	[ImmagineUrl] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Prodotto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prodotto] ADD  CONSTRAINT [DF_Prodotto_ImmagineUrl]  DEFAULT (N'https://t4.ftcdn.net/jpg/04/73/25/49/360_F_473254957_bxG9yf4ly7OBO5I0O5KABlN930GwaMQz.jpg') FOR [ImmagineUrl]
GO

