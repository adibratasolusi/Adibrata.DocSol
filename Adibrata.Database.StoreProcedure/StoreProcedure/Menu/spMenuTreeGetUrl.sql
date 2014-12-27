Create Procedure spMenuTreeGetURL
	@MenuName Varchar(50), 
	@UserLogin varchar(50) = null
	
as
Set NoCount On 
Declare @CurrentLevel BigInt
If @UserLogin is null
Begin
		Select FormURL
		From 
		(
		Select * from (
		Select FormID, MenuLevel, ParentLevel, IsForm, isnull(FormUrl,'') as FormURL,[Root],
		(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName
		From MS_menu with (nolock)
		Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
			where MS_Menu.ISActive=1)  qry1
			where MenuName = @MenuName
			) qry2
End
Else
Begin
		Select FormURL
		From 
		(
		Select * from (
		Select FormID, MenuLevel, ParentLevel, IsForm, isnull(FormUrl,'') as FormURL,[Root],
		(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName
		From MS_menu with (nolock)
		Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
			where MS_Menu.ISActive=1)  qry1
			where MenuName = @MenuName
			) qry2
END