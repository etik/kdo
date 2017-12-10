create procedure dbo.sFacebookContactUpdate
(
    @ContactId          int,
	@UserId			    int,
	@FriendFacebookId   int
)
as
begin
	update dbo.tEvent
	set 
		UserId = @UserId,
		UserFacebookId = @UserFacebookId,
		FriendFacebookId = @FriendFacebookId
	where ContactId = @ContactId;
	return 0;
end;