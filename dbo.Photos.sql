CREATE TABLE [dbo].[Photos] (
    [PhotoID]    INT            IDENTITY (1, 1) NOT NULL,
    [WhenPosted] SMALLDATETIME  NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Photo]      NVARCHAR (MAX) NOT NULL,
    [Poster]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([PhotoID] ASC),
    CONSTRAINT [FK_Photos_Posters] FOREIGN KEY ([Poster]) REFERENCES [dbo].[Posters] ([Id])
);

