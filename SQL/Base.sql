CREATE DATABASE DB_GP; 
GO
USE DB_GP;
GO

CREATE TABLE Equipos (
    idEquipos INT PRIMARY KEY,
    NombreEquipos VARCHAR(45),
    Activo BIT,
    Fecha_Registro DATETIME
);
GO

CREATE TABLE Portafolio (
    idPortafolio INT PRIMARY KEY,
    NombrePortafolio VARCHAR(300),
    Activo BIT,
    Descripcion NVARCHAR(MAX),
    FechaCreacion DATETIME
);
GO

CREATE TABLE Proyectos (
    idProyectos INT PRIMARY KEY,
    NombreProyecto VARCHAR(500),
    Descripcion NVARCHAR(MAX),
    Activo BIT,
    FechaEstimada DATE,
    FechaInicio DATE,
    FechaFinal DATE,
    Prioridad VARCHAR(45),
    Portafolio_idPortafolio INT,
    FOREIGN KEY (Portafolio_idPortafolio) REFERENCES Portafolio(idPortafolio)
);
GO

CREATE TABLE Subtareas (
    idSubtareas INT PRIMARY KEY,
    NombreSubtareas VARCHAR(45),
    Descripcion NVARCHAR(MAX),
    Prioridad VARCHAR(45),
    FechaInicio DATE,
    FechaFinal DATE
);
GO

CREATE TABLE Comentarios (
    idComentarios INT PRIMARY KEY,
    Comentario NVARCHAR(MAX),
    FechaCreacion DATETIME,
    Activo BIT
);

GO


CREATE TABLE Tareas (
    idTareas INT PRIMARY KEY,
    NombreTareas VARCHAR(45),
    Descripcion NVARCHAR(MAX),
    Prioridad VARCHAR(45),
    FechaInicio DATE,
    FechaFinal DATE,
    Activo BIT,
    Subtareas_idSubtareas INT,
    Proyectos_idProyectos INT,
    Comentarios_idComentarios INT,
    FOREIGN KEY (Subtareas_idSubtareas) REFERENCES Subtareas(idSubtareas),
    FOREIGN KEY (Proyectos_idProyectos) REFERENCES Proyectos(idProyectos),
    FOREIGN KEY (Comentarios_idComentarios) REFERENCES Comentarios(idComentarios)
);
GO

CREATE TABLE Usuarios (
    idUsuarios INT PRIMARY KEY,
    Nombre VARCHAR(200),
    Email VARCHAR(200) UNIQUE,
    contrasena VARCHAR(500),
    Activo BIT,
    FechaRegistro DATETIME,
    Comentarios_idComentarios INT,
    FOREIGN KEY (Comentarios_idComentarios) REFERENCES Comentarios(idComentarios)
);
GO

CREATE TABLE Permisos (
    idPermisos INT PRIMARY KEY,
    Nombre_Permisos VARCHAR(100),
    Activo BIT
);
GO

CREATE TABLE Roles (
    idRoles INT PRIMARY KEY,
    Nombre_Roles VARCHAR(100),
    Activo BIT
);
GO

CREATE TABLE RolesPermisos (
    idRolesPermisos INT PRIMARY KEY,
    Permisos_idPermisos INT,
    Roles_idRoles INT,
    FOREIGN KEY (Permisos_idPermisos) REFERENCES Permisos(idPermisos),
    FOREIGN KEY (Roles_idRoles) REFERENCES Roles(idRoles)
);
GO

CREATE TABLE Miembros_de_equipos (
    idMiembros_de_equipos INT PRIMARY KEY,
    Equipos_idEquipos INT,
    Usuarios_idUsuarios INT,
    RolesPermisos_idRolesPermisos INT,
    FOREIGN KEY (Equipos_idEquipos) REFERENCES Equipos(idEquipos),
    FOREIGN KEY (Usuarios_idUsuarios) REFERENCES Usuarios(idUsuarios),
    FOREIGN KEY (RolesPermisos_idRolesPermisos) REFERENCES RolesPermisos(idRolesPermisos)
);
GO

CREATE TABLE Equipos_Proyectos (
    Equipos_idEquipos INT,
    Proyectos_idProyectos INT,
    PRIMARY KEY (Equipos_idEquipos, Proyectos_idProyectos),
    FOREIGN KEY (Equipos_idEquipos) REFERENCES Equipos(idEquipos),
    FOREIGN KEY (Proyectos_idProyectos) REFERENCES Proyectos(idProyectos)
);
GO

CREATE TABLE Historial_de_cambios (
    idHistorial_de_cambios INT PRIMARY KEY,
    Tareas_idTareas INT,
    Proyectos_idProyectos INT,
    Portafolio_idPortafolio INT,
    Descripcioncambio NVARCHAR(MAX),
    FechaCambio DATETIME,
    FOREIGN KEY (Tareas_idTareas) REFERENCES Tareas(idTareas),
    FOREIGN KEY (Proyectos_idProyectos) REFERENCES Proyectos(idProyectos),
    FOREIGN KEY (Portafolio_idPortafolio) REFERENCES Portafolio(idPortafolio)
);
GO