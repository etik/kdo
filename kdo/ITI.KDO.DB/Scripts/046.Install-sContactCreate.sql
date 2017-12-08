create procedure dbo.sContactCreate
(
	@FirstEmail nvarchar(64),
	@SecondEmail nvarchar(64)
)
as
begin
	insert into dbo.tContact(FirstEmail , SecondEmail)
	                  values(@FirstEmail, @SecondEmail);
	return 0;
end
