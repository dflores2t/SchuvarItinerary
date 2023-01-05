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
create TABLE Customer
(
  IdCustomer INT IDENTITY(1,1),
  FullName VARCHAR(100) not null,
  Contact INT not null
    CONSTRAINT pk_id_customer PRIMARY KEY(IdCustomer),
)
go
create TABLE Aerolinea
(
  IdAerolinea INT IDENTITY(1,1),
  AerolineaName VARCHAR(2) not null,
  AeroDescription VARCHAR(50) not null,
  isDeleted BIT,
  constraint pk_Id_aerolinea PRIMARY KEY(IdAerolinea)
)
GO
create table FlyCustomer
(
  IdFly INT IDENTITY(1,1),
  IdCustomer INT not null,
  IdAerolinea INT not null,
  Route VARCHAR(20) not null,
  Localizer VARCHAR(10) not null,
  Departures DATETIME not null,
  Arrivals DATETIME not null
    CONSTRAINT pk_IdFly_FlyCustomer primary KEY(IdFly),
  constraint fk_IdCustomer_FlyCustomer_Customer FOREIGN KEY(IdCustomer) REFERENCES Customer(IdCustomer),
  CONSTRAINT fk_IdAerolinea_FlyCustomer_Aerolinea FOREIGN KEY(IdAerolinea) REFERENCES Aerolinea(IdAerolinea)

)