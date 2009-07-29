
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

.backup ../bin/Debug/Database.dat
