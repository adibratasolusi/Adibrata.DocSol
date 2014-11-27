CREATE PROCEDURE [dbo].[spCustSave]
	@CustName varchar(50), 
	@CustType varchar(1), 
	@PostingDate datetime, 
	@OfficeID int, 
	@UsrCrt Varchar(50), 
	@DtmCrt Varchar(50), 
	@CustID Bigint OutpUt
AS
set Nocount on 
Declare @CustCode varchar(50)

Exec spMasterSequence 1, 'Cust', @PostingDate, @CustCode Output
Insert Into Cust (CustCode, CustName, CustType, UsrCrt, DtmCrt)
values (@CustCode, @CustName, @CustType, @UsrCrt, @DtmCrt)

Select @CustID = ID From Cust with (nolock) Where CustCode = @CustCode

RETURN 0
