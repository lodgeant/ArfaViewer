DROP TABLE SET_DETAILS;
CREATE TABLE SET_DETAILS
(
	ID int,
	REF varchar(25),
	DESCRIPTION varchar(250),
	TYPE varchar(15),
	THEME varchar(50),
	SUB_THEME varchar(50),
	YEAR int,
	PART_COUNT int,
	SUBSET_COUNT int,
	MODEL_COUNT int,
	MINIFIG_COUNT int,
	STATUS varchar(15),
	ASSIGNED_TO varchar(25),
	INSTRUCTIONS xml
)

