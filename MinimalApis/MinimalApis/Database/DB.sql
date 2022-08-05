create database minimal_api;
use minimal_api;

create table user (
    id int not null auto_increment,
    firstname varchar(50),
    lastname varchar(100),
    phone varchar(15),
    email varchar(50),
    primary key(id)
);

insert into user(firstname, lastname, phone, email) 
values("Cesar", "Villalobos Olmos", "3461005286", "cesar-09@hotmail.com");

select * from user;