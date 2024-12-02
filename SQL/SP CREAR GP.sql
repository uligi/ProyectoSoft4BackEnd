USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Comentario]    Script Date: 11/28/2024 7:45:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Comentario]
    @Comentario NVARCHAR(MAX),
    @FechaCreacion DATETIME,
    @Activo BIT
AS
BEGIN
    INSERT INTO Comentarios (Comentario, FechaCreacion, Activo)
    VALUES (@Comentario, @FechaCreacion, @Activo);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Equipo]    Script Date: 11/28/2024 7:45:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Equipo]
    @NombreEquipos VARCHAR(45),
    @Activo BIT,
    @Fecha_Registro DATETIME
AS
BEGIN
    INSERT INTO Equipos (NombreEquipos, Activo, Fecha_Registro)
    VALUES (@NombreEquipos, @Activo, @Fecha_Registro);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Equipo_Proyecto]    Script Date: 11/28/2024 7:45:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Equipo_Proyecto]
    @Equipos_idEquipos INT,
    @Proyectos_idProyectos INT
AS
BEGIN
    INSERT INTO Equipos_Proyectos (Equipos_idEquipos, Proyectos_idProyectos)
    VALUES (@Equipos_idEquipos, @Proyectos_idProyectos);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_HistorialCambio]    Script Date: 11/28/2024 7:46:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_HistorialCambio]
    @Tareas_idTareas INT,
    @Proyectos_idProyectos INT,
    @Portafolio_idPortafolio INT,
    @DescripcionCambio NVARCHAR(MAX),
    @FechaCambio DATETIME
AS
BEGIN
    INSERT INTO Historial_de_cambios (Tareas_idTareas, Proyectos_idProyectos, Portafolio_idPortafolio, DescripcionCambio, FechaCambio)
    VALUES (@Tareas_idTareas, @Proyectos_idProyectos, @Portafolio_idPortafolio, @DescripcionCambio, @FechaCambio);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Miembro_Equipo]    Script Date: 11/28/2024 7:46:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Miembro_Equipo]
    @Equipos_idEquipos INT,
    @Usuarios_idUsuarios INT,
    @RolesPermisos_idRolesPermisos INT
AS
BEGIN
    INSERT INTO Miembros_de_equipos (Equipos_idEquipos, Usuarios_idUsuarios, RolesPermisos_idRolesPermisos)
    VALUES (@Equipos_idEquipos, @Usuarios_idUsuarios, @RolesPermisos_idRolesPermisos);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Permiso]    Script Date: 11/28/2024 7:46:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Permiso]
    @Nombre_Permisos VARCHAR(100),
    @Activo BIT
AS
BEGIN
    INSERT INTO Permisos (Nombre_Permisos, Activo)
    VALUES (@Nombre_Permisos, @Activo);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Portafolio]    Script Date: 11/28/2024 7:46:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Portafolio]
    @NombrePortafolio VARCHAR(300),
    @Activo BIT,
    @Descripcion NVARCHAR(MAX),
    @FechaCreacion DATETIME
AS
BEGIN
    INSERT INTO Portafolio (NombrePortafolio, Activo, Descripcion, FechaCreacion)
    VALUES (@NombrePortafolio, @Activo, @Descripcion, @FechaCreacion);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Proyecto]    Script Date: 11/28/2024 7:47:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Proyecto]
    @NombreProyecto VARCHAR(500),
    @Descripcion NVARCHAR(MAX),
    @Activo BIT,
    @FechaEstimada DATE,
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Prioridad VARCHAR(45),
    @Portafolio_idPortafolio INT
AS
BEGIN
    INSERT INTO Proyectos (NombreProyecto, Descripcion, Activo, FechaEstimada, FechaInicio, FechaFinal, Prioridad, Portafolio_idPortafolio)
    VALUES (@NombreProyecto, @Descripcion, @Activo, @FechaEstimada, @FechaInicio, @FechaFinal, @Prioridad, @Portafolio_idPortafolio);
END;
GO


USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Rol]    Script Date: 11/28/2024 7:47:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Rol]
    @Nombre_Roles VARCHAR(100),
    @Activo BIT
AS
BEGIN
    INSERT INTO Roles (Nombre_Roles, Activo)
    VALUES (@Nombre_Roles, @Activo);
END;
GO


USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Rol_Permiso]    Script Date: 11/28/2024 7:47:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Rol_Permiso]
    @Permisos_idPermisos INT,
    @Roles_idRoles INT
AS
BEGIN
    INSERT INTO RolesPermisos (Permisos_idPermisos, Roles_idRoles)
    VALUES (@Permisos_idPermisos, @Roles_idRoles);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Subtarea]    Script Date: 11/28/2024 7:47:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Subtarea]
    @NombreSubtareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE
AS
BEGIN
    INSERT INTO Subtareas (NombreSubtareas, Descripcion, Prioridad, FechaInicio, FechaFinal)
    VALUES (@NombreSubtareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Tarea]    Script Date: 11/28/2024 7:47:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Tarea]
    @NombreTareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Activo BIT,
    @Subtareas_idSubtareas INT,
    @Proyectos_idProyectos INT,
    @Comentarios_idComentarios INT
AS
BEGIN
    INSERT INTO Tareas (NombreTareas, Descripcion, Prioridad, FechaInicio, FechaFinal, Activo, Subtareas_idSubtareas, Proyectos_idProyectos, Comentarios_idComentarios)
    VALUES (@NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, @Activo, @Subtareas_idSubtareas, @Proyectos_idProyectos, @Comentarios_idComentarios);
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Crear_Usuario]    Script Date: 11/28/2024 7:47:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Usuario]
    @Nombre VARCHAR(200),
    @Email VARCHAR(200),
    @Contrasena VARCHAR(500),
    @Activo BIT,
    @FechaRegistro DATETIME,
    @Comentarios_idComentarios INT
AS
BEGIN
    INSERT INTO Usuarios (Nombre, Email, Contrasena, Activo, FechaRegistro, Comentarios_idComentarios)
    VALUES (@Nombre, @Email, @Contrasena, @Activo, @FechaRegistro, @Comentarios_idComentarios);
END;
GO














