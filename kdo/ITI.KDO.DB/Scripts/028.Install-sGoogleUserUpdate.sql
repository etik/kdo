create procedure dbo.sGoogleUserUpdate
(
	@UserId       int,
	@GoogleId	  varchar(32),
	@RefreshToken varchar(64)
)
as
begin
	update dbo.tGoogleUser set RefreshToken = @RefreshToken where GoogleId = @GoogleId;
	return 0;
end;