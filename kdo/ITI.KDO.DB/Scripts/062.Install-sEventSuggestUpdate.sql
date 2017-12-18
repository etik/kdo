create procedure dbo.sEventSuggestUpdate
(
    @EventId int,
	@UserId int,
	@DateSuggest date,
	@PresentSuggest nvarchar(128)
)
as
begin
	update dbo.tEventSuggest
	set		DateSuggest = @DateSuggest,
			PresentSuggest = @PresentSuggest
	where	EventId = @EventId and UserId = @UserId;
	return 0;
end;