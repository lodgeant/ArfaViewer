﻿DROP TABLE TICKBACK;
CREATE TABLE TICKBACK
(
ID int,
NAME varchar(50),
DATA xml
);

CREATE UNIQUE INDEX INDEX_NAME ON TICKBACK (NAME)