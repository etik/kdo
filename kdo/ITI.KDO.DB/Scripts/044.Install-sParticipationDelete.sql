create procedure dbo.sParticipationDelete
(
	@UserId int,
	@EventId int
)
as
begin
	delete from dbo.tParticipation where UserId = @UserId and EventId = @EventId;
	return 0;
end;