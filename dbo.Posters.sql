CREATE TABLE [dbo].[Posters] (
    [Id]       BIGINT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [UserName]       NVARCHAR (50)  NOT NULL,
    [Email]          NVARCHAR (50)  NOT NULL,
    [PasswordHash]       NVARCHAR (500)  NOT NULL,
    [User_profile]   NVARCHAR (MAX) NULL,
    [ADA]            BIT            DEFAULT ((0)) NOT NULL,
    [IsConfirmed]      BIT            DEFAULT ((0)) NOT NULL,
    [WhenRegistered] SMALLDATETIME  NOT NULL,
    [WhenConfirmed]  SMALLDATETIME  DEFAULT (NULL) NULL,
    [SecurityStamp] NVARCHAR(254) NULL, 
    [Discriminator] NVARCHAR(254) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

