CREATE PROCEDURE [dbo].[spDocTransCheckIn]
    -- Add the parameters for the stored procedure here
    @DocTransId bigint,
    @Usr varchar(50)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    update DocTrans set DocTransStatus = 'ACTIVE', CheckOutBy = '', UsrUpd = @usr, DtmUpd = getdate() where Id = @DocTransId
END