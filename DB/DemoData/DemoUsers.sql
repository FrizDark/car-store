SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([Id], [Firstname], [Lastname], [Email], [Phone], [Password], [TypeId], [AvatarId]) VALUES (2, N'Vasya', N'Pupkin', N'vasya@mail.com', N'(000)000-00-00', <Binary Data>, 1, NULL)
INSERT INTO [dbo].[User] ([Id], [Firstname], [Lastname], [Email], [Phone], [Password], [TypeId], [AvatarId]) VALUES (3, N'Petya', N'Sidorov', N'petya@mail.com', N'(111)111-11-11', <Binary Data>, 1, NULL)
INSERT INTO [dbo].[User] ([Id], [Firstname], [Lastname], [Email], [Phone], [Password], [TypeId], [AvatarId]) VALUES (4, N'Anna', N'Ogurtsova', N'anna@mail.com', N'(222)222-22-22', <Binary Data>, 2, NULL)
INSERT INTO [dbo].[User] ([Id], [Firstname], [Lastname], [Email], [Phone], [Password], [TypeId], [AvatarId]) VALUES (5, N'Andrey', N'Arbuzov', N'andrey@mail.com', N'(444)444-44-44', <Binary Data>, 3, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
