ALTER PROCEDURE [dbo].[spDocTransApprovalView]
	@DocTransApprCode varchar(50)
AS
Set NoCount On 
Select A.DocTransApprCode, B.DocTypeCode, B.DocTransCode, C.ProjName, C.ProjType, D.CustName, D.CustCode, C.ProjCode, C.ProjName, B.Id as DocTransID
from DocTransAppr A WITH (NOLOCK)																								 
				Inner Join DocTrans B WITH (NOLOCK) on A.DocTransID = B.ID
				inner Join Proj C WITH (NOLOCK) on B.TransID = C.Id
				inner Join Cust D WITH (NOLOCK) on C.CustID = D.ID
				where A.DocTransApprCode = 	@DocTransApprCode
RETURN 0
