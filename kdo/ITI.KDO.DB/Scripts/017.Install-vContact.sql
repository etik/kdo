create view dbo.vContact
as
select
    UserId = c.UserId,
	FriendId = c.FriendId,
	Invitation = c.Invitation

	from dbo.tContact c