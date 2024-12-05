USE DB_GP;
GO

-- Stored Procedure para la tabla Proyectos con JOIN a Portafolio y Equipos
CREATE PROCEDURE [dbo].[sp_Listar_Proyectos]
AS
BEGIN
SELECT 
    p.idProyectos,
    p.NombreProyecto,
    p.Descripcion,
    p.FechaEstimada,
    p.FechaInicio,
    p.FechaFinal,
    p.Prioridad,
    pf.NombrePortafolio,
    e.NombreEquipos,
    p.Activo
FROM Proyectos p
INNER JOIN Portafolio pf ON p.idPortafolio = pf.idPortafolio
INNER JOIN Equipos e ON p.Equipos_idEquipos = e.idEquipos;

END;

GO

-- Stored Procedure para la tabla Tareas con JOIN a Proyectos y Usuarios
CREATE PROCEDURE Listar_Tareas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.idTareas,
        t.NombreTareas,
        t.Descripcion,
        t.Prioridad,
        t.FechaInicio,
        t.FechaFinal,
        t.Activo,
        t.idProyectos,
        t.idUsuarios,
        p.NombreProyecto,
        u.Nombre AS NombreUsuario
    FROM Tareas t
    LEFT JOIN Proyectos p ON t.idProyectos = p.idProyectos
    LEFT JOIN Usuarios u ON t.idUsuarios = u.idUsuarios;
END;
GO


-- Stored Procedure para la tabla Subtareas con JOIN a Tareas
CREATE PROCEDURE Listar_Subtareas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ST.idSubtareas,
        ST.NombreSubtareas,
        ST.Descripcion,
        ST.Prioridad,
        ST.FechaInicio,
        ST.FechaFinal,
        ST.idTareas,
        T.NombreTareas AS NombreTarea
    FROM Subtareas ST
    INNER JOIN Tareas T ON ST.idTareas = T.idTareas;
END;
GO


-- Stored Procedure para la tabla Comentarios con JOIN a Tareas, Subtareas y Proyectos
CREATE PROCEDURE [dbo].[sp_Listar_Comentarios]
AS
BEGIN
    SELECT 
        c.idComentarios,
        c.Comentario,
        c.FechaCreacion,
        c.Activo,
        c.Tareas_idTareas,
        t.NombreTareas AS NombreTareas,
        c.idSubtareas,
        st.NombreSubtareas AS NombreSubtareas,
        c.idProyectos,
        p.NombreProyecto AS NombreProyecto
    FROM Comentarios c
    LEFT JOIN Tareas t ON c.Tareas_idTareas = t.idTareas
    LEFT JOIN Subtareas st ON c.idSubtareas = st.idSubtareas
    LEFT JOIN Proyectos p ON c.idProyectos = p.idProyectos;
END;

GO

-- Stored Procedure para la tabla Miembros_de_equipos con JOIN a Equipos, Usuarios y Roles
CREATE PROCEDURE Listar_Todos_Los_Miembros
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        me.idMiembros_de_equipos, 
        me.idEquipos, 
        me.idUsuarios, 
        u.Nombre AS NombreUsuario, 
        e.NombreEquipos
    FROM Miembros_de_equipos me
    INNER JOIN Usuarios u ON me.idUsuarios = u.idUsuarios
    INNER JOIN Equipos e ON me.idEquipos = e.idEquipos;
END;
GO




-- Stored Procedure para la tabla Historial_de_cambios con JOIN a Tareas, Proyectos y Portafolio
CREATE PROCEDURE [dbo].[sp_Listar_Historial_Cambios]
AS
BEGIN
    SELECT 
        hc.idHistorial_de_cambios,
        hc.idTareas,
        t.NombreTareas AS NombreTareas,
        hc.idProyectos,
        p.NombreProyecto AS NombreProyecto,
        hc.Portafolio_idPortafolio,
        pf.NombrePortafolio AS NombrePortafolio,
        hc.Descripcioncambio,
        hc.FechaCambio
    FROM Historial_de_cambios hc
    LEFT JOIN Tareas t ON hc.idTareas = t.idTareas
    LEFT JOIN Proyectos p ON hc.idProyectos = p.idProyectos
    LEFT JOIN Portafolio pf ON hc.Portafolio_idPortafolio = pf.idPortafolio;
END;
GO

CREATE PROCEDURE [dbo].[sp_Listar_Permisos]
AS
BEGIN
    SELECT 
        idPermisos,
        Nombre_Permisos,
        Activo
    FROM Permisos;
END;
GO

CREATE PROCEDURE [dbo].[sp_Listar_Roles]
AS
BEGIN
    SELECT 
        r.idRoles,
        r.Nombre_Roles,
        r.Activo,
        r.idPermisos,
        p.Nombre_Permisos AS NombrePermisos
    FROM Roles r
    LEFT JOIN Permisos p ON r.idPermisos = p.idPermisos;
END;
GO



create PROCEDURE [dbo].[sp_Listar_Usuarios]
AS
BEGIN
    SELECT 
        u.idUsuarios,
        u.Nombre,
        u.Email,
        u.Activo,
        u.FechaRegistro,
        u.RestablecerContrasena,
        u.idRoles,
        r.Nombre_Roles AS NombreRol,
        p.Nombre_Permisos AS PermisoRelacionado
    FROM Usuarios u
    LEFT JOIN Roles r ON u.idRoles = r.idRoles
    LEFT JOIN Permisos p ON r.idPermisos = p.idPermisos; -- Cambiado a idPermisos
END;
GO


CREATE PROCEDURE [dbo].[Listar_Portafolios]
AS
BEGIN
    SELECT idPortafolio, NombrePortafolio, Descripcion, Activo, FechaCreacion
    FROM Portafolio
    ORDER BY FechaCreacion DESC;
END;
GO

CREATE PROCEDURE [dbo].[Listar_Equipos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idEquipos, NombreEquipos, Activo, Fecha_Registro
    FROM Equipos
    WHERE Activo = 1;
END;
GO
