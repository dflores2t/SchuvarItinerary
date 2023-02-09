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
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE schuvaritinerary
    IS 'database to managment custemore itinerary';
--create schema
CREATE SCHEMA schu
    AUTHORIZATION schuvardb;
set search_path to schu, public;
show search_path;


