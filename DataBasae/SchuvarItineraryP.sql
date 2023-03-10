--Postgresql v.15, DataBase SchuvarItinerary.
--create role schuvar to admin bd
DROP ROLE IF EXISTS schuvardb;
CREATE ROLE schuvardb WITH
	LOGIN
	NOSUPERUSER
	CREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD 'xxxxxx';
--create tablespace	
CREATE TABLESPACE sc_primary
  OWNER schuvardb
  LOCATION '/home/dail/PosDataBases';

ALTER TABLESPACE sc_primary
  OWNER TO dail;
COMMENT ON ROLE schuvardb IS 'this user is to managment db';
--create database
DROP DATABASE IF EXISTS schuvaritinerary;
CREATE DATABASE schuvaritinerary
    WITH
    OWNER = schuvardb
    ENCODING = 'UTF8'
    TABLESPACE = sc_primary
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE schuvaritinerary
    IS 'databaso to management customers itinerary';
--create schema
CREATE SCHEMA schu
    AUTHORIZATION schuvardb;
set search_path to schu, public;
show search_path;

--TABLES CREATION SECTION
--create table customer
--create table customer
drop table if exists schu.customer;
CREATE TABLE schu.customer
(
    customer_id integer GENERATED ALWAYS AS IDENTITY,
    customer_fullname character varying(100) NOT NULL,
    customer_phone character varying(9) NOT NULL,
    customer_isdeleted boolean default false,
    customer_dateup timestamp default now(),
    customer_datemodify timestamp default now(),
    PRIMARY KEY (customer_id),
    CONSTRAINT unq_phone UNIQUE (customer_phone),
    CONSTRAINT chk_pone CHECK (customer_phone ~ '^[0-9]{4}-[0-9]{4}')
);
--index on customer_phone
create index idx_customer_phone
	on schu.customer(customer_phone);
--table owner -
ALTER TABLE IF EXISTS schu.customer
    OWNER to schuvardb;
--create table aerolinea.
drop table if exists schu.aerolinea;
create table schu.aerolinea(
	aerolinea_id integer generated always as identity,
	aerolinea_shortname varchar(2) not null,
	aerolinea_fullname varchar(50) not null,
	aerolinea_isDeleted boolean default  false,
	aerolinea_dateup timestamp default now(),
	aerolinea_datemodify timestamp default now(),
	primary key(aerolinea_id)
);
--table owner
alter table if exists schu.aerolinea
	OWNER to schuvardb;
--table flycustomer
drop table if exists schu.flycustomer;
create table schu.flycustomer(
	flycustomer_id integer generated always as identity,
	flycustomer_idcustomer int not null,
	flycustomer_idaerolinea int not null,
	flycustomer_route varchar(20) not null,
	flycustomer_localyzer varchar(10) not null,
	flycustomer_departure date default current_date,
	flycustomer_arrivals date  default current_date,
	flycustomer_filled boolean default false,
	flycustomer_isdeleted boolean default false,
	flycustomer_dateup timestamp default now(),
	flycustomer_datemodify timestamp default now(),
	primary key(flycustomer_id),
	foreign key(flycustomer_idcustomer)
		references schu.customer(customer_id)
		on update cascade on delete cascade,
	foreign key(flycustomer_idaerolinea)
		references schu.aerolinea(aerolinea_id)
		on update cascade on delete cascade	
);
--set table owner
alter table if exists schu.flycustomer
	owner to schuvardb;
