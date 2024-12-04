use DB_GP
GO

CREATE PROCEDURE [dbo].[Modificar_Comentario]
    @idComentarios INT,
    @Comentario NVARCHAR(MAX),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Comentarios WHERE idComentarios = @idComentarios)
    BEGIN
        SELECT -1 AS Codigo, 'Comentario no encontrado' AS Mensaje;
        RETURN;
    END;

    UPDATE Comentarios
    SET Comentario = @Comentario,
        Activo = @Activo
    WHERE idComentarios = @idComentarios;

    SELECT 1 AS Codigo, 'Comentario modificado exitosamente' AS Mensaje;
END;
GO

CREATE PROCEDURE [dbo].[Actualizar_Equipo]
    @idEquipos INT,
    @NombreEquipos VARCHAR(45)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @idEquipos)
    BEGIN
        SELECT -1 AS Codigo, 'El equipo no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el equipo
    UPDATE Equipos
    SET NombreEquipos = @NombreEquipos
    WHERE idEquipos = @idEquipos;

    SELECT 1 AS Codigo, 'Equipo actualizado exitosamente' AS Mensaje;
END;
GO


CREATE PROCEDURE [dbo].[Modificar_Miembro_Equipo]
    @idMiembros_de_equipos INT,
    @idEquipos INT,
    @idUsuarios INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Miembros_de_equipos WHERE idMiembros_de_equipos = @idMiembros_de_equipos)
    BEGIN
        SELECT -1 AS Codigo, 'Miembro no encontrado' AS Mensaje;
        RETURN;
    END;

    UPDATE Miembros_de_equipos
    SET idEquipos = @idEquipos,
        idUsuarios = @idUsuarios
    WHERE idMiembros_de_equipos = @idMiembros_de_equipos;

    SELECT 1 AS Codigo, 'Miembro del equipo modificado exitosamente' AS Mensaje;
END;
GO

CREATE PROCEDURE [dbo].[Actualizar_Portafolio]
    @idPortafolio INT,
    @NombrePortafolio VARCHAR(300),
    @Descripcion NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -1 AS Codigo, 'El portafolio no existe' AS Mensaje;
        RETURN;
    END;

    UPDATE Portafolio
    SET NombrePortafolio = @NombrePortafolio,
        Descripcion = @Descripcion
    WHERE idPortafolio = @idPortafolio;

    SELECT 1 AS Codigo, 'Portafolio actualizado exitosamente' AS Mensaje;
END;
GO



CREATE PROCEDURE [dbo].[Actualizar_Proyecto]
    @idProyectos INT,
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

    -- Validar si el proyecto existe
    IF NOT EXISTS (SELECT 1 FROM Proyectos WHERE idProyectos = @idProyectos)
    BEGIN
        SELECT -1 AS Codigo, 'El proyecto no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el portafolio existe
    IF NOT EXISTS (SELECT 1 FROM Portafolio WHERE idPortafolio = @idPortafolio)
    BEGIN
        SELECT -2 AS Codigo, 'El portafolio asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el equipo existe
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE idEquipos = @Equipos_idEquipos)
    BEGIN
        SELECT -3 AS Codigo, 'El equipo asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el proyecto
    UPDATE Proyectos
    SET NombreProyecto = @NombreProyecto,
        Descripcion = @Descripcion,
        FechaEstimada = @FechaEstimada,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Prioridad = @Prioridad,
        idPortafolio = @idPortafolio,
        Equipos_idEquipos = @Equipos_idEquipos
    WHERE idProyectos = @idProyectos;

    SELECT 1 AS Codigo, 'Proyecto actualizado exitosamente' AS Mensaje;
END;
GO


CREATE PROCEDURE [dbo].[Modificar_Tarea]
    @idTareas INT,
    @NombreTareas VARCHAR(45),
    @Descripcion NVARCHAR(MAX),
    @Prioridad VARCHAR(45),
    @FechaInicio DATE,
    @FechaFinal DATE,
    @Activo BIT,
    @idProyectos INT,
    @idUsuarios INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Tareas WHERE idTareas = @idTareas)
    BEGIN
        SELECT -1 AS Codigo, 'Tarea no encontrada' AS Mensaje;
        RETURN;
    END;

    UPDATE Tareas
    SET NombreTareas = @NombreTareas,
        Descripcion = @Descripcion,
        Prioridad = @Prioridad,
        FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Activo = @Activo,
        idProyectos = @idProyectos,
        idUsuarios = @idUsuarios
    WHERE idTareas = @idTareas;

    SELECT 1 AS Codigo, 'Tarea modificada exitosamente' AS Mensaje;
END;
GO

Create PROCEDURE [dbo].[Modificar_Usuario]
    @idUsuarios INT,
    @Nombre VARCHAR(200),
    @Email VARCHAR(200),
    @idRoles INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE idUsuarios = @idUsuarios)
    BEGIN
        SELECT -1 AS Codigo, 'Usuario no encontrado' AS Mensaje;
        RETURN;
    END;

    UPDATE Usuarios
    SET Nombre = @Nombre,
        Email = @Email,
        idRoles = @idRoles
    WHERE idUsuarios = @idUsuarios;

    SELECT 1 AS Codigo, 'Usuario modificado exitosamente' AS Mensaje;
END;
GO


CREATE PROCEDURE [dbo].[Modificar_Permiso]
    @idPermisos INT,
    @Nombre_Permisos VARCHAR(100),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el permiso existe
    IF NOT EXISTS (SELECT 1 FROM Permisos WHERE idPermisos = @idPermisos)
    BEGIN
        SELECT -1 AS Codigo, 'El permiso no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el permiso
    UPDATE Permisos
    SET Nombre_Permisos = @Nombre_Permisos,
        Activo = @Activo
    WHERE idPermisos = @idPermisos;

    -- Respuesta
    SELECT 1 AS Codigo, 'Permiso modificado exitosamente' AS Mensaje;
END;
GO

CREATE PROCEDURE [dbo].[Modificar_Rol]
    @idRoles INT,
    @Nombre_Roles VARCHAR(100),
    @Activo BIT,
    @idPermisos INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el rol existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE idRoles = @idRoles)
    BEGIN
        SELECT -1 AS Codigo, 'El rol no existe' AS Mensaje;
        RETURN;
    END;

    -- Validar si el permiso asociado existe
    IF NOT EXISTS (SELECT 1 FROM Permisos WHERE idPermisos = @idPermisos)
    BEGIN
        SELECT -2 AS Codigo, 'El permiso asociado no existe' AS Mensaje;
        RETURN;
    END;

    -- Actualizar el rol
    UPDATE Roles
    SET Nombre_Roles = @Nombre_Roles,
        Activo = @Activo,
        idPermisos = @idPermisos
    WHERE idRoles = @idRoles;

    -- Respuesta
    SELECT 1 AS Codigo, 'Rol modificado exitosamente' AS Mensaje;
END;
GO
