USE master
go
if exists(select name
from master.sys.databases
WHERE name ='SchuvarItinerary')
Drop DATABASE SchuvarItinerary
go
Create DATABASE SchuvarItinerary
go
use SchuvarItinerary
go
create TABLE customer
(
  customer_id INT IDENTITY(1,1),
  customer_fullname VARCHAR(100) not null,
  customer_phone INT not null,
  customer_isdeleted bit,
  customer_dateup datetime,
  customer_datemodify datetime
    CONSTRAINT pk_id_customer PRIMARY KEY(customer_id),
)
--create index on customer_phone column
create index idx_cusmoer_phone on customer(customer_phone)
go

create TABLE aerolinea
(
  aerolinea_id INT IDENTITY(1,1),
  aerolinea_shortname VARCHAR(2) not null,
  aerolinea_fullname VARCHAR(50) not null,
  aerolinea_isdeleted BIT,
  aerolinea_dateup datetime,
  aeroliena_datemodify datetime,
  constraint pk_Id_aerolinea PRIMARY KEY(IdAerolinea)
)
GO
create table flycustomer
(
  flycustomer_id INT IDENTITY(1,1),
  flycustomer_idcustomer INT not null,
  flycustomer_idaerolinea INT not null,
  flycustomer_route VARCHAR(20) not null,
  flycustomer_localizer VARCHAR(10) not null,
  flycustomer_departures DATE not null,
  flycustomer_arrivals DATE not null,
  flycustomer_dateup datetime,
  flycustomer_datemodify datetime
    CONSTRAINT pk_IdFly_FlyCustomer primary KEY(flycustomer_id),
  constraint fk_IdCustomer_FlyCustomer_Customer FOREIGN KEY(flycustomer_idcustomer) REFERENCES customer(customer_id),
  CONSTRAINT fk_IdAerolinea_FlyCustomer_Aerolinea FOREIGN KEY(flycustomer_idaerolinea) REFERENCES aerolinea(aerolinea_id)

)
