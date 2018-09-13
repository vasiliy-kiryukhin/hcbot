create table enrollment (
	userid text,
	enroll_date timestamp,
	trainingType integer,
	location text,
	displayName text,
	positive boolean,
	rectimestamp timestamp
);

create table person (
	chatid bigint,
	firstname text,
	lastname text,
	birthday date,
	unique(chatid)	
)