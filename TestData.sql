/* run on database named GSTdata */

/* create user table */

DROP TABLE USERS;
CREATE TABLE USERS (
user_id int identity,
first_name varchar(32),
last_name varchar(32),
email varchar(40),
photo_url varchar(256),
user_type varchar(16),
sharable_link varchar(256)
);
INSERT INTO USERS
VALUES('Weston','Dial','weston.dial@eagles.oc.edu','','student','');
INSERT INTO USERS
VALUES('Colby','Wood','colby.wood@eagles.oc.edu','','faculty','');

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
VALUES('7-9-2015','1','10011','10010','5','5','uderwhelming');
INSERT INTO BADGE_GIFTS
VALUES('8-8-2015','2','10010','10011','6','6','GOOD');

/* create badges table */

DROP TABLE BADGES;
CREATE TABLE BADGES (
badge_id int identity,
descript varchar(32),
badge_type varchar(32),
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
VALUES('did some praying','Leaf','10/10/2017','1/1/2012','Prays a lot','1','1','1','1','');
INSERT INTO BADGES 
VALUES('did some praising','Flower','11/11/2017','2/2/2013','Praise a lot','0','0','0','0','');
insert into BADGES
VALUES('knows how to praise','apple','11/11/2022','1/1/2000','Certified praise expert','0','0','0','0','');

/* create prerequisite table */
DROP table PREREQUISITE
CREATE TABLE PREREQUISITE (
parent_id int,
child_id int
);
insert into PREREQUISITE
values (3,2);
