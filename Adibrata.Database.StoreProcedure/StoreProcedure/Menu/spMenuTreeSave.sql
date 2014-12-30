Create Procedure spMenuTreeSave
	@FormName varchar(50) = null, 
	@ParentLevel BigInt, 
	@MenuName varchar(50) = null, 
	@MenuOrder smallint, 
	@UsrCrt varchar(50)
As
Set NoCount On 
Declare @MenuLevel BigInt
Declare @IsForm smallint
Declare @FormID BigInt

Select @MenuLevel = isnull(Max(MenuLevel),0) +1 From Ms_Menu with (nolock) 

If @FormName is Null
	set @IsForm = 0

else
Begin
	Select @FormID = ID from MS_Form with (nolock) where FormName = @FormName
	Set @IsForm = 1
End
insert Into Ms_menu (FormID, MenuLevel, ParentLevel, [Root], MenuDesc, IsForm, IsActive, MenuOrder, UsrCrt, DtmCrt)
Values (IsNull(@FormID,0),  @MenuLevel, @ParentLevel, 0, @MenuName, @IsForm, 1, @MenuOrder, @UsrCrt, Getdate())
