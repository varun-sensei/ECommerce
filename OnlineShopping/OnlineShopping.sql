use onlineshopdb
CREATE TABLE products(
	[pid] [varchar](10) primary key,
	[pname] [varchar](20) NULL,
	[description] [varchar](50) NULL,
	[price] [int] NULL,
	[pimage] [varchar](50) NULL)



insert into products values(
'p001'	,'TV',	'Samsung co',	10000,	'images/tv.jpg'),
('p002','Mobile' ,'Samsung',10000,'images/Mobile.jpg'),
('p003'	,'AC',	'BlueStar', 8000,'images/AC.jpg'),
('p004'	,'WashingMachine','Videocon'	,12000,	'images/WashingMachine.jpg'),
('p005'	,'Fan',	'Bajaj',4000,'images/Fan.jpg'),
('p006'	,'Monitor','Dell',6000 ,'images/Monitor.jpg'),
('p007'	,'projector','Epson', 15000,'images/projector.jpg'),
('p008'	,'watch','Titan',7500,	'images/watch.jpg'),
('p009'	,'Fridge','videocon',	900,'images/Fridge.jpg'),
('p0010','car',	'Tata',	200000,	'images/car.jpg'),
('p0011','Invertor','Epson',19000,'images/Invertor.jpg'),
('p0012','Mouse','Dell',2000,'images/Mouse.jpg');




create table Register
(
uname varchar(20) primary key,
password varchar(20),
gender bit,
DOB datetime,
designation varchar(20),
email varchar(50),
country varchar(20),
)
select * from Register
insert into Register values
('Luffy','yoyo-123',1,'05-05-2003','Pirate King','Luffy@email.com','Japan');

CREATE TABLE userorders
(
	[tranid] [int] primary key identity(1,1),
	[username] [varchar](20) NULL,
	[pid] [varchar](10) NULL,
	[transdate] [date] NULL,
	[qty] [int] NULL
	)
	select * from userorders


create table feedbacktbl
(
id int primary key identity(1,1),
username varchar(20),
pid [varchar](10) NULL,
ratings int,
usermessage varchar(100),
fstatus bit
)
select * from feedbacktbl

