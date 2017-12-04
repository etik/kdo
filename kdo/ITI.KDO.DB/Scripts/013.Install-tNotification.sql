create table dbo.tNotification
(
    NotificationId int identity(0, 1),
    RecipientsEmail nvarchar(32) not null,
    SenderEmail nvarchar(32) not null,
    Descriptions nvarchar not null,
    InviteAccept bit
	
    constraint PK_tNotifications_NotificationId primary key(NotificationId)
);
insert into dbo.tNotification(RecipientsEmail    , SenderEmail        , Descriptions, InviteAccept)
					   values('nameEx@outlook.fr', 'nameEx@outlook.fr', 'N'         , 0           );