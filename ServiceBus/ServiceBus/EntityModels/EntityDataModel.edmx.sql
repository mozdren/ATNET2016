
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/08/2016 22:29:59
-- Generated from EDMX file: D:\Visual Studio workplace\ATNET2016\ServiceBus\ServiceBus\EntityModels\EntityDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
--USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AddressOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_AddressOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_BasketBasketItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BasketItems] DROP CONSTRAINT [FK_BasketBasketItem];
GO
IF OBJECT_ID(N'[dbo].[FK_BasketCampaignItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CampaignItems] DROP CONSTRAINT [FK_BasketCampaignItem];
GO
IF OBJECT_ID(N'[dbo].[FK_BillingInformationAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingInformations] DROP CONSTRAINT [FK_BillingInformationAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_BillingInformationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_BillingInformationOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_CampaignCampaignItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CampaignItems] DROP CONSTRAINT [FK_CampaignCampaignItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CampaignProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Campaigns] DROP CONSTRAINT [FK_CampaignProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderBasket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_OrderBasket];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderStatusOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_OrderStatusOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductBasketItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BasketItems] DROP CONSTRAINT [FK_ProductBasketItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductRepair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_ProductRepair];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ProductReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStorageItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorageItems] DROP CONSTRAINT [FK_ProductStorageItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StorageAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Storages] DROP CONSTRAINT [FK_StorageAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_StorageRepair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_StorageRepair];
GO
IF OBJECT_ID(N'[dbo].[FK_StorageReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_StorageReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_StorageStorageItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StorageItems] DROP CONSTRAINT [FK_StorageStorageItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_UserOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRepair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repairs] DROP CONSTRAINT [FK_UserRepair];
GO
IF OBJECT_ID(N'[dbo].[FK_UserReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_UserReservation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[BasketItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BasketItems];
GO
IF OBJECT_ID(N'[dbo].[Baskets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Baskets];
GO
IF OBJECT_ID(N'[dbo].[BillingInformations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillingInformations];
GO
IF OBJECT_ID(N'[dbo].[CampaignItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CampaignItems];
GO
IF OBJECT_ID(N'[dbo].[Campaigns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Campaigns];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[OrderStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderStatus];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[Repairs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Repairs];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[StorageItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StorageItems];
GO
IF OBJECT_ID(N'[dbo].[Storages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Storages];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Price] float  NULL,
    [Enabled] bit  NOT NULL,
    [Headliner] bit  NOT NULL,
    [ProductType_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Baskets'
CREATE TABLE [dbo].[Baskets] (
    [Id] uniqueidentifier  NOT NULL,
    [CampaignId] uniqueidentifier  NULL,
    [TotalPrice] float  NOT NULL
);
GO

-- Creating table 'Campaigns'
CREATE TABLE [dbo].[Campaigns] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Discount] float  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Active] bit  NOT NULL,
    [ProductType_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'BasketItems'
CREATE TABLE [dbo].[BasketItems] (
    [ProductId] uniqueidentifier  NOT NULL,
    [BasketId] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Salt] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BillingInformations'
CREATE TABLE [dbo].[BillingInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [BIC] nvarchar(max)  NULL,
    [IBAN] nvarchar(max)  NULL,
    [Address_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] uniqueidentifier  NOT NULL,
    [PostCode] nvarchar(max)  NOT NULL,
    [HouseNumber] int  NOT NULL,
    [HouseNumberExtension] nvarchar(max)  NULL,
    [Street] nvarchar(max)  NOT NULL,
    [District] nvarchar(max)  NULL,
    [DoorNumber] nvarchar(max)  NULL,
    [City] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderDate] datetime  NULL,
    [DeliveryDate] datetime  NULL,
    [AddressId] uniqueidentifier  NULL,
    [BillingInformationId] uniqueidentifier  NULL,
    [UserId] uniqueidentifier  NULL,
    [InvoiceNr] nvarchar(max)  NULL,
    [OrderStatusId] uniqueidentifier  NOT NULL,
    [CustNotes] nvarchar(max)  NOT NULL,
    [OrderNumber] nvarchar(max)  NOT NULL,
    [Basket_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] uniqueidentifier  NOT NULL,
    [Count] int  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [StorageId] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Repairs'
CREATE TABLE [dbo].[Repairs] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [Serial] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [StorageId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Storages'
CREATE TABLE [dbo].[Storages] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'StorageItems'
CREATE TABLE [dbo].[StorageItems] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [StorageId] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'CampaignItems'
CREATE TABLE [dbo].[CampaignItems] (
    [Id] uniqueidentifier  NOT NULL,
    [BasketId] uniqueidentifier  NOT NULL,
    [CampaignId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderStatus'
CREATE TABLE [dbo].[OrderStatus] (
    [Id] uniqueidentifier  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Baskets'
ALTER TABLE [dbo].[Baskets]
ADD CONSTRAINT [PK_Baskets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Campaigns'
ALTER TABLE [dbo].[Campaigns]
ADD CONSTRAINT [PK_Campaigns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BasketItems'
ALTER TABLE [dbo].[BasketItems]
ADD CONSTRAINT [PK_BasketItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillingInformations'
ALTER TABLE [dbo].[BillingInformations]
ADD CONSTRAINT [PK_BillingInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [PK_Repairs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Storages'
ALTER TABLE [dbo].[Storages]
ADD CONSTRAINT [PK_Storages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorageItems'
ALTER TABLE [dbo].[StorageItems]
ADD CONSTRAINT [PK_StorageItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CampaignItems'
ALTER TABLE [dbo].[CampaignItems]
ADD CONSTRAINT [PK_CampaignItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderStatus'
ALTER TABLE [dbo].[OrderStatus]
ADD CONSTRAINT [PK_OrderStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductId] in table 'BasketItems'
ALTER TABLE [dbo].[BasketItems]
ADD CONSTRAINT [FK_ProductBasketItem]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductBasketItem'
CREATE INDEX [IX_FK_ProductBasketItem]
ON [dbo].[BasketItems]
    ([ProductId]);
GO

-- Creating foreign key on [BasketId] in table 'BasketItems'
ALTER TABLE [dbo].[BasketItems]
ADD CONSTRAINT [FK_BasketBasketItem]
    FOREIGN KEY ([BasketId])
    REFERENCES [dbo].[Baskets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BasketBasketItem'
CREATE INDEX [IX_FK_BasketBasketItem]
ON [dbo].[BasketItems]
    ([BasketId]);
GO

-- Creating foreign key on [Address_Id] in table 'BillingInformations'
ALTER TABLE [dbo].[BillingInformations]
ADD CONSTRAINT [FK_BillingInformationAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInformationAddress'
CREATE INDEX [IX_FK_BillingInformationAddress]
ON [dbo].[BillingInformations]
    ([Address_Id]);
GO

-- Creating foreign key on [AddressId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_AddressOrder]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressOrder'
CREATE INDEX [IX_FK_AddressOrder]
ON [dbo].[Orders]
    ([AddressId]);
GO

-- Creating foreign key on [BillingInformationId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_BillingInformationOrder]
    FOREIGN KEY ([BillingInformationId])
    REFERENCES [dbo].[BillingInformations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillingInformationOrder'
CREATE INDEX [IX_FK_BillingInformationOrder]
ON [dbo].[Orders]
    ([BillingInformationId]);
GO

-- Creating foreign key on [Basket_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_OrderBasket]
    FOREIGN KEY ([Basket_Id])
    REFERENCES [dbo].[Baskets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderBasket'
CREATE INDEX [IX_FK_OrderBasket]
ON [dbo].[Orders]
    ([Basket_Id]);
GO

-- Creating foreign key on [UserId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_UserOrder]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrder'
CREATE INDEX [IX_FK_UserOrder]
ON [dbo].[Orders]
    ([UserId]);
GO

-- Creating foreign key on [ProductId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_ProductRepair]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductRepair'
CREATE INDEX [IX_FK_ProductRepair]
ON [dbo].[Repairs]
    ([ProductId]);
GO

-- Creating foreign key on [UserId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_UserReservation]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserReservation'
CREATE INDEX [IX_FK_UserReservation]
ON [dbo].[Reservations]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_UserRepair]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRepair'
CREATE INDEX [IX_FK_UserRepair]
ON [dbo].[Repairs]
    ([UserId]);
GO

-- Creating foreign key on [StorageId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_StorageReservation]
    FOREIGN KEY ([StorageId])
    REFERENCES [dbo].[Storages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorageReservation'
CREATE INDEX [IX_FK_StorageReservation]
ON [dbo].[Reservations]
    ([StorageId]);
GO

-- Creating foreign key on [StorageId] in table 'Repairs'
ALTER TABLE [dbo].[Repairs]
ADD CONSTRAINT [FK_StorageRepair]
    FOREIGN KEY ([StorageId])
    REFERENCES [dbo].[Storages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorageRepair'
CREATE INDEX [IX_FK_StorageRepair]
ON [dbo].[Repairs]
    ([StorageId]);
GO

-- Creating foreign key on [ProductId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ProductReservation]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductReservation'
CREATE INDEX [IX_FK_ProductReservation]
ON [dbo].[Reservations]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'StorageItems'
ALTER TABLE [dbo].[StorageItems]
ADD CONSTRAINT [FK_ProductStorageItem]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStorageItem'
CREATE INDEX [IX_FK_ProductStorageItem]
ON [dbo].[StorageItems]
    ([ProductId]);
GO

-- Creating foreign key on [StorageId] in table 'StorageItems'
ALTER TABLE [dbo].[StorageItems]
ADD CONSTRAINT [FK_StorageStorageItem]
    FOREIGN KEY ([StorageId])
    REFERENCES [dbo].[Storages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorageStorageItem'
CREATE INDEX [IX_FK_StorageStorageItem]
ON [dbo].[StorageItems]
    ([StorageId]);
GO

-- Creating foreign key on [Address_Id] in table 'Storages'
ALTER TABLE [dbo].[Storages]
ADD CONSTRAINT [FK_StorageAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorageAddress'
CREATE INDEX [IX_FK_StorageAddress]
ON [dbo].[Storages]
    ([Address_Id]);
GO

-- Creating foreign key on [BasketId] in table 'CampaignItems'
ALTER TABLE [dbo].[CampaignItems]
ADD CONSTRAINT [FK_BasketCampaignItem]
    FOREIGN KEY ([BasketId])
    REFERENCES [dbo].[Baskets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BasketCampaignItem'
CREATE INDEX [IX_FK_BasketCampaignItem]
ON [dbo].[CampaignItems]
    ([BasketId]);
GO

-- Creating foreign key on [CampaignId] in table 'CampaignItems'
ALTER TABLE [dbo].[CampaignItems]
ADD CONSTRAINT [FK_CampaignCampaignItem]
    FOREIGN KEY ([CampaignId])
    REFERENCES [dbo].[Campaigns]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CampaignCampaignItem'
CREATE INDEX [IX_FK_CampaignCampaignItem]
ON [dbo].[CampaignItems]
    ([CampaignId]);
GO

-- Creating foreign key on [ProductType_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductProductType]
    FOREIGN KEY ([ProductType_Id])
    REFERENCES [dbo].[ProductTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductType'
CREATE INDEX [IX_FK_ProductProductType]
ON [dbo].[Products]
    ([ProductType_Id]);
GO

-- Creating foreign key on [ProductType_Id] in table 'Campaigns'
ALTER TABLE [dbo].[Campaigns]
ADD CONSTRAINT [FK_CampaignProductType]
    FOREIGN KEY ([ProductType_Id])
    REFERENCES [dbo].[ProductTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CampaignProductType'
CREATE INDEX [IX_FK_CampaignProductType]
ON [dbo].[Campaigns]
    ([ProductType_Id]);
GO

-- Creating foreign key on [OrderStatusId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_OrderStatusOrder]
    FOREIGN KEY ([OrderStatusId])
    REFERENCES [dbo].[OrderStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderStatusOrder'
CREATE INDEX [IX_FK_OrderStatusOrder]
ON [dbo].[Orders]
    ([OrderStatusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------