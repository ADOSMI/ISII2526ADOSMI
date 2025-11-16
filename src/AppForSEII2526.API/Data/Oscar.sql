INSERT INTO [dbo].[Users] ([Id], [Nombre], [Apellido1], [Apellido2], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Adolfo', N'Escribano', N'Martinez', N'Adolfo123', N'a', N'adolfo@gmail.com', N'a', 1, N'a', N'a', N'a', N'a', 1, 1, N'05/11/2025 0:00:00 +01:00', 1, 0)
INSERT INTO [dbo].[Users] ([Id], [Nombre], [Apellido1], [Apellido2], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2', N'Oscar', N'Merino', N'Gualo', N'Oscar123', N'a', N'oscar@gmail.com', N'a', 1, N'a', N'a', N'a', N'a', 1, 1, N'16/11/2025 0:00:00 +01:00', 1, 0)

SET IDENTITY_INSERT [dbo].[TipoPan] ON
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (1, N'Rustico')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (2, N'Pipas')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (3, N'Integral')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (5, N'Semillas')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (6, N'Sin gluten')
INSERT INTO [dbo].[TipoPan] ([Id], [Nombre]) VALUES (7, N'Normal')
SET IDENTITY_INSERT [dbo].[TipoPan] OFF

SET IDENTITY_INSERT [dbo].[Bocadillo] ON
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (14, N'Completo', 6, 34, 1, 5)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (16, N'Bacon', 5, 56, 1, 6)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (17, N'Vegetal', 4, 34, 0, 6)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (1002, N'Serranito', 2, 10, 1, 1)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (1005, N'Politecnico', 3, 10, 0, 3)
INSERT INTO [dbo].[Bocadillo] ([Id], [Nombre], [PVP], [Stock], [Tamano], [TipoPanId]) VALUES (1007, N'Completo', 3, 7, 1, 2)
SET IDENTITY_INSERT [dbo].[Bocadillo] OFF

SET IDENTITY_INSERT [dbo].[ResenyaBocadillo] ON
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (14, 1, 5)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (16, 2, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (16, 3, 9)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 4, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 5, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 6, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 7, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 10, 9)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1002, 11, 9)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1005, 8, 7)
INSERT INTO [dbo].[ResenyaBocadillo] ([BocadilloId], [ResenyaId], [Puntuacion]) VALUES (1005, 9, 4)
SET IDENTITY_INSERT [dbo].[ResenyaBocadillo] OFF

SET IDENTITY_INSERT [dbo].[Resenya] ON
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (1, N'Muy bueno', N'2025-05-23 00:00:00', N'Martin', N'SATISFECHO', N'1', 9)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (2, N'Regular', N'2025-05-24 00:00:00', N'Roberto', N'DESCONTENTO', N'1', 5)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (3, N'Prueba correcta', N'2025-11-16 12:21:05', N'Mario', N'Excelente', NULL, 4)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (4, N'Que bueno estaba', N'2025-11-16 12:36:47', N'Manolo Lama', N'Contento', NULL, 3)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (5, N'Que bueno estaba', N'2025-11-16 12:37:56', N'Manolo Lama', N'Contento', NULL, 3)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (6, N'Que bueno estaba', N'2025-11-16 12:38:17', N'Manolo Lama', N'Contento', NULL, 3)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (7, N'Que bueno estaba', N'2025-11-16 12:38:32', N'Manolo Lama', N'Contento', NULL, 3)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (8, N'Mar de dudas', N'2025-11-16 12:39:32', N'Paco Gonzalez', N'Regular', NULL, 1)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (9, N'Mar de dudas', N'2025-11-16 12:39:49', N'Paco Gonzalez', N'Regular', NULL, 1)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (10, N'Muy agradecido con el trato. Estuvo genial', N'2025-11-16 12:54:45', N'Adolfo123', N'Impresionante', N'1', 4)
INSERT INTO [dbo].[Resenya] ([Id], [Descripcion], [FechaPublicacion], [NombreUsuario], [Titulo], [ApplicationUserId], [Valoracion]) VALUES (11, N'Muy agradecido con el trato. Estuvo genial', N'2025-11-16 12:56:08', N'Adolfo123', N'Impresionante', N'1', 4)
SET IDENTITY_INSERT [dbo].[Resenya] OFF