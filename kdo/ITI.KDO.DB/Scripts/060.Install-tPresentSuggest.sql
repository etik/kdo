create table dbo.tPresentSuggest
(
    EventId int not null,
    UserId  int not null,
	DateSuggest date,
	PresentSuggest nvarchar(128) not null

    constraint FK_tPresentSuggest_EventId foreign key(EventId) references dbo.tEvent(EventId),
    constraint FK_tPresentSuggest_UserId foreign key(UserId) references dbo.tUser(UserId),
);
insert into dbo.tPresentSuggest(EventId, UserId, DateSuggest , PresentSuggest)
                         values(0      , 0     , '0001-01-01', 'N'         );
