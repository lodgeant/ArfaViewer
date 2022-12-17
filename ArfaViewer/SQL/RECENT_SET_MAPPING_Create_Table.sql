﻿DROP TABLE RECENT_SET_MAPPING;
CREATE TABLE RECENT_SET_MAPPING
(
ID int,
USER_ID varchar(25),
CREATED_TS  varchar(25),
SET_REF  varchar(25),
SET_DESCRIPTION varchar(100)
);

--CREATE UNIQUE INDEX INDEX_LDRAW_REF ON BASEPART (LDRAW_REF)