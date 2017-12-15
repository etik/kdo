create view dbo.vParticipant
as
	select
		UserId = p.UserId,
		EventId = p.EventId,
		ParticipantType = p.ParticipantType

	from dbo.tParticipant p

	where p.UserId <> 0;