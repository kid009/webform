USE [DB_WORKFLOW]
GO
/****** Object:  StoredProcedure [dbo].[GetRequestByCondition]    Script Date: 9/10/2562 11:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRequestAll]
@DateNow varchar(50) = null

AS
BEGIN

	SET NOCOUNT ON;

	SELECT *
  FROM [DB_WORKFLOW].[dbo].[REQUEST] 
  where ([CREATE_DATE] = CONVERT(datetime, @DateNow, 103) or [CREATE_DATE] is null )

END