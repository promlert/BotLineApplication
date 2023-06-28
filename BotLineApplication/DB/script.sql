USE [LineDB]
GO
/****** Object:  Table [dbo].[LogMessage]    Script Date: 6/28/2023 10:02:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogMessage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[Text] [varchar](300) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_LogMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceState]    Script Date: 6/28/2023 10:02:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceState](
	[UserName] [varchar](50) NOT NULL,
	[DisplayName] [varchar](50) NULL,
	[GroupName] [varchar](50) NULL,
	[Room] [varchar](50) NULL,
	[SourceType] [varchar](50) NULL,
	[Account] [varchar](50) NULL,
	[VehicleRegistration] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_UserLine] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
