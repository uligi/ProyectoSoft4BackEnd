

CREATE DATABASE DB_GP; --GestionProjectos
GO
USE DB_GP;
GO

CREATE TABLE Equipos (
    idEquipos INT PRIMARY KEY IDENTITY(1,1),
    NombreEquipos VARCHAR(45),
    Activo BIT,
    Fecha_Registro DATETIME
);
go

CREATE TABLE Portafolio (
    idPortafolio INT PRIMARY KEY IDENTITY(1,1),
    NombrePortafolio VARCHAR(300),
    Activo BIT,
    Descripcion NVARCHAR(MAX),
    FechaCreacion DATETIME
);
go

CREATE TABLE Proyectos (
    idProyectos INT PRIMARY KEY IDENTITY(1,1),
    NombreProyecto VARCHAR(500),
    Descripcion NVARCHAR(MAX),
    Activo BIT,
    FechaEstimada DATE,
    FechaInicio DATE,
    FechaFinal DATE,
    Prioridad VARCHAR(45),
    idPortafolio INT,
    Equipos_idEquipos INT NOT NULL,
    FOREIGN KEY (idPortafolio) REFERENCES Portafolio(idPortafolio),
    FOREIGN KEY (Equipos_idEquipos) REFERENCES Equipos(idEquipos)
);
go



CREATE TABLE Permisos (
    idPermisos INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Permisos VARCHAR(100),
    Activo BIT
);
go

CREATE TABLE Roles (
    idRoles INT PRIMARY KEY IDENTITY(1,1),
    Nombre_Roles VARCHAR(100),
    Activo BIT,
	idPermisos int
	 FOREIGN KEY (idPermisos) REFERENCES Permisos(idPermisos)
);
go


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
go

CREATE TABLE Miembros_de_equipos (
    idMiembros_de_equipos INT PRIMARY KEY IDENTITY(1,1),
    idEquipos INT,
    idUsuarios INT,
    
    FOREIGN KEY (idEquipos) REFERENCES Equipos(idEquipos),
    FOREIGN KEY (idUsuarios) REFERENCES Usuarios(idUsuarios),
    
);
go




CREATE TABLE Tareas (
    idTareas INT PRIMARY KEY IDENTITY(1,1),
    NombreTareas VARCHAR(45) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Prioridad VARCHAR(45),
    FechaInicio DATE,
    FechaFinal DATE,
    Activo BIT,
    idProyectos INT NOT NULL,
    idUsuarios INT NULL,
    FOREIGN KEY (idProyectos) REFERENCES Proyectos(idProyectos),
    FOREIGN KEY (idUsuarios) REFERENCES Usuarios(idUsuarios)
);
go

CREATE TABLE Subtareas (
    idSubtareas INT PRIMARY KEY IDENTITY(1,1),
    NombreSubtareas VARCHAR(45) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Prioridad VARCHAR(45),
    FechaInicio DATE,
    FechaFinal DATE,
    idTareas INT NOT NULL,
    FOREIGN KEY (idTareas) REFERENCES Tareas(idTareas)
);
go

CREATE TABLE Comentarios (
    idComentarios INT PRIMARY KEY IDENTITY(1,1),
    Comentario NVARCHAR(MAX),
    FechaCreacion DATETIME,
    Activo BIT,
    Tareas_idTareas INT NULL,
    idSubtareas INT NULL,
    idProyectos INT NULL,
    FOREIGN KEY (Tareas_idTareas) REFERENCES Tareas(idTareas),
    FOREIGN KEY (idSubtareas) REFERENCES Subtareas(idSubtareas),
    FOREIGN KEY (idProyectos) REFERENCES Proyectos(idProyectos)
);
go

CREATE TABLE Historial_de_cambios (
    idHistorial_de_cambios INT PRIMARY KEY IDENTITY(1,1),
    idTareas INT null,
    Portafolio_idPortafolio INT,	
    Descripcioncambio NVARCHAR(MAX),
    FechaCambio DATETIME,
	idProyectos int null,
	idPortafolio int null,
	idUsuarios int null,
	idSubtareas int null
    FOREIGN KEY (idTareas) REFERENCES Tareas(idTareas),
    FOREIGN KEY (idProyectos) REFERENCES Proyectos(idProyectos),
    FOREIGN KEY (idPortafolio) REFERENCES Portafolio(idPortafolio),
	FOREIGN KEY (idUsuarios) REFERENCES Usuarios(idUsuarios),
	FOREIGN KEY (idSubtareas) REFERENCES Subtareas(idSubtareas)
);
go