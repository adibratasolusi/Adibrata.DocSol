CREATE PROCEDURE [dbo].[spDocTransCheckOut]
    -- Add the parameters for the stored procedure here
    @DocTransId bigint,
    @Usr varchar(50)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    update DocTrans set DocTransStatus = 'CHECKOUT', CheckOutBy = @usr, UsrUpd = @usr, DtmUpd = GETDATE() where Id = @DocTransId
END
