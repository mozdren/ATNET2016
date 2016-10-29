
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2016 18:40:01
-- Generated from EDMX file: D:\Visual Studio workplace\ATNET2016\ServiceBus\ServiceBus\EntityDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [D:\VISUAL STUDIO WORKPLACE\ATNET2016\SERVICEBUS\SERVICEBUS\APP_DATA\DATABASE.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BasketCampaign_Basket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BasketCampaign] DROP CONSTRAINT [FK_BasketCampaign_Basket];
GO
IF OBJECT_ID(N'[dbo].[FK_BasketCampaign_Campaign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BasketCampaign] DROP CONSTRAINT [FK_BasketCampaign_Campaign];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Basket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Basket];
GO
IF OBJECT_ID(N'[dbo].[Campaign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Campaign];
GO
IF OBJECT_ID(N'[dbo].[BasketCampaign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BasketCampaign];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Price] float  NOT NULL
);
GO

-- Creating table 'Basket'
CREATE TABLE [dbo].[Basket] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Campaign'
CREATE TABLE [dbo].[Campaign] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Discount] float  NOT NULL
);
GO

-- Creating table 'BasketCampaign'
CREATE TABLE [dbo].[BasketCampaign] (
    [BasketCampaign_Campaign_Id] uniqueidentifier  NOT NULL,
    [Campaigns_Id] uniqueidentifier  NOT NULL
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

-- Creating primary key on [Id] in table 'Basket'
ALTER TABLE [dbo].[Basket]
ADD CONSTRAINT [PK_Basket]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Campaign'
ALTER TABLE [dbo].[Campaign]
ADD CONSTRAINT [PK_Campaign]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [BasketCampaign_Campaign_Id], [Campaigns_Id] in table 'BasketCampaign'
ALTER TABLE [dbo].[BasketCampaign]
ADD CONSTRAINT [PK_BasketCampaign]
    PRIMARY KEY CLUSTERED ([BasketCampaign_Campaign_Id], [Campaigns_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BasketCampaign_Campaign_Id] in table 'BasketCampaign'
ALTER TABLE [dbo].[BasketCampaign]
ADD CONSTRAINT [FK_BasketCampaign_Basket]
    FOREIGN KEY ([BasketCampaign_Campaign_Id])
    REFERENCES [dbo].[Basket]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Campaigns_Id] in table 'BasketCampaign'
ALTER TABLE [dbo].[BasketCampaign]
ADD CONSTRAINT [FK_BasketCampaign_Campaign]
    FOREIGN KEY ([Campaigns_Id])
    REFERENCES [dbo].[Campaign]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BasketCampaign_Campaign'
CREATE INDEX [IX_FK_BasketCampaign_Campaign]
ON [dbo].[BasketCampaign]
    ([Campaigns_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------