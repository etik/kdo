create table dbo.tFacebookContact
(
	ContactId int identity(0, 1),
    UserId int not null,
	FacebookId int not null

	constraint PK_tFacebookContact_ContactId primary key(ContactId),
    constraint FK_tFacebookContact_UserId foreign key(UserId) references dbo.tUser(UserId),
);