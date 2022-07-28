CREATE TABLE [dbo].[UserType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Photo] (
    [Id]   INT             IDENTITY (1, 1) NOT NULL,
    [Data] VARBINARY (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[User] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50)   NOT NULL,
    [Lastname]  NVARCHAR (50)   NOT NULL,
    [Email]     NVARCHAR (50)   NOT NULL,
    [Phone]     NVARCHAR (25)   NOT NULL,
    [Password]  VARBINARY (MAX) NOT NULL,
    [TypeId]    INT             NOT NULL,
    [AvatarId]  INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[UserType] ([Id]),
    FOREIGN KEY ([AvatarId]) REFERENCES [dbo].[Photo] ([Id])
);

CREATE TABLE [dbo].[CarType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CarMark] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (90) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CarModel] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (90) NOT NULL,
    [MarkId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([MarkId]) REFERENCES [dbo].[CarMark] ([Id])
);

CREATE TABLE [dbo].[Car] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [TypeId]       INT            NOT NULL,
    [ModelId]      INT            NOT NULL,
    [Price]        INT            NOT NULL,
    [Color]        NVARCHAR (30)  NOT NULL,
    [IsElectrical] BIT            NOT NULL,
    [Description]  NVARCHAR (200) NOT NULL,
    [Length]       INT            NOT NULL,
    [Width]        INT            NOT NULL,
    [Height]       INT            NOT NULL,
    [Weight]       INT            NOT NULL,
    [Power]        INT            NOT NULL,
    [TankSize]     INT            NOT NULL,
    [Seats]        INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[CarType] ([Id]),
    FOREIGN KEY ([ModelId]) REFERENCES [dbo].[CarModel] ([Id])
);

CREATE TABLE [dbo].[CarPhoto] (
    [CarId]   INT NOT NULL,
    [PhotoId] INT NOT NULL,
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id]),
    FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photo] ([Id])
);

CREATE TABLE [dbo].[CarPurpose] (
    [UserId] INT NOT NULL,
    [CarId]  INT NOT NULL,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id])
);

CREATE TABLE [dbo].[DetailType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Detail] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [TypeId]      INT            NOT NULL,
    [ModelId]     INT            NOT NULL,
    [PhotoId]     INT            NOT NULL,
    [Price]       INT            NOT NULL,
    [Description] NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DetailType] ([Id]),
    FOREIGN KEY ([PhotoId]) REFERENCES [dbo].[Photo] ([Id]),
    FOREIGN KEY ([ModelId]) REFERENCES [dbo].[CarModel] ([Id])
);

CREATE TABLE [dbo].[DetailPurpose] (
    [UserId]   INT NOT NULL,
    [DetailId] INT NOT NULL,
    FOREIGN KEY ([DetailId]) REFERENCES [dbo].[Detail] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

CREATE TABLE [dbo].[Notification] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserFromId]  INT            NOT NULL,
    [UserToId]    INT            NOT NULL,
    [CarId]       INT            NULL,
    [DetailId]    INT            NULL,
    [Description] NVARCHAR (300) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([DetailId]) REFERENCES [dbo].[Detail] ([Id]),
    FOREIGN KEY ([UserFromId]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([UserToId]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Car] ([Id])
);