USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Comentario]    Script Date: 11/28/2024 7:48:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Comentario]
    @idComentarios INT,
    @Comentario NVARCHAR(MAX),
    @Activo BIT
AS
BEGIN
    UPDATE Comentarios
    SET Comentario = @Comentario,
        Activo = @Activo
    WHERE idComentarios = @idComentarios;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Equipo]    Script Date: 11/28/2024 7:48:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Equipo]
    @idEquipos INT,
    @NombreEquipos VARCHAR(45),
    @Activo BIT
AS
BEGIN
    UPDATE Equipos
    SET NombreEquipos = @NombreEquipos,
        Activo = @Activo
    WHERE idEquipos = @idEquipos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Equipo_Proyecto]    Script Date: 11/28/2024 7:48:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Equipo_Proyecto]
    @Equipos_idEquipos INT,
    @Proyectos_idProyectos INT,
    @Nuevo_Equipos_idEquipos INT,
    @Nuevo_Proyectos_idProyectos INT
AS
BEGIN
    UPDATE Equipos_Proyectos
    SET Equipos_idEquipos = @Nuevo_Equipos_idEquipos,
        Proyectos_idProyectos = @Nuevo_Proyectos_idProyectos
    WHERE Equipos_idEquipos = @Equipos_idEquipos AND Proyectos_idProyectos = @Proyectos_idProyectos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_HistorialCambio]    Script Date: 11/28/2024 7:48:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_HistorialCambio]
    @idHistorial_de_cambios INT,
    @DescripcionCambio NVARCHAR(MAX),
    @FechaCambio DATETIME
AS
BEGIN
    UPDATE Historial_de_cambios
    SET DescripcionCambio = @DescripcionCambio,
        FechaCambio = @FechaCambio
    WHERE idHistorial_de_cambios = @idHistorial_de_cambios;
END;
GO
USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Miembro_Equipo]    Script Date: 11/28/2024 7:50:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Miembro_Equipo]
    @idMiembros_de_equipos INT,
    @Equipos_idEquipos INT,
    @Usuarios_idUsuarios INT,
    @RolesPermisos_idRolesPermisos INT
AS
BEGIN
    UPDATE Miembros_de_equipos
    SET Equipos_idEquipos = @Equipos_idEquipos,
        Usuarios_idUsuarios = @Usuarios_idUsuarios,
        RolesPermisos_idRolesPermisos = @RolesPermisos_idRolesPermisos
    WHERE idMiembros_de_equipos = @idMiembros_de_equipos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Permiso]    Script Date: 11/28/2024 7:50:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Permiso]
    @idPermisos INT,
    @Nombre_Permisos VARCHAR(100),
    @Activo BIT
AS
BEGIN
    UPDATE Permisos
    SET Nombre_Permisos = @Nombre_Permisos,
        Activo = @Activo
    WHERE idPermisos = @idPermisos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Portafolio]    Script Date: 11/28/2024 7:51:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Portafolio]
    @idPortafolio INT,
    @NombrePortafolio VARCHAR(300),
    @Activo BIT,
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    UPDATE Portafolio
    SET NombrePortafolio = @NombrePortafolio,
        Activo = @Activo,
        Descripcion = @Descripcion
    WHERE idPortafolio = @idPortafolio;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Proyecto]    Script Date: 11/28/2024 7:51:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Proyecto]
    @idProyectos INT,
    @NombreProyecto VARCHAR(500),
    @Descripcion NVARCHAR(MAX),
    @Activo BIT,
    @FechaEstimada DATE,
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Prioridad VARCHAR(45)
AS
BEGIN
    UPDATE Proyectos
    SET NombreProyecto = @NombreProyecto,
        Descripcion = @Descripcion,
        Activo = @Activo,
        FechaEstimada = @FechaEstimada,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Prioridad = @Prioridad
    WHERE idProyectos = @idProyectos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Rol]    Script Date: 11/28/2024 7:51:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Rol]
    @idRoles INT,
    @Nombre_Roles VARCHAR(100),
    @Activo BIT
AS
BEGIN
    UPDATE Roles
    SET Nombre_Roles = @Nombre_Roles,
        Activo = @Activo
    WHERE idRoles = @idRoles;
END;
GO


USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Rol_Permiso]    Script Date: 11/28/2024 7:51:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Rol_Permiso]
    @idRolesPermisos INT,
    @Permisos_idPermisos INT,
    @Roles_idRoles INT
AS
BEGIN
    UPDATE RolesPermisos
    SET Permisos_idPermisos = @Permisos_idPermisos,
        Roles_idRoles = @Roles_idRoles
    WHERE idRolesPermisos = @idRolesPermisos;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Subtarea]    Script Date: 11/28/2024 7:51:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Subtarea]
    @idSubtareas INT,
    @NombreSubtareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE
AS
BEGIN
    UPDATE Subtareas
    SET NombreSubtareas = @NombreSubtareas,
        Descripcion = @Descripcion,
        Prioridad = @Prioridad,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal
    WHERE idSubtareas = @idSubtareas;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Tarea]    Script Date: 11/28/2024 7:51:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Tarea]
    @idTareas INT,
    @NombreTareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Activo BIT
AS
BEGIN
    UPDATE Tareas
    SET NombreTareas = @NombreTareas,
        Descripcion = @Descripcion,
        Prioridad = @Prioridad,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Activo = @Activo
    WHERE idTareas = @idTareas;
END;
GO

USE [DB_GP]
GO

/****** Object:  StoredProcedure [dbo].[Modificar_Usuario]    Script Date: 11/28/2024 7:52:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Usuario]
    @idUsuarios INT,
    @Nombre VARCHAR(200),
    @Email VARCHAR(200),
    @Activo BIT
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Email = @Email,
        Activo = @Activo
    WHERE idUsuarios = @idUsuarios;
END;
GO





















