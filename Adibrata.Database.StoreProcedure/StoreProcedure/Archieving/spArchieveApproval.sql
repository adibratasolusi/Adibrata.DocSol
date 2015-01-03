CREATE PROCEDURE [dbo].[spArchieveApproval] 
    -- Add the parameters for the stored procedure here
    @DocTransCode Varchar(50),
    @Status Varchar(2)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    if @Status = '1'
    begin
    
    update DocTrans set ArchiveStatus = 'ARCHIEVE' where Id = @DocTransCode
    end

    if @Status = '2'
    
    begin
    
    update DocTrans set ArchiveStatus = 'HOLD' where  Id = @DocTransCode
    end

    if @Status = '3'
    
    begin
    
    update DocTrans set ArchiveStatus = 'REJECT' where  Id = @DocTransCode
    end

	else 
	
	---------------For button in queue--- 

	 if @Status = 'A'
    begin
    
    update DocTrans set ArchiveStatus = 'ARCHIEVE' where DocTransCode = @DocTransCode
    end

    if @Status = 'H'
    
    begin
    
    update DocTrans set ArchiveStatus = 'HOLD' where  DocTransCode = @DocTransCode
    end

    if @Status = 'R'
    
    begin
    
    update DocTrans set ArchiveStatus = 'REJECT' where  DocTransCode = @DocTransCode
    end
    -- Insert statements for procedure here
END
