create proc dbo.sUserCreate
(
	@FirstName nvarchar(32),
	@LastName nvarchar(32),
	@Birthdate date,
	@Email nvarchar(32),
	@Phone nvarchar(12),
	@Photo nvarchar(32)
)
as
begin
	insert into dbo.tUser(FirstName , LastName , Birthdate , Email , Phone , Photo)
	               values(@FirstName, @LastName, @Birthdate, @Email, @Phone, @Photo);
	return scope_identity();
end