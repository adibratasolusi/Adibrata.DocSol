ALTER PROCEDURE [dbo].[spMenuSearch]
	@Crit varchar(50)
AS
IF LOWER(@Crit) = 'all'
BEGIN
	SELECT FormName,FormURL from MS_Form
END
ELSE
BEGIN
	SELECT FormName,FormURL from MS_Form where FormName like '%'+@Crit+'%' or FormURL like '%'+@Crit+'%'
END
RETURN 0

select * from MS_Form

insert into MS_Form values('TestFredy','FredyAsd',1,GETDATE(),'sa',GETDATE(),'sa')
insert into MS_Form values('TestMenu','AsdAsd',1,GETDATE(),'sa',GETDATE(),'sa')