DROP TABLE GLOBAL_MAP_DATA;
CREATE TABLE GLOBAL_MAP_DATA
(	
	REF varchar(25),
	DATA xml	
)

CREATE UNIQUE INDEX INDEX_REF ON GLOBAL_MAP_DATA (REF)