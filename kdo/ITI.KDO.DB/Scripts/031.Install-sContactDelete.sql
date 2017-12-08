create procedure dbo.sContactDelete
(
    @FriendId        int,
	@UserId			 int
)
as
begin
	delete from dbo.tContact 
	where (FriendId = @FriendId and UserId = @UserId) or 
		  (FriendId = @UserId and UserId = @FriendId);
	return 0;
end;