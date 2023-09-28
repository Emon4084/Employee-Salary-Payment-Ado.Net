USE [PayrollSystemDB]
GO

/****** Object:  Table [dbo].[tblPayrecords]    Script Date: 4/17/2023 4:34:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPayrecords](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[PayPeriod] [nvarchar](50) NOT NULL,
	[PayMonth] [nvarchar](50) NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[ContractualHours] [decimal](18, 2) NOT NULL,
	[TotalHours] [decimal](18, 2) NOT NULL,
	[ContactualEarnings] [money] NOT NULL,
	[OvertimeEarnings] [money] NOT NULL,
	[TotalEarnings] [money] NOT NULL,
	[NeyPay] [money] NOT NULL,
 CONSTRAINT [PK_tblPayrecords] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblPayrecords]  WITH CHECK ADD  CONSTRAINT [FK_tblPayrecords_tblEmployee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[tblEmployee] ([EmployeeID])
GO

ALTER TABLE [dbo].[tblPayrecords] CHECK CONSTRAINT [FK_tblPayrecords_tblEmployee]
GO

