create procedure dbo.sNotificationCreate
(
    @RecipientsEmail nvarchar(32),
    @SenderEmail nvarchar(32),
    @Descriptions nvarchar(max),
    @InviteAccept bit
)
as
begin
	insert into dbo.tNotification(RecipientsEmail , SenderEmail , Descriptions , InviteAccept)
	                       values(@RecipientsEmail, @SenderEmail, @Descriptions, 0           );
	return 0;
end
