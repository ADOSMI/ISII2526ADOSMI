INSERT INTO [dbo].[Users] ([Id], [Nombre], [Apellido1], [Apellido2], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Alberto', N'Cuenca', N'Aleman', N'Alberto', N'a', N'alberto@gmail.com', N'a', 1, N'a', N'a', N'a', N'a', 1, 1, N'05/11/2025 0:00:00 +01:00', 1, 0)

SET IDENTITY_INSERT [dbo].[TipoPan] ON
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (1, N'Semillas')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (2, N'Sin gluten')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (3, N'Vegano')
SET IDENTITY_INSERT [dbo].[TipoPan] OFF

SET IDENTITY_INSERT [dbo].[Bocadillo] ON
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (4, N'Completo', 3, 9, 0, 1)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (6, N'Bacon', 4, 11, 1, 2)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (7, N'Vegetal', 3.5, 13, 0, 2)
SET IDENTITY_INSERT [dbo].[Bocadillo] OFF

SET IDENTITY_INSERT [dbo].[Compra] ON
INSERT INTO [dbo].[Compra] ([Id], [Apellido_1Cliente], [Apellido_2Cliente], [NombreCliente], [FechaCompra], [PrecioTotal], [NumBocadillos], [MetodoPago], [ApplicationUserId]) VALUES (1, N'Cuenca', NULL, N'Alberto', N'2024-01-13 00:00:00', 15, 5, 1, NULL)
SET IDENTITY_INSERT [dbo].[Compra] OFF

INSERT INTO [dbo].[CompraBocadillo] ([BocadilloId], [CompraId], [Cantidad], [Precio], [NombreBocadillo], [TipoPan]) VALUES (4, 1, 3, 15, N'Completo', N'1')
INSERT INTO [dbo].[CompraBocadillo] ([BocadilloId], [CompraId], [Cantidad], [Precio], [NombreBocadillo], [TipoPan]) VALUES (6, 1, 2, 11, N'Bacon', N'2')
