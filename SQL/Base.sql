

CREATE DATABASE DB_GP; --GestionProjectos
GO
USE DB_GP;
GO

CREATE TABLE Equipos (
    idEquipos INT PRIMARY KEY,
    NombreEquipos VARCHAR(45),
    Activo BIT,
    Fecha_Registro DATETIME
);

CREATE TABLE Portafolio (
    idPortafolio INT PRIMARY KEY IDENTITY(1,1),
    NombrePortafolio VARCHAR(300),
    Activo BIT,
    Descripcion NVARCHAR(MAX),
    FechaCreacion DATETIME
);

CREATE TABLE Proyectos (
    idProyectos INT PRIMARY KEY IDENTITY(1,1),
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

CREATE TABLE Subtareas (
    idSubtareas INT PRIMARY KEY IDENTITY(1,1),
    NombreSubtareas VARCHAR(45),
    Descripcion NVARCHAR(MAX),
    Prioridad VARCHAR(45),
    FechaInicio DATE,
    FechaFinal DATE
);

CREATE TABLE Comentarios (
    idComentarios INT PRIMARY KEY IDENTITY(1,1),
    Comentario NVARCHAR(MAX),
    FechaCreacion DATETIME,
    Activo BIT
);

CREATE TABLE Tareas (
    idTareas INT PRIMARY KEY IDENTITY(1,1),
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



CREATE TABLE Permisos (
    idPermisos INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Permisos VARCHAR(100),
    Activo BIT
);

CREATE TABLE Roles (
    idRoles INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Roles VARCHAR(100),
    Activo BIT,
	idPermisos int
	 FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);



CREATE TABLE Usuarios (
    idUsuarios INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(200),
    Email VARCHAR(200) UNIQUE,
    contrasena VARCHAR(500),
	RestablecerContrasena BIT,
    Activo BIT,
    FechaRegistro DATETIME,
	idRoles Int
	FOREIGN KEY (idRoles) REFERENCES Roles(idRoles)
   

);
CREATE TABLE Miembros_de_equipos (
    idMiembros_de_equipos INT PRIMARY KEY IDENTITY(1,1),
    Equipos_idEquipos INT,
    Usuarios_idUsuarios INT,
    RolesPermisos_idRolesPermisos INT,
    FOREIGN KEY (Equipos_idEquipos) REFERENCES Equipos(idEquipos),
    FOREIGN KEY (Usuarios_idUsuarios) REFERENCES Usuarios(idUsuarios),
    FOREIGN KEY (RolesPermisos_idRolesPermisos) REFERENCES RolesPermisos(idRolesPermisos)
);



CREATE TABLE Historial_de_cambios (
    idHistorial_de_cambios INT PRIMARY KEY IDENTITY(1,1),
    Tareas_idTareas INT,
    Proyectos_idProyectos INT,
    Portafolio_idPortafolio INT,
    Descripcioncambio NVARCHAR(MAX),
    FechaCambio DATETIME,
    FOREIGN KEY (Tareas_idTareas) REFERENCES Tareas(idTareas),
    FOREIGN KEY (Proyectos_idProyectos) REFERENCES Proyectos(idProyectos),
    FOREIGN KEY (Portafolio_idPortafolio) REFERENCES Portafolio(idPortafolio)
);


