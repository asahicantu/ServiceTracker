﻿TRUNCATE TABLE TABLESETTINGS
INSERT INTO TableSettings (TableName,ColumnName) 
SELECT DISTINCT T.TABLE_NAME, C.COLUMN_NAME FROM 
	INFORMATION_SCHEMA.TABLES T
	INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON
	C.TABLE_NAME = C.TABLE_NAME
WHERE T.TABLE_CATALOG = 'ST'


SELECT * FROM TableSettings 