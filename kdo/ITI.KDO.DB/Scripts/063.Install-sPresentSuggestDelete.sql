create procedure dbo.sPresentSuggestDelete
(
    @EventId int,
	@UserId int
)
as
begin
	delete from dbo.tPresentSuggest where EventId = @EventId and UserId = @UserId
	return 0;
end;