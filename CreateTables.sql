/* run on database named GSTdata */

/* create user table */

DROP TABLE USERS;
CREATE TABLE USERS (
user_id int identity,
first_name varchar(32),
last_name varchar(32),
email varchar(40),
photo_url varchar(256),
user_type int,
shareable_link varchar(256)
);

/* create gifts table */

DROP TABLE BADGE_GIFTS;
CREATE TABLE BADGE_GIFTS (
gift_id int identity,
gift_date datetime,
badge_id int,
sender_id int,
recipient_id int,
tree_loc_x int,
tree_loc_y int,
comment text
);

/* create badges table */

DROP TABLE BADGES;
CREATE TABLE BADGES (
badge_id int identity,
descript varchar(max),
badge_type int,
retirement_date datetime,
begin_date datetime,
name varchar(32),
self_give bit,
student_give bit,
staff_give bit,
faculty_give bit,
picture text
);

/* create default badge table */

DROP TABLE DEFAULT_BADGES;
CREATE TABLE DEFAULT_BADGES (
badge_id int,
badge_type int,
tree_loc_x int,
tree_loc_y int
);

/* create prerequisite table */

DROP table PREREQUISITE;
CREATE TABLE PREREQUISITE (
prerequisite_id int identity,
parent_id int,
child_id int
);
