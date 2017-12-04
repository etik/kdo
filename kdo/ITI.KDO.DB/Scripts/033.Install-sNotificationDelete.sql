create procedure dbo.sNotificationDelete
(
	@NotificationId int
)
as
begin
    delete from dbo.tNotification where NotificationId = @NotificationId;
	return 0;
end;