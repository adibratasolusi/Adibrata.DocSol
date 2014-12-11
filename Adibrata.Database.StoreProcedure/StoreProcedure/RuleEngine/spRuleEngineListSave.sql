
CREATE PROCEDURE [dbo].[spRuleEngineListSave]
	@RuleCode varchar(20), 
	@RuleName Varchar(50), 
	@RuleFileName varchar(500), 
	@UsrCrt varchar(50), 
	@DtmCrt SmallDateTime
AS
Set NoCount On 
If Not exists (Select 1 from RuleList where RuleSchmName = @RuleName)
Begin	
	Insert Into RuleList (RuleSchmCode, RuleSchmName, RuleFileName, UsrCrt, DtmCrt) values (@RuleCode, @RuleName, @RuleFileName, @UsrCrt, @DtmCrt)
END
else
Begin
	Update RuleList Set UsrUpd = @UsrCrt, DtmUpd = @DtmCrt where RuleSchmName = @RuleName
END
RETURN 0
