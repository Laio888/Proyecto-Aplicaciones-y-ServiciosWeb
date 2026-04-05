-- Tabla universidad
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='universidad' AND xtype='U')
CREATE TABLE universidad (
  id    INT          NOT NULL PRIMARY KEY,
  nombre NVARCHAR(60) NOT NULL,
  tipo   NVARCHAR(45) NOT NULL,
  ciudad NVARCHAR(45) NOT NULL
);
GO

-- Tabla linea_investigacion
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='linea_investigacion' AND xtype='U')
CREATE TABLE linea_investigacion (
  id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  nombre      NVARCHAR(45)  NOT NULL,
  descripcion NVARCHAR(256) NOT NULL
);
GO

-- Tabla termino_clave
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='termino_clave' AND xtype='U')
CREATE TABLE termino_clave (
  termino        NVARCHAR(30) NOT NULL PRIMARY KEY,
  termino_ingles NVARCHAR(30) NULL
);
GO

-- Tabla area_aplicacion
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='area_aplicacion' AND xtype='U')
CREATE TABLE area_aplicacion (
  id     INT          NOT NULL PRIMARY KEY,
  nombre NVARCHAR(60) NOT NULL
);
GO

-- Tabla objetivo_desarrollo_sostenible
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='objetivo_desarrollo_sostenible' AND xtype='U')
CREATE TABLE objetivo_desarrollo_sostenible (
  id        INT          NOT NULL PRIMARY KEY,
  nombre    NVARCHAR(60) NOT NULL,
  categoria NVARCHAR(45) NOT NULL
);
GO

-- Tabla area_conocimiento
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='area_conocimiento' AND xtype='U')
CREATE TABLE area_conocimiento (
  id          INT          NOT NULL PRIMARY KEY,
  gran_area   NVARCHAR(60) NOT NULL,
  area        NVARCHAR(60) NOT NULL,
  disciplina  NVARCHAR(60) NOT NULL
);
GO

-- Tabla aliado
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aliado' AND xtype='U')
CREATE TABLE aliado (
  nit             BIGINT       NOT NULL PRIMARY KEY,
  razon_social    NVARCHAR(60) NOT NULL,
  nombre_contacto NVARCHAR(60) NOT NULL,
  correo          NVARCHAR(70) NOT NULL,
  telefono        NVARCHAR(45) NOT NULL,
  ciudad          NVARCHAR(45) NOT NULL
);
GO

-- Tabla proyecto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='proyecto' AND xtype='U')
CREATE TABLE proyecto (
  id               INT          NOT NULL PRIMARY KEY,
  titulo           NVARCHAR(70) NOT NULL,
  resumen          NVARCHAR(256) NOT NULL,
  presupuesto      FLOAT        NOT NULL,
  tipo_financiacion NVARCHAR(45) NOT NULL,
  tipo_fondos      NVARCHAR(45) NOT NULL,
  fecha_inicio     DATE         NOT NULL,
  fecha_fin        DATE         NULL
);
GO

-- Tabla tipo_producto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tipo_producto' AND xtype='U')
CREATE TABLE tipo_producto (
  id        INT          NOT NULL PRIMARY KEY,
  categoria NVARCHAR(45) NOT NULL,
  clase     NVARCHAR(45) NOT NULL,
  nombre    NVARCHAR(45) NOT NULL,
  tipologia NVARCHAR(45) NOT NULL
);
GO

-- Tabla enfoque
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='enfoque' AND xtype='U')
CREATE TABLE enfoque (
  id          INT          NOT NULL PRIMARY KEY,
  nombre      NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL
);
GO

-- Tabla aspecto_normativo
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aspecto_normativo' AND xtype='U')
CREATE TABLE aspecto_normativo (
  id          INT          NOT NULL PRIMARY KEY,
  tipo        NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL,
  fuente      NVARCHAR(45) NOT NULL
);
GO

