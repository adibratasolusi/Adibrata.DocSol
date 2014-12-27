CREATE PROCEDURE [dbo].[spMenuTreeListForm]
AS
Set NoCount On 
Declare @CurrentLevel BigInt
Select FormName From MS_Form with (nolock) 
where not exists (select 1 from Ms_menu where Ms_Menu.FormId = Ms_Form.ID) 
and Ms_form.ISActive = 1
