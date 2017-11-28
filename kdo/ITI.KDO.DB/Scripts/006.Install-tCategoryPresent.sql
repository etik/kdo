create table dbo.tCategoryPresent
(
    CategoryPresentId int identity(0, 1),
    CategoryName nvarchar(32) not null,
	Link nvarchar(32) not null

    constraint PK_tCategoryPresent_CategoryPresentId primary key(CategoryPresentId),
    constraint CK_tPresents_PresentName check(CategoryName <> N''),
);
insert into dbo.tCategoryPresent(CategoryName, Link)
                          values('N'         , 'N ');
insert into dbo.tCategoryPresent(CategoryName, Link)
                          values('Bureau', 'www.azeazea');
insert into dbo.tCategoryPresent(CategoryName  , Link)
                          values('Electro-menage', 'www.azeazea');
insert into dbo.tCategoryPresent(CategoryName, Link) 
                          values('Laptop'      , 'www.azeaze');
insert into dbo.tCategoryPresent(CategoryName  , Link)
                          values('Computer', 'www.azeazea');