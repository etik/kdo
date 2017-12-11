create view dbo.vFacebookContact
as
	select
		ContactId = c.ContactId,
        UserId = c.UserId,
        FacebookId = c.FacebookId,
		Email = c.Email,
		FirstName = c.FirstName,
		LastName = c.LastName,
		BirthDate = c.BirthDate,
		Phone = c.Phone
		
	from dbo.tFacebookContact c
		left outer join dbo.tFacebookContactInfo i on i.FacebookId = c.FacebookId
	where c.UserId <> 0;