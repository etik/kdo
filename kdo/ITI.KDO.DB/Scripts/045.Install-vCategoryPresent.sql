create view dbo.vCategoryPresent
AS
    select
        CategoryPresentId = c.CategoryPresentId,
        CategoryName = c.CategoryName,
        Link = c.Link
    from dbo.tCategoryPresent c
    where c.CategoryPresentId <> 0;