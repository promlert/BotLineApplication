USE [LineDB]
GO

/****** Object:  Table [dbo].[SourceState]    Script Date: 6/19/2023 12:51:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SourceState]') AND type in (N'U'))
DROP TABLE [dbo].[SourceState]
GO

/****** Object:  Table [dbo].[SourceState]    Script Date: 6/19/2023 12:51:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SourceState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[DisplayName] [varchar](50) NULL,
	[SourceType] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_UserLine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO