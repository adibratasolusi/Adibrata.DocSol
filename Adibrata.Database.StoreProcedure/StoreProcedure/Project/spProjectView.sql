CREATE PROCEDURE [dbo].[spProjectView]
@ProjCode Varchar(50)
	
AS
	SELECT Proj.Id, Proj.ProjCode, Proj.ProjName, Proj.ProjType, Cust.CustCode, Cust.CustName from proj With (nolock) Left join Cust on 
	Proj.CustID = Cust.ID 
	where ProjCode = @ProjCode
	
RETURN 0
