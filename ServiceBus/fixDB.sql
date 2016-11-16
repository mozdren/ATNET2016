select * from Campaigns

alter table Campaigns alter column Discount decimal;

ALTER table dbo.ProductTypes alter column [Type] VARCHAR(50);
GO
ALTER table dbo.ProductTypes add constraint UniqueName unique (Type);