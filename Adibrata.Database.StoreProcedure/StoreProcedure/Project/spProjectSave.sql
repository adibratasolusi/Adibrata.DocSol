ALTER PROCEDURE [dbo].[spProjectSave]
	@CustCode Varchar(50), 
	@ProjName Varchar(50), 
	@ProjType Varchar(50), 
	@UsrCrt Varchar(50), 
	@DtmCrt smallDateTime,
	@ProjCode varchar(50) = null
AS
set nocount on 
Declare @CustID BiGINT
Select @CustID = ID From Cust With (nolock) Where Cust.CustCode = @CustCode
Declare @PostingDate datetime
Set @PostingDate = getdate()
if @ProjCode is null
Begin
		Exec spMasterSequence 1, 'Proj', @PostingDate, @ProjCode Output

		Insert Into Proj (ProjCode, ProjName, ProjType, CustID, UsrCrt, DtmCrt )
		Values (@ProjCode, @ProjName, @ProjType, @CustID, @UsrCrt, getDate())
End
ELSE
BEGIN
	Update Proj Set ProjName = @ProjName, ProjType = @ProjType
	where Projcode = @ProjCode
END
RETURN 0
