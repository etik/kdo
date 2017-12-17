create procedure dbo.sPresentSuggestCreate
(
	@EventId int,
	@UserId int,
	@DateSuggest date,
	@PresentSuggest nvarchar(128)
)
as
begin
	insert into dbo.tPresentSuggest(EventId , UserId , DateSuggest , PresentSuggest)
	                         values(@EventId, @UserId, @DateSuggest, @PresentSuggest);
end
