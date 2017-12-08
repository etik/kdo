create view dbo.vContact
as
select
	c.ContactId,
	c.FirstEmail,
	c.SecondEmail
from dbo.tContact c
where c.ContactId > 0







		

