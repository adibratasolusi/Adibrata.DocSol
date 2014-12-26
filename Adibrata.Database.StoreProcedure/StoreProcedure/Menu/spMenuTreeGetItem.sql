Alter Procedure spMenuTreeGetItem
	@CurrentLevel BigInt, 
	@UserLogin varchar(50) = null
	
as
Set NoCount On 
If @UserLogin is null
Begin
	Select FormID, MenuLevel, ParentLevel, IsForm, isnull(FormUrl,'') as FormURL,[Root],
	(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName

	From MS_menu with (nolock)
	Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
	 where ParentLevel = @CurrentLevel and MS_Form.ISActive=1
 END
 else
 BEGIN
	 Select FormID, MenuLevel, ParentLevel, IsForm, FormUrl,[Root],
	(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName

	From MS_menu with (nolock)
	Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
	 where ParentLevel = @CurrentLevel and MS_Form.ISActive=1
 END