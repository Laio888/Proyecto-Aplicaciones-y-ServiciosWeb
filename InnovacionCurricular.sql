-- 1
CREATE TABLE universidad (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(60) NOT NULL,
  tipo NVARCHAR(45) NOT NULL,
  ciudad NVARCHAR(45) NOT NULL
);
GO

-- 2
CREATE TABLE area_conocimiento (
  id INT NOT NULL PRIMARY KEY,
  gran_area NVARCHAR(60) NOT NULL,
  area NVARCHAR(60) NOT NULL,
  disciplina NVARCHAR(60) NOT NULL
);
GO

-- 3
CREATE TABLE enfoque (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL
);
GO

-- 4
CREATE TABLE aspecto_normativo (
  id INT NOT NULL PRIMARY KEY,
  tipo NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL,
  fuente NVARCHAR(45) NOT NULL
);
GO

-- 5
CREATE TABLE practica_estrategia (
  id INT NOT NULL PRIMARY KEY,
  tipo NVARCHAR(45) NOT NULL,
  nombre NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL
);
GO

-- 6
CREATE TABLE car_innovacion (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(MAX) NOT NULL,
  tipo NVARCHAR(45) NOT NULL
);
GO

-- 7
CREATE TABLE aliado (
  nit BIGINT NOT NULL PRIMARY KEY,
  razon_social NVARCHAR(60) NOT NULL,
  nombre_contacto NVARCHAR(60) NOT NULL,
  correo NVARCHAR(70) NOT NULL,
  telefono NVARCHAR(45) NOT NULL,
  ciudad NVARCHAR(45) NOT NULL
);
GO

-- 8
CREATE TABLE facultad (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(60) NOT NULL,
  tipo NVARCHAR(45) NOT NULL,
  fecha_fun DATE NOT NULL,
  universidad INT NOT NULL,
  CONSTRAINT fk_facultad_universidad FOREIGN KEY (universidad)
    REFERENCES universidad (id)
);
GO

-- 9
CREATE TABLE programa (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(60) NOT NULL,
  tipo NVARCHAR(45) NOT NULL,
  nivel NVARCHAR(45) NOT NULL,
  fecha_creacion NVARCHAR(45) NOT NULL,
  fecha_cierre NVARCHAR(45) NULL,
  numero_cohortes NVARCHAR(45) NOT NULL,
  cant_graduados NVARCHAR(45) NOT NULL,
  fecha_actualizacion NVARCHAR(45) NOT NULL,
  ciudad NVARCHAR(45) NOT NULL,
  facultad INT NOT NULL,
  CONSTRAINT fk_programa_facultad FOREIGN KEY (facultad)
    REFERENCES facultad (id)
);
GO

-- 10
CREATE TABLE docente (
  cedula INT NOT NULL PRIMARY KEY,
  nombres NVARCHAR(60) NOT NULL,
  apellidos NVARCHAR(60) NOT NULL,
  genero NVARCHAR(12) NOT NULL,
  cargo NVARCHAR(30) NOT NULL,
  fecha_nacimiento DATE NOT NULL,
  correo NVARCHAR(70) NOT NULL,
  telefono NVARCHAR(20) NOT NULL,
  url_cvlac NVARCHAR(128) NOT NULL,
  fecha_actualizacion DATE NOT NULL,
  escalafon NVARCHAR(45) NOT NULL,
  perfil NVARCHAR(MAX) NOT NULL,
  cat_minciencia NVARCHAR(45) NULL,
  conv_minciencia NVARCHAR(45) NOT NULL,
  nacionalidaad NVARCHAR(45) NOT NULL
);
GO

-- 11
CREATE TABLE registro_calificado (
  codigo INT NOT NULL PRIMARY KEY,
  cant_creditos NVARCHAR(45) NOT NULL,
  hora_acom NVARCHAR(45) NOT NULL,
  hora_ind NVARCHAR(45) NOT NULL,
  metodologia NVARCHAR(45) NOT NULL,
  fecha_inicio DATE NOT NULL,
  fecha_fin DATE NOT NULL,
  duracion_anios NVARCHAR(45) NOT NULL,
  duracion_semestres NVARCHAR(45) NOT NULL,
  tipo_titulacion NVARCHAR(45) NOT NULL,
  programa INT NOT NULL,
  CONSTRAINT fk_registro_calificado_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
);
GO

-- 12
CREATE TABLE activ_academica (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  num_creditos INT NOT NULL,
  tipo NVARCHAR(20) NOT NULL,
  area_formacion NVARCHAR(45) NOT NULL,
  h_acom INT NOT NULL,
  h_indep INT NOT NULL,
  idioma NVARCHAR(45) NOT NULL,
  espejo BIT NOT NULL,
  entidad_espejo NVARCHAR(45) NOT NULL,
  pais_espejo NVARCHAR(45) NOT NULL,
  disenio INT NULL,
  CONSTRAINT fk_activ_academica_programa FOREIGN KEY (disenio)
    REFERENCES programa (id)
);
GO

