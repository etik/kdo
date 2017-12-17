create procedure dbo.sPresentSuggestUpdate
(
    @EventId int,
	@UserId int,
	@DateSuggest date,
	@PresentSuggest nvarchar(128)
)
as
begin
	update dbo.tPresentSuggest
	set		DateSuggest = @DateSuggest,
			PresentSuggest = @PresentSuggest
	where	EventId = @EventId and UserId = @UserId;
	return 0;
end;