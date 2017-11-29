create table dbo.tCategoryPresent
(
    CategoryPresentId int identity(0, 1),
    CategoryName nvarchar(32) not null,
	Link nvarchar(max) not null

    constraint PK_tCategoryPresent_CategoryPresentId primary key(CategoryPresentId),
    constraint CK_tPresents_PresentName check(CategoryName <> N''),
);
insert into dbo.tCategoryPresent(CategoryName, Link)
               values(N'Informatique & high-tech', N'https://www.amazon.fr/idee-cadeau-hightech/b?ie=UTF8&node=2174081031');
insert into dbo.tCategoryPresent(CategoryName, Link)
               values(N'Jeux & jouets', N'https://www.fnac.com/enfants.asp');
insert into dbo.tCategoryPresent(CategoryName, Link) 
			   values(N'Livre', N'https://livre.fnac.com/s127468/Idees-cadeaux-Livres');
insert into dbo.tCategoryPresent(CategoryName, Link) 
               values(N'Maison & Déco', N'https://www.ideecadeau.fr/cadeau-deco-et-interieur');
insert into dbo.tCategoryPresent(CategoryName, Link) 
               values(N'Nourriture & boissons', N'https://www.bienmanger.com/1C235_Cadeaux_Gourmands.html');
insert into dbo.tCategoryPresent(CategoryName, Link) 
               values(N'Santé & Beauté', N's52EALw_wcB');
insert into dbo.tCategoryPresent(CategoryName, Link) 
               values(N'Sport & loisirs', N'https://www.cadeaux.com/idee-cadeau-sport-loisir-ado-r133700.html');
insert into dbo.tCategoryPresent(CategoryName, Link) 
               values(N'Vêtements & accessoires', N'https://www.ideecadeau.fr/cadeau-personnalisable/vetement-et-accessoire');
