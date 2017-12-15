create procedure dbo.sParticipantCreate
(
    @UserId				int,
	@EventId			int,
	@ParticipantType    bit
)

as
begin
	insert into dbo.tParticipant(UserId, EventId, ParticipantType)
	values(@UserId, @EventId, @ParticipantType);
	return 0;
end