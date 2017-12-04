create table dbo.tNotification
(
    NotificationId int identity(0, 1),
	UserId int not null,
    RecipientsEmail nvarchar(32) not null,
    SenderEmail nvarchar(32) not null,
    Descriptions nvarchar not null,
    InviteAccept bit
	
    constraint PK_tNotifications_NotificationId primary key(NotificationId),
	constraint FK_tNotifications_UserId foreign key(UserId) references dbo.tUser(UserId),
);
insert into dbo.tNotification(UserId, RecipientsEmail    , SenderEmail        , Descriptions, InviteAccept)
					   values(0     , 'nameEx@outlook.fr', 'nameEx@outlook.fr', 'N'         , 0           );