-- Tabla practica_estrategia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='practica_estrategia' AND xtype='U')
CREATE TABLE practica_estrategia (
  id          INT          NOT NULL PRIMARY KEY,
  tipo        NVARCHAR(45) NOT NULL,
  nombre      NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL
);
GO

-- Tabla car_innovacion
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='car_innovacion' AND xtype='U')
CREATE TABLE car_innovacion (
  id          INT           NOT NULL PRIMARY KEY,
  nombre      NVARCHAR(45)  NOT NULL,
  descripcion NVARCHAR(MAX) NOT NULL,
  tipo        NVARCHAR(45)  NOT NULL
);
GO

-- Tabla red
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='red' AND xtype='U')
CREATE TABLE red (
  idr    INT          NOT NULL PRIMARY KEY,
  nombre NVARCHAR(45) NOT NULL,
  url    NVARCHAR(45) NOT NULL,
  pais   NVARCHAR(45) NOT NULL
);
GO


-- Tabla facultad
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='facultad' AND xtype='U')
CREATE TABLE facultad (
  id          INT          NOT NULL PRIMARY KEY,
  nombre      NVARCHAR(60) NOT NULL,
  tipo        NVARCHAR(45) NOT NULL,
  fecha_fun   DATE         NOT NULL,
  universidad INT          NOT NULL,
  CONSTRAINT fk_unidad_sede FOREIGN KEY (universidad)
    REFERENCES universidad (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla grupo_investigacion
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='grupo_investigacion' AND xtype='U')
CREATE TABLE grupo_investigacion (
  id              INT           NOT NULL PRIMARY KEY,
  nombre          NVARCHAR(60)  NOT NULL,
  url_gruplac     NVARCHAR(128) NULL,
  categoria       NVARCHAR(10)  NULL,
  convocatoria    NVARCHAR(10)  NULL,
  fecha_fundacion DATE          NOT NULL,
  universidad     INT           NULL,
  interno         BIT           NOT NULL,
  ambito          NVARCHAR(45)  NOT NULL,
  CONSTRAINT fk_grupo_investigacion_sede FOREIGN KEY (universidad)
    REFERENCES universidad (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla programa
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='programa' AND xtype='U')
CREATE TABLE programa (
  id                   INT          NOT NULL PRIMARY KEY,
  nombre               NVARCHAR(60) NOT NULL,
  tipo                 NVARCHAR(45) NOT NULL,
  nivel                NVARCHAR(45) NOT NULL,
  fecha_creacion       NVARCHAR(45) NOT NULL,
  fecha_cierre         NVARCHAR(45) NULL,
  numero_cohortes      NVARCHAR(45) NOT NULL,
  cant_graduados       NVARCHAR(45) NOT NULL,
  fecha_actualizacion  NVARCHAR(45) NOT NULL,
  ciudad               NVARCHAR(45) NOT NULL,
  facultad             INT          NOT NULL,
  CONSTRAINT fk_programa_facultad FOREIGN KEY (facultad)
    REFERENCES facultad (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla docente
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='docente' AND xtype='U')
CREATE TABLE docente (
  cedula                      INT           NOT NULL PRIMARY KEY,
  nombres                     NVARCHAR(60)  NOT NULL,
  apellidos                   NVARCHAR(60)  NOT NULL,
  genero                      NVARCHAR(12)  NOT NULL,
  cargo                       NVARCHAR(30)  NOT NULL,
  fecha_nacimiento            DATE          NOT NULL,
  correo                      NVARCHAR(70)  NOT NULL,
  telefono                    NVARCHAR(20)  NOT NULL,
  url_cvlac                   NVARCHAR(128) NOT NULL,
  fecha_actualizacion         DATE          NOT NULL,
  escalafon                   NVARCHAR(45)  NOT NULL,
  perfil                      NVARCHAR(MAX) NOT NULL,
  cat_minciencia              NVARCHAR(45)  NULL,
  conv_minciencia             NVARCHAR(45)  NOT NULL,
  nacionalidaad               NVARCHAR(45)  NOT NULL,
  linea_investigacion_principal INT          NULL,
  CONSTRAINT fk_docente_linea_investigacion FOREIGN KEY (linea_investigacion_principal)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla estudios_realizados
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='estudios_realizados' AND xtype='U')
CREATE TABLE estudios_realizados (
  id              INT           NOT NULL PRIMARY KEY,
  titulo          NVARCHAR(45)  NOT NULL,
  universidad     NVARCHAR(50)  NOT NULL,
  fecha           DATE          NOT NULL,
  tipo            NVARCHAR(45)  NOT NULL,
  ciudad          NVARCHAR(45)  NOT NULL,
  docente         INT           NOT NULL,
  ins_acreditada  BIT           NOT NULL,
  metodologia     NVARCHAR(45)  NOT NULL,
  perfil_egresado NVARCHAR(MAX) NOT NULL,
  pais            NVARCHAR(45)  NOT NULL,
  CONSTRAINT fk_estudio_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla evaluacion_docente
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='evaluacion_docente' AND xtype='U')
CREATE TABLE evaluacion_docente (
  id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  calificacion FLOAT        NOT NULL,
  semestre     NVARCHAR(45) NOT NULL,
  docente      INT          NOT NULL,
  CONSTRAINT fk_evaluacion_docente_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla reconocimiento
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='reconocimiento' AND xtype='U')
CREATE TABLE reconocimiento (
  id          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  tipo        NVARCHAR(45) NOT NULL,
  fecha       DATE         NOT NULL,
  institucion NVARCHAR(45) NOT NULL,
  nombre      NVARCHAR(45) NOT NULL,
  ambito      NVARCHAR(45) NOT NULL,
  docente     INT          NOT NULL,
  CONSTRAINT fk_reconocimiento_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla experiencia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='experiencia' AND xtype='U')
CREATE TABLE experiencia (
  id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  nombre_cargo NVARCHAR(45) NOT NULL,
  institucion  NVARCHAR(45) NOT NULL,
  tipo         NVARCHAR(45) NOT NULL,
  fecha_inicio DATE         NOT NULL,
  fecha_fin    DATE         NULL,
  docente      INT          NOT NULL,
  CONSTRAINT fk_experiencia_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla apoyo_profesoral
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='apoyo_profesoral' AND xtype='U')
CREATE TABLE apoyo_profesoral (
  estudios    INT          NOT NULL PRIMARY KEY,
  con_apoyo   BIT          NOT NULL,
  institucion NVARCHAR(45) NOT NULL,
  tipo        NVARCHAR(45) NOT NULL,
  CONSTRAINT fk_apoyo_profesoral_estudios1 FOREIGN KEY (estudios)
    REFERENCES estudios_realizados (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla beca
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='beca' AND xtype='U')
CREATE TABLE beca (
  estudios     INT          NOT NULL PRIMARY KEY,
  tipo         NVARCHAR(45) NOT NULL,
  institucion  NVARCHAR(80) NOT NULL,
  fecha_inicio DATE         NOT NULL,
  fecha_fin    DATE         NULL,
  CONSTRAINT fk_beca_estudios1 FOREIGN KEY (estudios)
    REFERENCES estudios_realizados (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla registro_calificado
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='registro_calificado' AND xtype='U')
CREATE TABLE registro_calificado (
  codigo              INT          NOT NULL PRIMARY KEY,
  cant_creditos       NVARCHAR(45) NOT NULL,
  hora_acom           NVARCHAR(45) NOT NULL,
  hora_ind            NVARCHAR(45) NOT NULL,
  metodologia         NVARCHAR(45) NOT NULL,
  fecha_inicio        DATE         NOT NULL,
  fecha_fin           DATE         NOT NULL,
  duracion_anios      NVARCHAR(45) NOT NULL,
  duracion_semestres  NVARCHAR(45) NOT NULL,
  tipo_titulacion     NVARCHAR(45) NOT NULL,
  programa            INT          NOT NULL,
  CONSTRAINT fk_registro_calificado_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla activ_academica
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='activ_academica' AND xtype='U')
CREATE TABLE activ_academica (
  id             INT          NOT NULL PRIMARY KEY,
  nombre         NVARCHAR(45) NOT NULL,
  num_creditos   INT          NOT NULL,
  tipo           NVARCHAR(20) NOT NULL,
  area_formacion NVARCHAR(45) NOT NULL,
  h_acom         INT          NOT NULL,
  h_indep        INT          NOT NULL,
  idioma         NVARCHAR(45) NOT NULL,
  espejo         BIT          NOT NULL,
  entidad_espejo NVARCHAR(45) NOT NULL,
  pais_espejo    NVARCHAR(45) NOT NULL,
  disenio        INT          NULL,
  CONSTRAINT fk_activ_academicas_programa FOREIGN KEY (disenio)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla acreditacion
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='acreditacion' AND xtype='U')
CREATE TABLE acreditacion (
  resolucion   INT          NOT NULL PRIMARY KEY,
  tipo         NVARCHAR(45) NOT NULL,
  calificacion NVARCHAR(45) NOT NULL,
  fecha_inicio NVARCHAR(45) NOT NULL,
  fecha_fin    NVARCHAR(45) NOT NULL,
  programa     INT          NOT NULL,
  CONSTRAINT fk_acreditacion_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla pasantia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='pasantia' AND xtype='U')
CREATE TABLE pasantia (
  id          INT          NOT NULL PRIMARY KEY,
  nombre      NVARCHAR(45) NOT NULL,
  pais        NVARCHAR(45) NOT NULL,
  empresa     NVARCHAR(45) NOT NULL,
  descripcion NVARCHAR(45) NOT NULL,
  programa    INT          NOT NULL,
  CONSTRAINT fk_pasantia_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla premio
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='premio' AND xtype='U')
CREATE TABLE premio (
  id                INT          NOT NULL PRIMARY KEY,
  nombre            NVARCHAR(45) NOT NULL,
  descripcion       NVARCHAR(45) NOT NULL,
  fecha             DATE         NOT NULL,
  entidad_otorgante NVARCHAR(45) NOT NULL,
  pais              NVARCHAR(45) NOT NULL,
  programa          INT          NOT NULL,
  CONSTRAINT fk_premio_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO


-- Tabla semillero
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='semillero' AND xtype='U')
CREATE TABLE semillero (
  id                  INT          NOT NULL PRIMARY KEY,
  nombre              NVARCHAR(60) NOT NULL,
  fecha_fundacion     DATE         NOT NULL,
  grupo_investigacion INT          NOT NULL,
  CONSTRAINT fk_semillero_grupo_investigacion FOREIGN KEY (grupo_investigacion)
    REFERENCES grupo_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla producto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='producto' AND xtype='U')
CREATE TABLE producto (
  id            INT          NOT NULL PRIMARY KEY,
  nombre        NVARCHAR(45) NOT NULL,
  categoria     NVARCHAR(45) NOT NULL,
  fecha_entrega DATE         NOT NULL,
  proyecto      INT          NULL,
  tipo_producto INT          NOT NULL,
  CONSTRAINT fk_producto_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_producto_tipo_producto FOREIGN KEY (tipo_producto)
    REFERENCES tipo_producto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla semillero_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='semillero_linea' AND xtype='U')
CREATE TABLE semillero_linea (
  semillero           INT NOT NULL,
  linea_investigacion INT NOT NULL,
  PRIMARY KEY (semillero, linea_investigacion),
  CONSTRAINT fk_semillero_linea_semillero FOREIGN KEY (semillero)
    REFERENCES semillero (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_semillero_linea_linea FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla grupo_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='grupo_linea' AND xtype='U')
CREATE TABLE grupo_linea (
  grupo_investigacion INT NOT NULL,
  linea_investigacion INT NOT NULL,
  PRIMARY KEY (grupo_investigacion, linea_investigacion),
  CONSTRAINT fk_grupo_linea_grupo FOREIGN KEY (grupo_investigacion)
    REFERENCES grupo_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_grupo_linea_linea FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla docente_departamento
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='docente_departamento' AND xtype='U')
CREATE TABLE docente_departamento (
  docente      INT          NOT NULL,
  departamento INT          NOT NULL,
  dedicacion   NVARCHAR(15) NOT NULL,
  modalidad    NVARCHAR(45) NOT NULL,
  fecha_ingreso DATE        NOT NULL,
  fecha_salida  DATE        NULL,
  PRIMARY KEY (docente, departamento),
  CONSTRAINT fk_docente_departamento_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_docente_departamento_departamento FOREIGN KEY (departamento)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla participa_semillero
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='participa_semillero' AND xtype='U')
CREATE TABLE participa_semillero (
  docente      INT          NOT NULL,
  semillero    INT          NOT NULL,
  rol          NVARCHAR(15) NOT NULL,
  fecha_inicio DATE         NOT NULL,
  fecha_fin    DATE         NULL,
  PRIMARY KEY (docente, semillero),
  CONSTRAINT fk_participa_semillero_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_participa_semillero_semillero FOREIGN KEY (semillero)
    REFERENCES semillero (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla participa_grupo
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='participa_grupo' AND xtype='U')
CREATE TABLE participa_grupo (
  docente_cedula         INT          NOT NULL,
  grupo_investigacion_id INT          NOT NULL,
  rol                    NVARCHAR(15) NOT NULL,
  fecha_inicio           DATE         NOT NULL,
  fecha_fin              DATE         NULL,
  PRIMARY KEY (docente_cedula, grupo_investigacion_id),
  CONSTRAINT fk_participa_grupo_docente FOREIGN KEY (docente_cedula)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_participa_grupo_grupo FOREIGN KEY (grupo_investigacion_id)
    REFERENCES grupo_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla alianza
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='alianza' AND xtype='U')
CREATE TABLE alianza (
  aliado       BIGINT NOT NULL,
  departamento INT    NOT NULL,
  fecha_inicio DATE   NOT NULL,
  fecha_fin    DATE   NULL,
  docente      INT    NULL,
  PRIMARY KEY (aliado, departamento),
  CONSTRAINT fk_alianza_aliado FOREIGN KEY (aliado)
    REFERENCES aliado (nit)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_alianza_departamento FOREIGN KEY (departamento)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_alianza_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla aliado_proyecto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aliado_proyecto' AND xtype='U')
CREATE TABLE aliado_proyecto (
  aliado   BIGINT NOT NULL,
  proyecto INT    NOT NULL,
  PRIMARY KEY (aliado, proyecto),
  CONSTRAINT fk_aliado_proyecto_aliado FOREIGN KEY (aliado)
    REFERENCES aliado (nit)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_aliado_proyecto_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla desarrolla
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='desarrolla' AND xtype='U')
CREATE TABLE desarrolla (
  docente     INT           NOT NULL,
  proyecto    INT           NOT NULL,
  rol         NVARCHAR(45)  NOT NULL,
  descripcion NVARCHAR(256) NOT NULL,
  PRIMARY KEY (docente, proyecto),
  CONSTRAINT fk_desarrolla_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_desarrolla_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla palabras_clave
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='palabras_clave' AND xtype='U')
CREATE TABLE palabras_clave (
  proyecto      INT          NOT NULL,
  termino_clave NVARCHAR(30) NOT NULL,
  PRIMARY KEY (proyecto, termino_clave),
  CONSTRAINT fk_palabras_clave_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_palabras_clave_termino_clave FOREIGN KEY (termino_clave)
    REFERENCES termino_clave (termino)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla ac_proyecto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ac_proyecto' AND xtype='U')
CREATE TABLE ac_proyecto (
  proyecto         INT NOT NULL,
  area_conocimiento INT NOT NULL,
  PRIMARY KEY (proyecto, area_conocimiento),
  CONSTRAINT fk_ac_proyecto_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_ac_proyecto_area_conocimiento FOREIGN KEY (area_conocimiento)
    REFERENCES area_conocimiento (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla proyecto_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='proyecto_linea' AND xtype='U')
CREATE TABLE proyecto_linea (
  proyecto            INT NOT NULL,
  linea_investigacion INT NOT NULL,
  PRIMARY KEY (proyecto, linea_investigacion),
  CONSTRAINT fk_proyecto_linea_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_proyecto_linea_linea_investigacion FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla ods_proyecto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ods_proyecto' AND xtype='U')
CREATE TABLE ods_proyecto (
  proyecto INT NOT NULL,
  ods      INT NOT NULL,
  PRIMARY KEY (proyecto, ods),
  CONSTRAINT fk_ods_proyecto_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_ods_proyecto_ods FOREIGN KEY (ods)
    REFERENCES objetivo_desarrollo_sostenible (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla aa_proyecto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aa_proyecto' AND xtype='U')
CREATE TABLE aa_proyecto (
  proyecto        INT NOT NULL,
  area_aplicacion INT NOT NULL,
  PRIMARY KEY (proyecto, area_aplicacion),
  CONSTRAINT fk_aa_proyecto_proyecto FOREIGN KEY (proyecto)
    REFERENCES proyecto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_aa_proyecto_area_aplicacion FOREIGN KEY (area_aplicacion)
    REFERENCES area_aplicacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla ac_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ac_linea' AND xtype='U')
CREATE TABLE ac_linea (
  linea_investigacion INT NOT NULL,
  area_conocimiento   INT NOT NULL,
  PRIMARY KEY (linea_investigacion, area_conocimiento),
  CONSTRAINT fk_ac_linea_linea_investigacion FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_ac_linea_area_conocimie FOREIGN KEY (area_conocimiento)
    REFERENCES area_conocimiento (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla ods_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ods_linea' AND xtype='U')
CREATE TABLE ods_linea (
  linea_investigacion INT NOT NULL,
  ods                 INT NOT NULL,
  PRIMARY KEY (linea_investigacion, ods),
  CONSTRAINT fk_ods_linea_linea FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_ods_linea_ods FOREIGN KEY (ods)
    REFERENCES objetivo_desarrollo_sostenible (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla aa_linea
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aa_linea' AND xtype='U')
CREATE TABLE aa_linea (
  area_aplicacion     INT NOT NULL,
  linea_investigacion INT NOT NULL,
  PRIMARY KEY (area_aplicacion, linea_investigacion),
  CONSTRAINT fk_aa_linea_area_aplicacion FOREIGN KEY (area_aplicacion)
    REFERENCES area_aplicacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_aa_linea_linea_investigacion FOREIGN KEY (linea_investigacion)
    REFERENCES linea_investigacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla docente_producto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='docente_producto' AND xtype='U')
CREATE TABLE docente_producto (
  docente  INT NOT NULL,
  producto INT NOT NULL,
  PRIMARY KEY (docente, producto),
  CONSTRAINT fk_docente_producto_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_docente_producto_producto FOREIGN KEY (producto)
    REFERENCES producto (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla estudio_ac
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='estudio_ac' AND xtype='U')
CREATE TABLE estudio_ac (
  estudio           INT NOT NULL,
  area_conocimiento INT NOT NULL,
  PRIMARY KEY (estudio, area_conocimiento),
  CONSTRAINT fk_estudio_ac_estudio FOREIGN KEY (estudio)
    REFERENCES estudios_realizados (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_estudio_ac_area_conocimiento FOREIGN KEY (area_conocimiento)
    REFERENCES area_conocimiento (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla programa_ac
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='programa_ac' AND xtype='U')
CREATE TABLE programa_ac (
  programa          INT NOT NULL,
  area_conocimiento INT NOT NULL,
  PRIMARY KEY (programa, area_conocimiento),
  CONSTRAINT fk_programa_ac_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_programa_ac_area_conocimiento FOREIGN KEY (area_conocimiento)
    REFERENCES area_conocimiento (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla enfoque_rc
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='enfoque_rc' AND xtype='U')
CREATE TABLE enfoque_rc (
  enfoque             INT NOT NULL,
  registro_calificado INT NOT NULL,
  PRIMARY KEY (enfoque, registro_calificado),
  CONSTRAINT fk_enfoque_rc_enfoque FOREIGN KEY (enfoque)
    REFERENCES enfoque (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_enfoque_rc_registro_calificado FOREIGN KEY (registro_calificado)
    REFERENCES registro_calificado (codigo)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla an_programa
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='an_programa' AND xtype='U')
CREATE TABLE an_programa (
  aspecto_normativo INT NOT NULL,
  programa          INT NOT NULL,
  PRIMARY KEY (aspecto_normativo, programa),
  CONSTRAINT fk_an_programa_aspecto_normativo FOREIGN KEY (aspecto_normativo)
    REFERENCES aspecto_normativo (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_an_programa_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla programa_pe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='programa_pe' AND xtype='U')
CREATE TABLE programa_pe (
  programa           INT NOT NULL,
  practica_estrategia INT NOT NULL,
  PRIMARY KEY (programa, practica_estrategia),
  CONSTRAINT fk_programa_pe_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_programa_pe_practica_estrategia FOREIGN KEY (practica_estrategia)
    REFERENCES practica_estrategia (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla aa_rc
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='aa_rc' AND xtype='U')
CREATE TABLE aa_rc (
  activ_academicas_idcurso    INT          NOT NULL,
  registro_calificado_codigo  INT          NOT NULL,
  componente                  NVARCHAR(45) NOT NULL,
  semestre                    NVARCHAR(45) NOT NULL,
  PRIMARY KEY (activ_academicas_idcurso, registro_calificado_codigo),
  CONSTRAINT fk_aa_rc_activ_academica FOREIGN KEY (activ_academicas_idcurso)
    REFERENCES activ_academica (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_aa_rc_registro_calificado FOREIGN KEY (registro_calificado_codigo)
    REFERENCES registro_calificado (codigo)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla programa_ci
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='programa_ci' AND xtype='U')
CREATE TABLE programa_ci (
  programa      INT NOT NULL,
  car_innovacion INT NOT NULL,
  PRIMARY KEY (programa, car_innovacion),
  CONSTRAINT fk_programa_ci_programa FOREIGN KEY (programa)
    REFERENCES programa (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_programa_ci_car_innovacion FOREIGN KEY (car_innovacion)
    REFERENCES car_innovacion (id)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla intereses_futuros
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='intereses_futuros' AND xtype='U')
CREATE TABLE intereses_futuros (
  docente       INT          NOT NULL,
  termino_clave NVARCHAR(30) NOT NULL,
  PRIMARY KEY (docente, termino_clave),
  CONSTRAINT fk_intereses_futuros_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_intereses_futuros_termino_clave FOREIGN KEY (termino_clave)
    REFERENCES termino_clave (termino)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Tabla red_docente
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='red_docente' AND xtype='U')
CREATE TABLE red_docente (
  red           INT           NOT NULL,
  docente       INT           NOT NULL,
  fecha_inicio  DATE          NOT NULL,
  fecha_fin     NVARCHAR(45)  NULL,
  act_destacadas NVARCHAR(MAX) NOT NULL,
  PRIMARY KEY (red, docente),
  CONSTRAINT fk_red_docente_redes FOREIGN KEY (red)
    REFERENCES red (idr)
    ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_red_docente_docente FOREIGN KEY (docente)
    REFERENCES docente (cedula)
    ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO
