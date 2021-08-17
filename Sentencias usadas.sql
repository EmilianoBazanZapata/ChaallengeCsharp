--SELECCIONAR PERSONAJES

--SELECT *
--FROM Personajes;

--SELECT Nombre,imagen
--FROM Personajes;

--AGREGAR PEROSNAJE

/*CREATE PROCEDURE UP_AGREGAR_PERSONAJE
@NOMBRE NVARCHAR(50),
@EDAD INT,
@PESO FLOAT,
@HISTORIA NVARCHAR(50),
@IMAGEN NVARCHAR(50)
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR PERSONAJE
            INSERT INTO Personajes
            (Nombre, Edad, Peso, Historia, imagen, Activo)
            VALUES(@NOMBRE, @EDAD, @PESO, @HISTORIA, @IMAGEN , 1 );
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END*/


--EXEC UP_AGREGAR_PERSONAJE '', 0, 0,'a','AAAA' 

/* CREATE PROCEDURE UP_ACTUALIZAR_PERSONAJE
@NOMBRE NVARCHAR(50),
@EDAD INT,
@PESO FLOAT,
@HISTORIA NVARCHAR(50),
@IMAGEN NVARCHAR(50),
@ID INT
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --ACTUALIZAR PERSONAJE
            UPDATE Personajes
            SET Nombre=@NOMBRE, Edad=@EDAD, Peso=@PESO, Historia=@HISTORIA, imagen=@IMAGEN
            WHERE Id=@ID;
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

/* CREATE PROCEDURE UP_ELIMINAR_PERSONAJE
@ID INT
AS

BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR PERSONAJE
            UPDATE Personajes
            SET Activo = 0
            WHERE Id=@ID;
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

--SELECT Titulo, imagen , FechaCreacion
--FROM Peliculas;

/* CREATE PROCEDURE UP_AGREGAR_PELICULA
@TITULO NVARCHAR(50),
@FECHA DATETIME,
@CALIFICACION INT,
@IMAGEN NVARCHAR(50)
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR PELICULA
		 INSERT INTO Peliculas
		 (Titulo, FechaCreacion, Calificacion, imagen, Activo)
		 VALUES(@TITULO, @FECHA, @CALIFICACION, @IMAGEN, 1);
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

/* CREATE PROCEDURE UP_ACTUALIZAR_PELICULA
@TITULO NVARCHAR(50),
@FECHA DATETIME,
@CALIFICACION INT,
@IMAGEN NVARCHAR(50),
@ID INT
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --ACTUALIZAR PELICULA
		 	UPDATE Peliculas
			SET Titulo=@TITULO, FechaCreacion=@FECHA, Calificacion=@CALIFICACION, imagen=@IMAGEN
			WHERE Id=@ID;
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END

CREATE PROCEDURE UP_ELIMINAR_PELICULA
@ID INT
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --ELIMINAR PELICULA
		 	UPDATE Peliculas
			SET Activo = 0
			WHERE Id=@ID;
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

---RELACIONAR PELICULAS Y ACTORES
/* CREATE PROCEDURE UP_RELACIONAR_PELICULA_Y_ACTORES
@IDPELICULA INT,
@IDACTOR INT
AS
BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR RELACION ENTRE UNA PELICULA Y SUS ACTORES
         INSERT INTO PeliculaPersonaje
         (PeliculasId, PersonajesId)
         VALUES(@IDPELICULA, @IDACTOR);

	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END  */


--FUNCIONES PARA LAS BUSQUEDAS

--BUSCAR PERSONAJE POR NOMBRE

/* CREATE FUNCTION V_PER (@NOMBRE NVARCHAR(50))
RETURNS TABLE
AS
RETURN
SELECT Nombre, Edad, Peso, Historia, imagen
FROM Personajes P
WHERE P.Activo = 1
AND P.Nombre = @NOMBRE */

--BUSCAR PERSONAJE POR EDAD

