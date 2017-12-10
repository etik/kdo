create proc dbo.sFacebookContactCreate
(
	@UserId int not null,
    @FriendFacebookId int not null,
	@Email nvarchar(128),
	@FirstName nvarchar(128),
	@LastName nvarchar(128),
	@BirthDate date,
	@Phone nvarchar(64)
)
as
begin
	insert into dbo.tFacebookContact(UserId , FriendFacebookId)
	                          values(@UserId, @FriendFacebookId);
	insert into dbo.tFacebookContactInfo(FacebookId		  , Email , FirstName , LastName , BirthDate , Phone )
								  values(@FriendFacebookId, @Email, @FirstName, @LastName, @BirthDate, @Phone);
	return scope_identity();
end



		

