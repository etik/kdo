create procedure dbo.sUserDelete
(
	@UserId int
)
as
begin
	delete from dbo.tPasswordUser where UserId = @UserId;
	delete from dbo.tGoogleUser where UserId = @UserId;
    delete from dbo.tGithubUser where UserId = @UserId;
    delete from dbo.tUser where UserId = @UserId;
	return 0;

	end;

		

