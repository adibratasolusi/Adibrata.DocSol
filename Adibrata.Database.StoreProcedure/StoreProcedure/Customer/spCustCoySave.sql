ALter PROCEDURE [dbo].[spCustCoySave]
	@CustID bigint, 
	@CoyAddress Varchar(100), 
	@CoyRT varchar(4), 
	@CoyRW varchar(4), 
	@CoyKelurahan varchar(50), 
	@CoyKecamatan varchar(50), 
	@CoyCity varchar (50), 
	@CoyZipCode Varchar(12), 
	@CoyNPWP varchar(20), 
	@CoySIUP varchar(50), 
	@CoyTDP varchar(50),
	@CoyNotary Varchar(50), 
	@UsrCrt varchar(50), 
	@DtmCrt Datetime
AS
Set NoCount On 
Declare @FullAddress Varchar(5000)
Set @FullAddress = @CoyAddress + ' RT/RW: '+ @CoyRT + '/' + @CoyRW + ' Kelurahan: ' + @CoyKelurahan + ' Kecamatan: ' + @CoyKecamatan 
+ ' Kota: ' + @CoyCity + ' ZipCode: ' + @CoyZipCode

	Insert into CustCoy (CustID, Address, RT, RW, Kelurahan, Kecamatan, City, ZipCode, NPWP, SIUP, TDP, AkteNo, FullAddress, UsrCrt, DtmCrt)
	values (@CustID, @CoyAddress, @CoyRT, @CoyRW, @CoyKelurahan, @CoyKecamatan, @CoyCity, @CoyZipCode, @CoyNPWP, @CoySIUP, @CoyTDP, @CoyNotary, @FullAddress, @UsrCrt, @DtmCrt)

RETURN 0
