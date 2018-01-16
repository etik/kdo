create proc dbo.sUserUpdate
(
    @UserId int,
	@FirstName nvarchar(32),
	@LastName nvarchar(32),
	@Birthdate date,
	@Email nvarchar(32),
	@Phone nvarchar(12),
	@Photo varbinary(max)

)
as
begin
	update dbo.tUser
	set FirstName = @FirstName,
		LastName = @LastName,
		Birthdate = @Birthdate,
		Email = @Email,
		Phone = @Phone,
		Photo = @Photo
	where UserId = @UserId;
	return 0;
end;