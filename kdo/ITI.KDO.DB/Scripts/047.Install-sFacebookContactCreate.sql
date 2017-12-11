create proc dbo.sFacebookContactCreate
(
	@UserId int not null,
    @FacebookId int not null,
	@Email nvarchar(128),
	@FirstName nvarchar(128),
	@LastName nvarchar(128),
	@BirthDate date,
	@Phone nvarchar(64)
)
as
begin
	insert into dbo.tFacebookContact(UserId , FacebookId)
	                          values(@UserId, @FacebookId);
	insert into dbo.tFacebookContactInfo(FacebookId	, Email , FirstName , LastName , BirthDate , Phone )
								  values(@FacebookId, @Email, @FirstName, @LastName, @BirthDate, @Phone);
	return scope_identity();
end



		

