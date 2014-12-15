CREATE PROCEDURE [dbo].[spProjectSave]
	@CustCode Varchar(50), 
	@ProjName Varchar(50), 
	@ProjType Varchar(50), 
	@UsrCrt Varchar(50), 
	@DtmCrt smallDateTime
AS
set nocount on 
Declare @CustID BiGINT
Select @CustID = ID From Cust With (nolock) Where Cust.CustCode = @CustCode
Declare @PostingDate datetime, @ProjCode varchar(50)
Set @PostingDate = getdate()

Exec spMasterSequence 1, 'Proj', @PostingDate, @ProjCode Output

Insert Into Proj (ProjCode, ProjName, ProjType, CustID)
Values (@ProjCode, @ProjName, @ProjType, @CustID)

RETURN 0
