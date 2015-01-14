
ALTER PROCEDURE [dbo].[spCustCoyView]
	@CustCode Varchar(50)
AS
set Nocount On 
	SELECT Cust.ID as CustID, Cust.CustCode, Cust.CustName, 
	isnull(CustCoy.Address, '') Address,
	 isnull(CustCoy.NPWP, '') NPWP,
	 isnull(CustCoy.SIUP,'') SIUP, 
	 isnull(CustCoy.TDP, '') TDP,
	 isnull(CustCOy.AkteNo, '') AkteNo, 
	 isnull(custCoy.RT, '') RT, 
	isnull(CustCoy.RW, '') RW, 
	isnull(CustCoy.Kelurahan, '') Kelurahan,
	isnull(CustCoy.Kecamatan, '') Kecamatan, 
	isnull(CustCoy.City, '') City, 
	isnull(CustCoy.ZipCode,		'') ZipCode
	from Cust with (nolock) left join CustCoy with (nolock) on Cust.id = CustCoy.CustID
	where Cust.CustCode = @CustCode
RETURN 0


