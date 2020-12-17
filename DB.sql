CREATE TABLE COMPENSACION 
    (
    [id]                  NUMERIC (10) NOT NULL,
    [nombre]                VARCHAR (MAX) NULL,
    [telefono]             VARCHAR (MAX) NULL,
    [nombreEmpresa]              VARCHAR (MAX) NULL,
    [pais]          VARCHAR (MAX) NULL,
    [mail]       VARCHAR (MAX) NULL,
    [toneladas]         VARCHAR (MAX) NULL,
    [compensacion]              VARCHAR (MAX) NULL
    )
GO

ALTER TABLE COMPENSACION ADD CONSTRAINT COMPENSACION_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO