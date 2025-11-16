INSERT INTO [dbo].[Users] ([Id], [Nombre], [Apellido1], [Apellido2], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Adolfo', N'Escribano', N'Martínez', N'Adolfo123', N'a', N'adolfo@gmail.com', N'a', 1, N'a', N'a', N'a', N'a', 1, 1, N'05/11/2025 0:00:00 +01:00', 1, 0)

SET IDENTITY_INSERT [dbo].[TipoBocadillo] ON
INSERT INTO [dbo].[TipoBocadillo] ([Id], [Nombre]) VALUES (1, N'Serrano')
INSERT INTO [dbo].[TipoBocadillo] ([Id], [Nombre]) VALUES (2, N'Bufalo')
INSERT INTO [dbo].[TipoBocadillo] ([Id], [Nombre]) VALUES (3, N'Americano')
INSERT INTO [dbo].[TipoBocadillo] ([Id], [Nombre]) VALUES (4, N'Completo')
SET IDENTITY_INSERT [dbo].[TipoBocadillo] OFF

SET IDENTITY_INSERT [dbo].[BonoBocadillo] ON
INSERT INTO [dbo].[BonoBocadillo] ([Id], [TipoBocadilloId], [CantidadDisponible], [NumeroBocadillos], [Nombre], [PrecioPorBono]) VALUES (3, 1, 10, 3, N'Adolfo', 3)
INSERT INTO [dbo].[BonoBocadillo] ([Id], [TipoBocadilloId], [CantidadDisponible], [NumeroBocadillos], [Nombre], [PrecioPorBono]) VALUES (5, 2, 10, 5, N'Bono1', 4)
INSERT INTO [dbo].[BonoBocadillo] ([Id], [TipoBocadilloId], [CantidadDisponible], [NumeroBocadillos], [Nombre], [PrecioPorBono]) VALUES (7, 4, 5, 2, N'Bono2', 2)
INSERT INTO [dbo].[BonoBocadillo] ([Id], [TipoBocadilloId], [CantidadDisponible], [NumeroBocadillos], [Nombre], [PrecioPorBono]) VALUES (8, 4, 5, 2, N'Bono3', 2)
SET IDENTITY_INSERT [dbo].[BonoBocadillo] OFF

SET IDENTITY_INSERT [dbo].[CompraBono] ON
INSERT INTO [dbo].[CompraBono] ([Id], [Nombre], [Apellido1], [Apellido2], [FechaCompra],[MetodoPago], [NBonos], [PrecioTotalBono], [ApplicationUserId]) VALUES (2, N'Adolfo', N'Escribano', N'Martinez', N'2025-11-05 00:00:00', 1, 1, 3, N'1')
SET IDENTITY_INSERT [dbo].[CompraBono] OFF