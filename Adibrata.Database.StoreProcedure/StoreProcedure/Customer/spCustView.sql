CREATE PROCEDURE [dbo].[spCustCoyView]
	@CustID Bigint
AS
set Nocount On 
	SELECT Cust.ID as CustID, cust.CustName, 
		CustCoy.Address, CustCoy.NPWP,
	 CustCoy.SIUP, CustCoy.TDP, CustCOy.AkteNo, custCoy.RT, 
		CustCoy.RW, CustCoy.Kelurahan, CustCoy.Kecamatan, CustCoy.City, CustCoy.ZipCode		
	from Cust with (nolock) inner join CustCoy with (nolock) on Cust.id = CustCoy.CustID
	where Cust.Id = @CustID
	
RETURN 0
