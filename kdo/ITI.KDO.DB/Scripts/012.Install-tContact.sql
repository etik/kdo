create table dbo.tContact
(
	ContactId int identity(0, 1),
	FirstEmail nvarchar(64) not null,
	SecondEmail nvarchar(64) not null
	
	constraint PK_tContact_ContactId primary key(ContactId)
);