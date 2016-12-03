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
sharable_link varchar(256)
);
INSERT INTO USERS
VALUES('Weston','Dial','weston.dial@eagles.oc.edu','',3,'');
INSERT INTO USERS
VALUES('Colby','Wood','colby.wood@eagles.oc.edu','',2,'');
>>>>>>> feature/db

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
INSERT INTO BADGE_GIFTS
VALUES('7-9-2015','1','2','1','5','5','uderwhelming');
INSERT INTO BADGE_GIFTS
VALUES('8-8-2015','2','1','2','6','6','GOOD');

/* create badges table */

DROP TABLE BADGES;
CREATE TABLE BADGES (
badge_id int identity,
descript varchar(32),
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
INSERT INTO BADGES 

VALUES('did some praying','2','10/10/2017','1/1/2012','Prays a lot','1','1','1','1',null);
INSERT INTO BADGES 
VALUES('did some praising','1','11/11/2017','2/2/2013','Praise a lot','1','2','0','0',null);
insert into BADGES
VALUES('knows how to praise','0','11/11/2022','1/1/2000','Certified praise expert','0','0','1','0',null);

/* create prerequisite table */

DROP table PREREQUISITE;

CREATE TABLE PREREQUISITE (
prerequisite_id int identity,
parent_id int,
child_id int
);
insert into PREREQUISITE
values (3,2);
