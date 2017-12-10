create view dbo.vFacebookContact
as
	select
		ContactId = c.ContactId,
        UserId = c.UserId,
        FriendFacebookId = c.FriendFacebookId,
		Email = c.Email,
		FirstName = c.FirstName,
		LastName = c.LastName,
		BirthDate = c.BirthDate,
		Phone = c.Phone
		
	from dbo.tFacebookContact c
		left outer join dbo.tFacebookContactInfo i on i.FacebookId = c.FriendFacebookId
	where c.UserId <> 0;