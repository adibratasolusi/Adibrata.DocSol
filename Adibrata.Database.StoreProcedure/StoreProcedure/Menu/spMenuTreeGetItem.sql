Alter Procedure spMenuTreeGetItem
	@MenuName Varchar(50), 
	@UserLogin varchar(50) = null
	
as
Set NoCount On 
Declare @CurrentLevel BigInt
Select @CurrentLevel = Qry2.MenuLevel
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
	 
If @UserLogin is null
Begin
	Select FormID, MenuLevel, ParentLevel, IsForm, isnull(FormUrl,'') as FormURL,[Root],
	(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName

	From MS_menu with (nolock)
	Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
	 where ParentLevel = @CurrentLevel --and MS_Form.ISActive=1
	 order by MS_menu.MenuOrder
 END
 else
 BEGIN
	 Select FormID, MenuLevel, ParentLevel, IsForm, FormUrl,[Root],
	(Case When IsForm = 1 then MS_Form.FormName else MS_Menu.MenuDesc end) as MenuName

	From MS_menu with (nolock)
	Left Join MS_Form on MS_Form.ID = MS_Menu.FormID
	 where ParentLevel = @CurrentLevel --and MS_Form.ISActive=1
	 order by MS_menu.MenuOrder
 END