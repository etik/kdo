create procedure dbo.sFacebookContactUpdate
(
    @ContactId          int,
	@UserId			    int,
	@FacebookId   int
)
as
begin
	update dbo.tEvent
	set 
		UserId = @UserId,
		UserFacebookId = @UserFacebookId,
		FacebookId = @FacebookId
	where ContactId = @ContactId;
	return 0;
end;