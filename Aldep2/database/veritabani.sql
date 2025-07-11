USE [aldep]
GO
/****** Object:  Table [dbo].[araclar]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[araclar](
	[aracplaka] [nchar](10) NOT NULL,
	[aracmodel] [nchar](25) NULL,
	[aractur] [nchar](20) NOT NULL,
	[aracdesi] [smallint] NOT NULL,
	[kalandesi] [smallint] NULL,
	[YolaCikis] [datetime] NULL,
 CONSTRAINT [PK_araclar_1] PRIMARY KEY CLUSTERED 
(
	[aracplaka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gonderilenler]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gonderilenler](
	[aracplaka] [nchar](10) NOT NULL,
	[cikissehir] [nchar](15) NOT NULL,
	[varissehir] [nchar](15) NOT NULL,
	[cikiszaman] [datetime] NOT NULL,
	[tahminivaris] [datetime] NULL,
	[urunadi] [nchar](40) NOT NULL,
	[urunmiktar] [smallint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kullanicigiris]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kullanicigiris](
	[TCNO] [char](11) NOT NULL,
	[adsoyad] [varchar](50) NOT NULL,
	[kullaniciadi] [varchar](50) NOT NULL,
	[eposta] [varchar](50) NOT NULL,
	[sifre] [varchar](50) NOT NULL,
	[yetkitalep] [char](8) NOT NULL,
	[yetki] [tinyint] NULL,
 CONSTRAINT [PK_kullanıcıgiris] PRIMARY KEY CLUSTERED 
(
	[TCNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personel]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personel](
	[PerTC] [nchar](11) NOT NULL,
	[PerAdSoyad] [nchar](30) NOT NULL,
	[PerDTarih] [date] NOT NULL,
	[Telefon] [nvarchar](12) NOT NULL,
	[Adres] [nchar](80) NOT NULL,
	[TelsizKod] [nchar](10) NULL,
	[GrupNu] [smallint] NULL,
	[BagliKurum] [nchar](20) NULL,
	[GorevAlani] [nchar](30) NOT NULL,
 CONSTRAINT [PK_personel] PRIMARY KEY CLUSTERED 
(
	[PerTC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tahhutler]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tahhutler](
	[TId] [smallint] IDENTITY(421,1) NOT NULL,
	[Kurum] [nchar](25) NOT NULL,
	[UrunAdi] [nchar](30) NOT NULL,
	[UKategori] [nchar](15) NOT NULL,
	[UrunMiktarTur] [nchar](10) NOT NULL,
	[UrunMiktar] [smallint] NOT NULL,
	[USehir] [nchar](25) NOT NULL,
	[aciklama] [nchar](30) NULL,
 CONSTRAINT [PK_Tahhutler] PRIMARY KEY CLUSTERED 
(
	[TId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TalepF]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TalepF](
	[TKod] [int] IDENTITY(654321,1) NOT NULL,
	[TAdi] [nchar](20) NOT NULL,
	[TKategori] [nchar](15) NOT NULL,
	[TMiktarTur] [nchar](15) NOT NULL,
	[TMiktar] [smallint] NOT NULL,
	[TSehir] [nchar](15) NOT NULL,
	[TAciklama] [char](55) NULL,
	[TalepTarih] [date] NOT NULL,
	[TamamTarihi] [date] NULL,
	[KarsilananMiktar] [smallint] NULL,
 CONSTRAINT [PK_TalepF] PRIMARY KEY CLUSTERED 
(
	[TKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunKaydet]    Script Date: 21.06.2025 20:21:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunKaydet](
	[UNumara] [int] IDENTITY(123456,1) NOT NULL,
	[UAdi] [nchar](30) NOT NULL,
	[UKategori] [nchar](15) NOT NULL,
	[UMiktarTuru] [nchar](10) NOT NULL,
	[UMiktar] [smallint] NOT NULL,
	[UTarih] [date] NULL,
	[USonTarih] [date] NULL,
	[Sehir] [nchar](15) NOT NULL,
	[Aciklama] [char](55) NULL,
 CONSTRAINT [PK_UrunKaydet] PRIMARY KEY CLUSTERED 
(
	[UNumara] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
