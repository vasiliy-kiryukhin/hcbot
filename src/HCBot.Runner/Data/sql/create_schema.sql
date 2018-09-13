create table enrollment (
	chatid bigint,
	enroll_date date,
	trainingType smallint,
	location text,
	displayName text,
	positive boolean,
	unique(chatid, enroll_date, location)
);

create table person (
	chatid bigint,
	firstname text,
	lastname text,
	birthday date,
	unique(chatid)	
)