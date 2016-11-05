/* run on database named GSTdata */

DROP TABLE USERS;
CREATE TABLE USERS (
USR_ID int,
FIRST_NAME varchar(32),
LAST_NAME varchar(32),
EMAIL varchar(40),
PHOTO_URL varchar(256),
USR_TYPE varchar(16),
SHARABLE_LINK varchar(256)
);
INSERT INTO USERS VALUES('10010','Weston','Dial','weston.dial@eagles.oc.edu','','student','');
INSERT INTO USERS VALUES('10011','Colby','Wood','colby.wood@eagles.oc.edu','','faculty','');

/* -- */

DROP TABLE BADGE_GIFTS;
CREATE TABLE BADGE_GIFTS (
GIFT_ID int,
GIFT_DATE datetime,
BADGE_ID int,
SENDER_ID int,
RECIPIENT_ID int,
TREE_LOC_X int,
TREE_LOC_Y int,
COMMENT text
);
INSERT INTO BADGE_GIFTS VALUES('100','7/9/2015','1','10011','10010','5','5','uderwhelming');
INSERT INTO BADGE_GIFTS VALUES('101','8/8/2015','2','10010','10011','6','6','GOOD');

/* -- */
DROP TABLE BADGES;
CREATE TABLE BADGES (
BADGE_ID int,
BADGE_TYPE varchar(32),
RETIREMENT_DATE datetime,
BEGIN_DATE datetime,
NAME varchar(32),
SELF_GIVE bit,
STUDENT_GIVE bit,
STAFF_GIVE bit,
FACULTY_GIVE bit,
PICTRUE text
);
INSERT INTO BADGES VALUES('1','Leaf','10/10/2017','1/1/2012','Prays a lot','1','1','1','1','');
INSERT INTO BADGES VALUES('2','Flower','11/11/2017','2/2/2013','Praise a lot','0','0','0','0','');
