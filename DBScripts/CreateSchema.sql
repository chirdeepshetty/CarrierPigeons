
drop table if exists Request;
drop table if exists User;
create table Request (
  Id  integer,
   OriginPlace TEXT,
   OriginDate DATETIME,
   DestinationPlace TEXT,
   DestinationDate DATETIME,
   Description TEXT,
   Dimensions TEXT,
   Weight TEXT,
   user_id integer,
   accepting_user_id integer,
   primary key (Id)
);
create table User (
  Id  integer,
   FirstName TEXT,
   LastName TEXT,
   Email TEXT,
   Password TEXT,
   primary key (Id)
);
create table Journey (
  Id  integer,
   OriginPlace TEXT,
   OriginDate DATETIME,
   DestinationPlace TEXT,
   DestinationDate DATETIME,
   user_id integer,

   primary key (Id)
);
create table Match (
  Id  integer,
  Status TEXT,
   request_id integer,
   journey_id integer,
   primary key (Id)
); 

create table User_Group 
(
	id integer,
	name text
);

alter table User
add column group_id integer;

insert into User_Group(id,name) values(1,'Pune');
insert into User_Group(id,name) values(2,'Bangalore');
insert into User_Group(id,name) values(3,'Chennai');
insert into User_Group(id,name) values(4,'Global');

.backup ../bin/Debug/Database.dat
