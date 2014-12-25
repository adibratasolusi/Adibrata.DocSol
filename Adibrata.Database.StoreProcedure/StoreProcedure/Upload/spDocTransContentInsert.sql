ALTER PROCEDURE [dbo].[spDocTransContentInsert]
	-- Add the parameters for the stored procedure here
	@DocTypeCode varchar(50),
	@DocTransId bigint,
	@ContentName varchar(50) = '',
	@ContentValue varchar(8000) = '',
	@ContentValueDate datetime = GETDATE,
	@ContentValueNumeric numeric(17,2) = 0,
	@ContentSearchTag Varchar(Max) = '', 
	@UsrCrt Varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select @ContentSearchTag = @ContentSearchTag + 
					'#Document Transaction Code ' + isnull(DocTransCode,'') + 
					'#Project Code ' + isnull(ProjCode,'') + 
					'#Customer Name ' + isnull(CustName,'')  + 
					'#Project Type ' + isnull(ProjType,'') + 
					'#Customer Code ' + isnull(CustCode,'') +
					'#Address Alamat ' + isnull(FullAddress, '')+
					'#NPWP ' + isnull(NPWP, '')+
					'#SIUP ' + isnull(SIUP, '')+
					'#TDP ' + isnull(TDP, '')+
					'#AKTE NO ' + isnull(AkteNo, '')
					from DocTrans A with (nolock) Left join proj B with (nolock) on A.TransID = B.ID 
	Left Join Cust C with (nolock) on C.id = B.CustID
	Left Join CustCoy D with (nolock) on C.id = D.CustID
	where A.ID = @DocTransId

    -- Insert statements for procedure here
	Insert into DocTransContent
	(
		DocTypeCode,
		DocTransID,
		ContentName,
		ContentValue,
		ContenValueDate,
		ContentValueNumeric,
		ContentSearchTag,
		UsrCrt,
		DtmCrt
	)
	values
	(
		@DocTypeCode,
		@DocTransId,
		@ContentName,
		@ContentValue,
		@ContentValueDate,
		@ContentValueNumeric,
		@ContentSearchTag,
		@UsrCrt,
		GETDATE()
	)
END
GO
