--USE [master]
--GO
/****** Object:  Database [CashierSystem]    Script Date: 16-04-2021 14:39:40 ******/
CREATE DATABASE [CashierSystem]
/* CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CashierSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CashierSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CashierSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CashierSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CashierSystem] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CashierSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CashierSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CashierSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CashierSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CashierSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CashierSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [CashierSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CashierSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CashierSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CashierSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CashierSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CashierSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CashierSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CashierSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CashierSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CashierSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CashierSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CashierSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CashierSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CashierSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CashierSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CashierSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CashierSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CashierSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CashierSystem] SET  MULTI_USER 
GO
ALTER DATABASE [CashierSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CashierSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CashierSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CashierSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CashierSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CashierSystem] SET QUERY_STORE = OFF
*/
GO
USE [CashierSystem]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Balance] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[IsWithDraw] [bit] NULL,
	[Amount] [int] NULL,
	[Balance] [int] NULL,
	[TransactionDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[prCreateAccount]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[prCreateAccount]
@FirstName varchar(100),
@LastName varchar(100),
@Balance int
as
begin

 insert into accounts(firstname,lastname,balance)
	select @FirstName,@LastName,@Balance
	
end
GO
/****** Object:  StoredProcedure [dbo].[prCreateTranasaction]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[prCreateTranasaction]
@AccountId varchar(100),
@Amount int,
@isWithDraw bit

as
begin

Declare @Balance int=0

select @Balance =balance from accounts where accountid=@AccountId


--select isnull(@Balance,0)+isnull(@Amount,0)
if(@isWithDraw=1)
begin

  select @Balance=isnull(@Balance,0)-isnull(@Amount,0)
end
else
 begin

  set @Balance=isnull(@Balance,0)+isnull(@Amount,0)
 end

 select @Balance

insert into transactions(accountid,iswithDraw,Amount,balance,transactiondate)
		select @AccountId,@isWithDraw,@Amount,@Balance,GETDATE()

update accounts set balance=@Balance where accountid=@AccountId

	
end
GO
/****** Object:  StoredProcedure [dbo].[prGetAccountDetails]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[prGetAccountDetails]
@AccountId int
as
begin

 select  accountid as AccountId,firstname as FirstName,lastname as LastName,balance as Balance from accounts where accountid=@AccountId
end


GO
/****** Object:  StoredProcedure [dbo].[prGetAccounts]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[prGetAccounts]
as
begin

 select  AccountId,FirstName,LastName,Balance from Accounts
end
GO
/****** Object:  StoredProcedure [dbo].[prGetAccountTransactionDetails]    Script Date: 16-04-2021 14:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[prGetAccountTransactionDetails]
@AccountId int
as
begin

 select  TransactionId,AccountId,Amount,Balance,TransactionDate,IsWithDraw from transactions where accountid=@AccountId order by TransactionDate desc
end


GO
USE [master]
GO
ALTER DATABASE [CashierSystem] SET  READ_WRITE 
GO
