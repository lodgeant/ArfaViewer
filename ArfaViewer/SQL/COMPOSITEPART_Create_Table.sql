DROP TABLE COMPOSITEPART;
CREATE TABLE COMPOSITEPART
(
	ID int,
	LDRAW_REF varchar(25),
	LDRAW_DESCRIPTION varchar(250),
	PARENT_LDRAW_REF varchar(25),
	LDRAW_COLOUR_ID int,
	POS_X float,
	POS_Y float,
	POS_Z float,
	ROT_X float,
	ROT_Y float,
	ROT_Z float
);

