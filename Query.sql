CREATE TABLE [dbo].[Vehicle] (
    [ID]        INT            IDENTITY(1,1) NOT NULL,
    [make]      NVARCHAR (50)  NOT NULL,
    [model]     NVARCHAR (50) NOT NULL,
    [year]      NCHAR (4)      NOT NULL,
    [condition] NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
CREATE TABLE [dbo].[Inventory] (
    [ID]           INT   IDENTITY(1,1)NOT NULL,
    [vehicleID]    INT   NOT NULL,
    [numberOnHand] INT   NOT NULL,
    [price]        MONEY NOT NULL,
    [cost]         MONEY NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([vehicleID]) REFERENCES [dbo].[Vehicle] ([ID])
);
CREATE TABLE [dbo].[Repair] (
    [Id]           INT           IDENTITY(1,1) NOT NULL,
    [inventoryID]  INT           NOT NULL,
    [whatToRepair] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([inventoryID]) REFERENCES [dbo].[Inventory] ([ID])
);

INSERT Vehicle(make,model,year,condition) VALUES('MERCEDES', 'CLA', '2020','USED')
INSERT Vehicle(make,model,year,condition) VALUES('LAND ROVER', 'SPORT', '2018','USED')
INSERT Vehicle(make,model,year,condition) VALUES('JAGUAR', 'COUP', '2020','NEW')
INSERT Vehicle(make,model,year,condition) VALUES('ACURA', 'ILX', '2019','USED')
INSERT Vehicle(make,model,year,condition) VALUES('BMW', 'M760', '2021','NEW')

INSERT Inventory(vehicleID,numberOnHand,price,cost) VALUES(1, 2, 3500, 2570)
INSERT Inventory(vehicleID,numberOnHand,price,cost) VALUES(2, 3, 500, 150)
INSERT Inventory(vehicleID,numberOnHand,price,cost) VALUES(3, 4, 3000, 500)
INSERT Inventory(vehicleID,numberOnHand,price,cost) VALUES(4, 20, 355, 100)
INSERT Inventory(vehicleID,numberOnHand,price,cost) VALUES(5, 2, 2000, 3000)

INSERT Repair(inventoryID,whatToRepair) VALUES(1,'BRAKES')
INSERT Repair(inventoryID,whatToRepair) VALUES(2,'ENGINE')
INSERT Repair(inventoryID,whatToRepair) VALUES(3,'BRAKES')
INSERT Repair(inventoryID,whatToRepair) VALUES(4,'BRAKES')
INSERT Repair(inventoryID,whatToRepair) VALUES(5,'BUMPER')