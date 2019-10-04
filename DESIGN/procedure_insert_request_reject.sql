-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
USE [DB_WORKFLOW]
GO

CREATE PROCEDURE [DBO].[INSERT_REQUEST_REJECT]
	@REQUEST_ID VARCHAR(50) = NULL,
	@DATENOW VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT OFF;

	INSERT INTO REQUEST_REJECT (REQUEST_ID, CREATE_DATE) VALUES (@REQUEST_ID, CONVERT(DATETIME, @DATENOW, 103))
END

GO	