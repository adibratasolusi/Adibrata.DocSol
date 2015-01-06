ALTER PROCEDURE [dbo].[spCustSave]
	@CustName varchar(50), 
	@CustType varchar(1), 
	@PostingDate datetime, 
	@OfficeID int, 
	@UsrCrt Varchar(50), 
	@DtmCrt DateTime, 
	@CustID Bigint Output,
	@IsEdit Bit = null, 
	@CustIDReff BigInt = null,
	@UsrUpd Varchar(50) = Null, 
	@DtmUpd DateTime = null
AS
set Nocount on 
Declare @CustCode varchar(50)
If @ISedit = 0 or @IsEdit is null
Begin
	Exec spMasterSequence 1, 'Cust', @PostingDate, @CustCode Output
	Insert Into Cust (CustCode, CustName, CustType, UsrCrt, DtmCrt)
	output inserted.ID
	values (@CustCode, @CustName, @CustType, @UsrCrt, @DtmCrt)

	Select @CustID = ID From Cust with (nolock) Where CustCode = @CustCode
END
Else
BEGIN
	Update Cust set CustName = @CustName,
					UsrUpd = @UsrUpd,
					DtmUpd = @DtmUpd
	WHere ID = @CustIDReff
END
RETURN 
