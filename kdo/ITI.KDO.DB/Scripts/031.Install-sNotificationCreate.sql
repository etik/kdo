create procedure dbo.sNotificationCreate
(
	@UserId int,
    @RecipientsEmail nvarchar(32),
    @SenderEmail nvarchar(32),
    @Descriptions nvarchar,
    @InviteAccept bit
)
as
begin
	insert into dbo.tNotification(UserId , RecipientsEmail , SenderEmail , Descriptions , InviteAccept)
	                       values(@UserId, @RecipientsEmail, @SenderEmail, @Descriptions, 0);
	return 0;
end
