create table dbo.tEventSuggest
(
    EventId int not null,
    UserId  int not null,
	DateSuggest date,
	PresentSuggest nvarchar(128) not null

    constraint FK_tEventSuggest_EventId foreign key(EventId) references dbo.tEvent(EventId),
    constraint FK_tEventSuggest_UserId foreign key(UserId) references dbo.tUser(UserId),
);

