create view dbo.vNotification
as
	select
        NotificationId = n.NotificationId,
		UserId = n.UserId,
		RecipientsEmail = n.RecipientsEmail,
		SenderEmail = n.SenderEmail,
		Descriptions = n.Descriptions,
		InviteAccept = n.InviteAccept

	from dbo.tNotification n
	where n.NotificationId <> 0;