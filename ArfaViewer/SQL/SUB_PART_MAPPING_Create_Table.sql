﻿DROP TABLE SUB_PART_MAPPING;
CREATE TABLE SUB_PART_MAPPING
(	
	ID int,
	PARENT_LDRAW_REF varchar(25),
	SUB_PART_LDRAW_REF varchar(25),
	LDRAW_COLOUR_ID int,
	POS_X float,
	POS_Y float,
	POS_Z float,
	ROT_X float,
	ROT_Y float,
	ROT_Z float
);

