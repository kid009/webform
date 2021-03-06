USE [DB_WORKFLOW]
GO
/****** Object:  StoredProcedure [dbo].[GetRequestByCondition]    Script Date: 9/10/2562 11:09:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetRequestByCondition]
@ReqId as varchar(50) = null,
@Subject as varchar(200) = null,
@DateNow varchar(50) = null

AS
BEGIN

	SET NOCOUNT ON;

	SELECT [REQUEST_ID]
      ,[APPROVE_ID]
      ,[SUBJECT]
      ,[DESCRIPTION]
      ,[CREATE_BY]
      ,[CREATE_DATE]
      ,[PATH_FILE]
  FROM [DB_WORKFLOW].[dbo].[REQUEST] 
  where ([REQUEST_ID] like '% '+@ReqId+'%' or ISNULL(@ReqId, '') = '') 
		or ([SUBJECT] like '%'+@Subject+'%' or ISNULL(@Subject, '') = '') 
		or ([CREATE_DATE] = CONVERT(datetime, @DateNow, 103) or ISNULL(@DateNow, '') = '')

END