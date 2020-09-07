﻿USE Avocet_Geothermal_Test2
GO
DECLARE @AT AS DATETIME = '20180101'
DECLARE @SRC AS VARCHAR(32) = 'Avocet_Geothermal'
DECLARE @TGT AS VARCHAR(32) = 'Avocet_Geothermal_Test2'
DECLARE @AT_STR AS VARCHAR(30) = CAST(@AT AS VARCHAR(30))
DECLARE @TBL TABLE 
(
	TABLENAME VARCHAR(50)
	,QUERY NVARCHAR(MAX)
	,DELQUERY NVARCHAR(MAX)
)

INSERT INTO @TBL SELECT 
	TYPE
	,'INSERT ' +  @TGT + '.dbo.' + PROP_TBL_NAME + ' SELECT *  FROM  ' + @SRC + '.dbo.'   + PROP_TBL_NAME  + ' WHERE EVENT_TYPE = ''' + TYPE + ''' AND START_DATETIME > ''' +@AT_STR  + ''''  
	,'DELETE ' +  @TGT + '.dbo.' + PROP_TBL_NAME + ' WHERE EVENT_TYPE = ''' + TYPE + ''' AND START_DATETIME > ''' +@AT_STR  + ''''  
FROM Avocet_Geothermal.dbo.TYPE WHERE TYPE_CLASS = 'TRANSACTION'

INSERT INTO @TBL SELECT 
	'ITEM'
	,'INSERT ' + @TGT + '.dbo.ITEM SELECT *  FROM ' + @SRC + '.dbo.ITEM'
	,'DELETE ' + @TGT + '.dbo.ITEM' 
INSERT INTO @TBL SELECT 
	'ITEM_PROPERTY'
	,'INSERT ' + @TGT + '.dbo.ITEM_PROPERTY SELECT *  FROM  ' + @SRC  + '.dbo.ITEM_PROPERTY'
	,'DELETE ' + @TGT + '.dbo.ITEM_PROPERTY' 

INSERT INTO @TBL SELECT 
	'ITEM_LINK'
	,'INSERT ' + @TGT + '.dbo.ITEM_LINK  SELECT *  FROM ' + @SRC + '.dbo.ITEM_LINK'
	,'DELETE ' + @TGT + '.dbo.ITEM_LINK' 

INSERT INTO @TBL SELECT 
	'ITEM_LINK_PROPERTY'
	,'INSERT ' + @TGT + '.dbo.ITEM_LINK_PROPERTY  SELECT *  FROM  ' + @SRC + '.dbo.ITEM_LINK_PROPERTY'
	,'DELETE ' + @TGT + '.dbo.ITEM_LINK_PROPERTY' 


INSERT INTO @TBL SELECT 
	'ITEM_BLOB'
	,'INSERT ' + @TGT + '.dbo.ITEM_BLOB  SELECT *  FROM ' + @SRC  + '.dbo.ITEM_BLOB'
	,'DELETE ' + @TGT + '.dbo.ITEM_BLOB'

INSERT INTO @TBL SELECT 
	'ITEM_BLOB_STORE'
	,'INSERT '+ @TGT + '.dbo.ITEM_BLOB_STORE SELECT *  FROM ' + @SRC  + '.dbo.ITEM_BLOB_STORE'
	,'DELETE ' + @TGT + '.dbo.ITEM_BLOB_STORE'


DECLARE @TABLENAME  AS VARCHAR(MAX)
DECLARE @QUERY  AS VARCHAR(MAX)
DECLARE @DELQUERY  AS VARCHAR(MAX)
DECLARE CUR CURSOR FOR SELECT TABLENAME, QUERY,DELQUERY FROM @TBL
OPEN CUR
FETCH NEXT FROM CUR INTO @TABLENAME, @QUERY,@DELQUERY
WHILE @@FETCH_STATUS = 0 
BEGIN
	PRINT('------MIGRATING------'	+ @TABLENAME)
	PRINT '\tDELETE QUERY: '  + @DELQUERY
	PRINT '\tINSERT QUERY:'	+ @QUERY
	BEGIN TRY
		EXEC(@DELQUERY)
		EXEC(@QUERY)
	END TRY
	BEGIN CATCH
		PRINT('------ERROR MIGRATING------'	+ @TABLENAME)
		PRINT ('ERROR:' + ERROR_MESSAGE())
		PRINT '\tDELETE QUERY: '  + @DELQUERY
		PRINT '\tINSERT QUERY:'	+ @QUERY
	END CATCH
	PRINT('------DONE------')
	FETCH NEXT FROM CUR INTO @TABLENAME, @QUERY,@DELQUERY
END
CLOSE CUR
DEALLOCATE CUR



 