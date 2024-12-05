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
    @Activo BIT = 1,
    @Tareas_idTareas INT = NULL,
    @idSubtareas INT = NULL,
    @idProyectos INT = NULL
AS
BEGIN
    INSERT INTO Comentarios (Comentario, FechaCreacion, Activo, Tareas_idTareas, idSubtareas, idProyectos)
    VALUES (@Comentario, @FechaCreacion, @Activo, @Tareas_idTareas, @idSubtareas, @idProyectos);
END;
GO

CREATE PROCEDURE [dbo].[Crear_Equipo]
    @NombreEquipos VARCHAR(45),
    @Activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el equipo ya existe
    IF EXISTS (SELECT 1 FROM Equipos WHERE NombreEquipos = @NombreEquipos)
    BEGIN
        SELECT -1 AS Codigo, 'El equipo ya existe' AS Mensaje;
        RETURN;
    END;

    -- Insertar el nuevo equipo
    INSERT INTO Equipos (NombreEquipos, Activo, Fecha_Registro)
    VALUES (@NombreEquipos, @Activo, GETDATE());

    SELECT 1 AS Codigo, 'Equipo creado exitosamente' AS Mensaje;
END;
GO




CREATE PROCEDURE [dbo].[Crear_HistorialCambio]
    @idTareas INT = NULL,
    @idProyectos INT = NULL,
    @idPortafolio INT = NULL,
    @DescripcionCambio NVARCHAR(MAX),
    @FechaCambio DATETIME,
    @idUsuarios INT = NULL,
    @idSubtareas INT = NULL
AS
BEGIN
    INSERT INTO Historial_de_cambios (idTareas, idProyectos, idPortafolio, DescripcionCambio, FechaCambio, idUsuarios, idSubtareas)
    VALUES (@idTareas, @idProyectos, @idPortafolio, @DescripcionCambio, @FechaCambio, @idUsuarios, @idSubtareas);
END;
GO

CREATE PROCEDURE [dbo].[Crear_Miembro_Equipo]
    @idEquipos INT,
    @idUsuarios INT
AS
BEGIN
    INSERT INTO Miembros_de_equipos (idEquipos, idUsuarios)
    VALUES (@idEquipos, @idUsuarios);
END;
GO

CREATE PROCEDURE [dbo].[Crear_Proyecto]
    @NombreProyecto VARCHAR(500),
    @Descripcion NVARCHAR(MAX),
    @FechaEstimada DATE,
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Prioridad VARCHAR(45),
    @idPortafolio INT,
    @Equipos_idEquipos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si el portafolio existe
    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -1 AS Codigo, 'El portafolio asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @Equipos_idEquipos)
    BEGIN
        SELECT -2 AS Codigo, 'El equipo asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Insertar el proyecto
    INSERT INTO Proyectos (NombreProyecto, Descripcion, Activo, FechaEstimada, FechaInicio, FechaFinal, Prioridad, idPortafolio, Equipos_idEquipos)
    VALUES (@NombreProyecto, @Descripcion, 1, @FechaEstimada, @FechaInicio, @FechaFinal, @Prioridad, @idPortafolio, @Equipos_idEquipos);

    SELECT 1 AS Codigo, 'Proyecto creado exitosamente' AS Mensaje;
END;
GO



CREATE PROCEDURE Crear_Subtarea
    @NombreSubtareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @idTareas INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Subtareas (
        NombreSubtareas, 
        Descripcion, 
        Prioridad, 
        FechaInicio, 
        FechaFinal, 
        idTareas
    )
    VALUES (
        @NombreSubtareas, 
        @Descripcion, 
        @Prioridad, 
        @FechaInicio, 
        @FechaFinal, 
        @idTareas
    );

    SELECT 
        SCOPE_IDENTITY() AS idSubtareas,
        1 AS Codigo,
        'Subtarea creada exitosamente.' AS Mensaje;
END;
GO


CREATE PROCEDURE Crear_Tarea
    @NombreTareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @idProyectos INT,
    @idUsuarios INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tareas (NombreTareas, Descripcion, Prioridad, FechaInicio, FechaFinal, Activo, idProyectos, idUsuarios)
    VALUES (@NombreTareas, @Descripcion, @Prioridad, @FechaInicio, @FechaFinal, 1, @idProyectos, @idUsuarios);

    SELECT SCOPE_IDENTITY() AS idTareas,1 AS Codigo, 'Usuario creado exitosamente.' AS Mensaje;;
END;
GO


Create PROCEDURE [dbo].[Crear_Usuario]
    @Nombre VARCHAR(200),
    @Email VARCHAR(200),
    @Contrasena VARCHAR(500),
    @idRoles INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuarios (Nombre, Email, Contrasena, RestablecerContrasena, Activo, FechaRegistro, idRoles)
    VALUES (@Nombre, @Email, @Contrasena, 1, 1, GETDATE(), @idRoles);

    SELECT SCOPE_IDENTITY() AS idUsuarios, 1 AS Codigo, 'Usuario creado exitosamente.' AS Mensaje;
END;
GO


CREATE PROCEDURE Crear_Miembro_Equipo
    @idEquipos INT,
    @idUsuarios INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @idEquipos)
    BEGIN
        SELECT -1 AS Codigo, 'El equipo no existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE idUsuarios = @idUsuarios)
    BEGIN
        SELECT -2 AS Codigo, 'El usuario no existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si ya está asignado al equipo
    IF EXISTS (SELECT 1 FROM Miembros_de_equipos WHERE idEquipos = @idEquipos AND idUsuarios = @idUsuarios)
    BEGIN
        SELECT -3 AS Codigo, 'El usuario ya está asignado al equipo' AS Mensaje;
        RETURN;
    END;

    -- Insertar el miembro al equipo
    INSERT INTO Miembros_de_equipos (idEquipos, idUsuarios)
    VALUES (@idEquipos, @idUsuarios);

    SELECT 1 AS Codigo, 'Miembro agregado exitosamente' AS Mensaje;
END;
GO



CREATE PROCEDURE [dbo].[Crear_Rol]
    @Nombre_Roles VARCHAR(100),
    @Activo BIT = 1,
    @idPermisos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el rol ya existe
    IF EXISTS (SELECT 1 FROM Roles WHERE Nombre_Roles = @Nombre_Roles)
    BEGIN
        SELECT -1 AS Codigo, 'El rol ya existe' AS Mensaje;
        RETURN;
    END;

    -- Verificar si el permiso asociado existe
    IF NOT EXISTS (SELECT 1 FROM Permisos WHERE idPermisos = @idPermisos)
    BEGIN
        SELECT -2 AS Codigo, 'El permiso asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Insertar el rol
    INSERT INTO Roles (Nombre_Roles, Activo, idPermisos)
    VALUES (@Nombre_Roles, @Activo, @idPermisos);

    -- Respuesta con el ID generado
    SELECT SCOPE_IDENTITY() AS idRoles, 1 AS Codigo, 'Rol creado exitosamente' AS Mensaje;
END;
GO


CREATE PROCEDURE [dbo].[Crear_Portafolio]
    @NombrePortafolio VARCHAR(300),
    @Descripcion NVARCHAR(MAX),
    @Activo BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Portafolio (NombrePortafolio, Descripcion, Activo, FechaCreacion)
    VALUES (@NombrePortafolio, @Descripcion, @Activo, GETDATE());

    SELECT SCOPE_IDENTITY() AS idPortafolio,1 AS Codigo, 'Portafolio creado exitosamente' AS Mensaje;
	 
END;
GO










