create procedure dbo.sEventSuggestCreate
(
	@EventId int,
	@UserId int,
	@DateSuggest date,
	@PresentSuggest nvarchar(128)
)
as
begin
	insert into dbo.tEventSuggest(EventId , UserId , DateSuggest , PresentSuggest)
	                	   values(@EventId, @UserId, @DateSuggest, @PresentSuggest);
end
