create procedure dbo.sNotificationUpdate
(
    @NotificationId  int,
    @RecipientsEmail nvarchar,
    @SenderEmail     nvarchar,
    @Descriptions    nvarchar,
    @InviteAccept    bit
)
as
begin
	update dbo.tNotification
	set RecipientsEmail = @RecipientsEmail,
		SenderEmail = @SenderEmail,
		Descriptions = @Descriptions,
		InviteAccept = @InviteAccept
	where NotificationId = @NotificationId;
	return 0;
end;