/* CREATE FUNCTION V_PER_POR_EDAD (@EDAD INT)
RETURNS TABLE
AS
RETURN
SELECT Nombre, Edad, Peso, Historia, imagen
FROM Personajes P
WHERE P.Activo = 1
AND P.Edad = @EDAD  */

--VER DETALLE DE UN ACTOR

/* CREATE FUNCTION V_DETALLE_DEL_ACTOR (@ID_DE_ACTOR INT)
RETURNS TABLE
AS
RETURN
SELECT P.Id, P.Nombre, P.Edad, 
       P.Historia, P.imagen AS'IMAGEN DEL ACTOR',
       P.Peso,
       PL.Titulo, PL.imagen AS'IMAGEN DE LA PELICULA'
FROM PERSONAJES P , PELICULAS PL , PeliculaPersonaje PP
WHERE P.Id = PP.PersonajesId
AND PP.PeliculasId = PL.Id
AND PP.PersonajesId = @ID_DE_ACTOR */


/* CREATE FUNCTION V_VER_ACTORES_DE_UNA_PELICULA (@ID_DE_LA_PELICULA INT)
RETURNS TABLE
AS
RETURN
SELECT PL.Id, P.Nombre, P.Edad, 
       P.Historia, P.imagen AS'IMAGEN DEL ACTOR',
       P.Peso,
       PL.Titulo, PL.imagen AS'IMAGEN DE LA PELICULA' ,
       PL.Calificacion , PL.FechaCreacion 
FROM PERSONAJES P , PELICULAS PL , PeliculaPersonaje PP
WHERE P.Id = PP.PersonajesId
AND PP.PeliculasId = PL.Id
AND PP.PeliculasId = @ID_DE_LA_PELICULA */

/* CREATE PROCEDURE UP_AGREGAR_GENERO
@NOMBRE NVARCHAR(50),
@IMAGEN NVARCHAR(50)
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR GENERO
         INSERT INTO Generos
         (Nombre, imagen, Activo)
         VALUES(@NOMBRE, @IMAGEN, 1);
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

/* CREATE PROCEDURE UP_ACTUALIZAR_GENERO
@NOMBRE NVARCHAR(50),
@IMAGEN NVARCHAR(50),
@ID INT
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --ACTUALIZAR GENERO
         UPDATE Generos
         SET Nombre=@NOMBRE, imagen=@IMAGEN, Activo=0
         WHERE Id=@ID;
	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */


/* CREATE PROCEDURE UP_REGISTRAR_USUARIO
@EMAIL NVARCHAR(50),
@PASSWORD NVARCHAR(50)
AS


BEGIN
	BEGIN TRY
	     BEGIN TRANSACTION
		 --AGREGAR USUARIO
         INSERT INTO Usuarios
         (Email, Password)
         VALUES(@EMAIL , @PASSWORD);

	     COMMIT TRANSACTION
    END TRY	
	BEGIN CATCH
		ROLLBACK TRANSACTION
			
	    DECLARE @ErrorMessage NVARCHAR(4000);  
	    DECLARE @ErrorSeverity INT;  
	    DECLARE @ErrorState INT;  
  
		SELECT   
			@ErrorMessage = ERROR_MESSAGE(),  
	        @ErrorSeverity = ERROR_SEVERITY(),  
		    @ErrorState = ERROR_STATE();  
  
	    RAISERROR (@ErrorMessage,   
				   @ErrorSeverity,  
                   @ErrorState   
				  );  
	END CATCH
END */

/* ALTER FUNCTION V_VERIFICAR_EXISTENCIA_DE_USUARIO (@EMAIL NVARCHAR(50),@PASSWORD NVARCHAR(50))
RETURNS TABLE
AS
RETURN
SELECT *
FROM Usuarios U
WHERE U.Email = @EMAIL COLLATE Latin1_General_CS_AS
OR U.[Password] = @PASSWORD COLLATE Latin1_General_CS_AS */