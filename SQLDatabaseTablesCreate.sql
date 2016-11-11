DROP TABLE USERS;
DROP TABLE GIFT;
DROP TABLE BADGE;

CREATE TABLE USERS (
	USER_ID int not null IDENTITY(1,1),
	FIRST_NAME varchar(100) not null,
	LAST_NAME varchar(100) not null,
	EMAIL varchar(100) not null,
	PHOTO_URL varchar(MAX),
	USER_TYPE varchar(10) not null,
	SHAREABLE_LINK varchar(MAX) not null
);

CREATE TABLE GIFT (
	GIFT_ID int not null IDENTITY(1,1),
	BADGE_ID int not null,
	SENDER_ID int not null,
	RECIPIENT_ID int not null,
	TREE_LOC_X int not null,
	TREE_LOC_Y int not null,
	COMMENT varchar(140)
);

CREATE TABLE BADGE (
	BADGE_ID int not null IDENTITY(1,1),
	USER_TYPE varchar(MAX),
	RETIREMENT_DATE date,
	BADGE_START_DATE date not null,
	NAME varchar(MAX),
	SELF_GIVE bit,
	STUDENT_GIVE bit,
	STAFF_GIVE bit,
	FACULTY_GIVE bit,
	IMAGE_LINK varchar(MAX),
	BADGE_DESC varchar(MAX)
);