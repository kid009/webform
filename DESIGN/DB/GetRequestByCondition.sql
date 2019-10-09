USE [DB_WORKFLOW]
GO
CREATE PROCEDURE [GetRequestByCondition]
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
      ,[STATUS]
      ,[NOTE]
  FROM [DB_WORKFLOW].[dbo].[REQUEST] 
  where ([REQUEST_ID] = @ReqId or [REQUEST_ID] is null) 
		and ([SUBJECT] = @Subject or [SUBJECT] is null) 
		and ([CREATE_DATE] = CONVERT(datetime, @DateNow, 103) or [CREATE_DATE] is null )

END