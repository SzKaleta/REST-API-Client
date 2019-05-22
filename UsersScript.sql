create table Users
(
user_id INT  NOT NULL  IDENTITY PRIMARY KEY,
name VARCHAR(30) NOT NULL,
age INT
);


insert into Users(name, age) values ('Jan', 34);
insert into Users(name, age) values ('Krzysztof', 12);
insert into Users(name, age) values ('Magda', 22);