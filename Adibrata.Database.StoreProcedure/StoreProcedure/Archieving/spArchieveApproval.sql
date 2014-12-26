CREATE PROCEDURE spArchieveApproval 
    -- Add the parameters for the stored procedure here
    @DocTransId bigint,
    @Status char(1)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    if @Status = '1'
    begin
    
    update DocTrans set ArchieveStatus = 'ARCHIEVE' where Id = @DocTransId
    end

    if @Status = '2'
    
    begin
    
    update DocTrans set ArchieveStatus = 'HOLD' where Id = @DocTransId
    end

    if @Status = '3'
    
    begin
    
    update DocTrans set ArchieveStatus = 'REJECT' where Id = @DocTransId
    end
    -- Insert statements for procedure here
END
GO