-- 13
CREATE TABLE acreditacion (
  resolucion INT NOT NULL PRIMARY KEY,
  tipo NVARCHAR(45) NOT NULL,
  calificacion NVARCHAR(45) NOT NULL,
  fecha_inicio NVARCHAR(45) NOT NULL,
  fecha_fin NVARCHAR(45) NOT NULL,
  programa INT NOT NULL,
  CONSTRAINT fk_acreditacion_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
);
GO

-- 14
CREATE TABLE pasantia (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  pais NVARCHAR(45) NOT NULL,
  empresa NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL,
  programa INT NOT NULL,
  CONSTRAINT fk_pasantia_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
);
GO

-- 15
CREATE TABLE premio (
  id INT NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL,
  fecha DATE NOT NULL,
  entidad_otorgante NVARCHAR(45) NOT NULL,
  pais NVARCHAR(45) NOT NULL,
  programa INT NOT NULL,
  CONSTRAINT fk_premio_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
);
GO

-- 16
CREATE TABLE docente_departamento (
  docente INT NOT NULL,
  departamento INT NOT NULL,
  dedicacion NVARCHAR(15) NOT NULL,
  modalidad NVARCHAR(45) NOT NULL,
  fecha_ingreso DATE NOT NULL,
  fecha_salida DATE NULL,
  PRIMARY KEY (docente, departamento),
  CONSTRAINT fk_docente_departamento_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula),
  CONSTRAINT fk_docente_departamento_programa FOREIGN KEY (departamento)
    REFERENCES programa (id)
);
GO

-- 17
CREATE TABLE alianza (
  aliado BIGINT NOT NULL,
  departamento INT NOT NULL,
  fecha_inicio DATE NOT NULL,
  fecha_fin DATE NULL,
  docente INT NULL,
  PRIMARY KEY (aliado, departamento),
  CONSTRAINT fk_alianza_aliado FOREIGN KEY (aliado)
    REFERENCES aliado (nit),
  CONSTRAINT fk_alianza_departamento FOREIGN KEY (departamento)
    REFERENCES programa (id),
  CONSTRAINT fk_alianza_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
);
GO

-- 18
CREATE TABLE enfoque_rc (
  enfoque INT NOT NULL,
  registro_calificado INT NOT NULL,
  PRIMARY KEY (enfoque, registro_calificado),
  CONSTRAINT fk_enfoque_rc_enfoque FOREIGN KEY (enfoque)
    REFERENCES enfoque (id),
  CONSTRAINT fk_enfoque_rc_registro FOREIGN KEY (registro_calificado)
    REFERENCES registro_calificado (codigo)
);
GO

-- 19
CREATE TABLE an_programa (
  aspecto_normativo INT NOT NULL,
  programa INT NOT NULL,
  PRIMARY KEY (aspecto_normativo, programa),
  CONSTRAINT fk_an_programa_aspecto FOREIGN KEY (aspecto_normativo)
    REFERENCES aspecto_normativo (id),
  CONSTRAINT fk_an_programa_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
);
GO

-- 20
CREATE TABLE programa_pe (
  programa INT NOT NULL,
  practica_estrategia INT NOT NULL,
  PRIMARY KEY (programa, practica_estrategia),
  CONSTRAINT fk_programa_pe_programa FOREIGN KEY (programa)
    REFERENCES programa (id),
  CONSTRAINT fk_programa_pe_practica FOREIGN KEY (practica_estrategia)
    REFERENCES practica_estrategia (id)
);
GO

-- 21
CREATE TABLE aa_rc (
  activ_academicas_idcurso INT NOT NULL,
  registro_calificado_codigo INT NOT NULL,
  componente NVARCHAR(45) NOT NULL,
  semestre NVARCHAR(45) NOT NULL,
  PRIMARY KEY (activ_academicas_idcurso, registro_calificado_codigo),
  CONSTRAINT fk_aa_rc_activ FOREIGN KEY (activ_academicas_idcurso)
    REFERENCES activ_academica (id),
  CONSTRAINT fk_aa_rc_registro FOREIGN KEY (registro_calificado_codigo)
    REFERENCES registro_calificado (codigo)
);
GO

-- 22
CREATE TABLE programa_ci (
  programa INT NOT NULL,
  car_innovacion INT NOT NULL,
  PRIMARY KEY (programa, car_innovacion),
  CONSTRAINT fk_programa_ci_programa FOREIGN KEY (programa)
    REFERENCES programa (id),
  CONSTRAINT fk_programa_ci_car FOREIGN KEY (car_innovacion)
    REFERENCES car_innovacion (id)
);
GO

-- 23
CREATE TABLE programa_ac (
  programa INT NOT NULL,
  area_conocimiento INT NOT NULL,
  PRIMARY KEY (programa, area_conocimiento),
  CONSTRAINT fk_programa_ac_programa FOREIGN KEY (programa)
    REFERENCES programa (id),
  CONSTRAINT fk_programa_ac_area FOREIGN KEY (area_conocimiento)
    REFERENCES area_conocimiento (id)
);
GO