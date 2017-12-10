create table dbo.tFacebookContactInfo
(
	FacebookId int not null,
    Email nvarchar(64),
    FirstName nvarchar(64),
    LastName nvarchar(64),
    BirthDate date,
    Phone nvarchar(64)

	constraint FK_tFacebookContactInfo_FacebookId foreign key(FacebookId) references dbo.tFacebookContact(FriendFacebookId),
);
insert into dbo.tFacebookContact(UserId, FriendFacebookId)
                          values(0     , 0               )
insert into dbo.tFacebookContactInfo(FacebookId, Email, FirstName, LastName, BirthDate   , Phone       )
                              values(0         , 'N'  , 'N'      , 'N'     , '0001-01-01', '0123456789');