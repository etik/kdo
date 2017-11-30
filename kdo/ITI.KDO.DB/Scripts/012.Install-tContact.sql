create table dbo.tContact
(    
	FriendId int not null,
	UserId int not null,
	Invitation bit

	constraint FK_tContact_FriendId foreign key(FriendId) references dbo.tUser(UserId),
	constraint FK_tContact_UserId foreign key(UserId) references dbo.tUser(UserId),
);