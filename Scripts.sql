USE [AirWayTicketBook]
GO

/****** Object:  Table [dbo].[Flights]    Script Date: 10/18/2022 1:06:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Flights](
	[Flight_ID] [int] IDENTITY(1,1) NOT NULL,
	[DateOfArriving] [date] NOT NULL,
	[DateOfDepature] [date] NOT NULL,
	[Leave_from] [varchar](255) NULL,
	[Going_to] [varchar](255) NULL,
	[Timing] [decimal](18, 2) NULL,
	[NoSeatsAvailable] [int] NULL,
	[Rate] [money] NULL,
	[Tax] [int] NULL,
	[Charges] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[Flight_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Registration]    Script Date: 10/18/2022 1:07:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Registration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NULL,
	[Email] [varchar](35) NULL,
	[Password] [varchar](8) NULL,
	[ConfirmPassword] [varchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SearchHistory]    Script Date: 10/18/2022 1:08:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SearchHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TripType] [bit] NULL,
	[FromSource] [varchar](255) NOT NULL,
	[Destination] [varchar](255) NOT NULL,
	[DepatureDate] [date] NOT NULL,
	[ReturnTripDate] [date] NULL,
	[PassengersCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SearchHistory] ADD  DEFAULT ((0)) FOR [TripType]
GO

/****** Object:  Table [dbo].[Payment]    Script Date: 10/18/2022 1:09:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PaymentType] [int] NOT NULL,
	[PaymentDetails] [varchar](255) NOT NULL,
	[Payee] [varchar](255) NOT NULL,
	[PassengerCount] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[PaiedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [PaiedOn]
GO

ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flight_ID])
GO

ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Registration] ([ID])
GO

/****** Object:  Table [dbo].[BookingDetails]    Script Date: 10/18/2022 1:05:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookingDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentId] [int] NOT NULL,
	[PassengerName] [varchar](255) NOT NULL,
	[PassengerAge] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
GO


