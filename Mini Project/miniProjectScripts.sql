
Create database RailwayResDB;
USE RailwayResDB;


-- Table creation for Users
CREATE TABLE dbo.Users (
    UserID int NOT NULL identity(1,1) primary key,
    UserName nvarchar(max) NULL,
    UserPassword nvarchar(max) NULL,
    AdminStatus bit NOT NULL
);


-- Table creation for Days
CREATE TABLE dbo.Days (
    dayID int NOT NULL identity(1,1) primary key,
    dayName nvarchar(max) NULL
);


-- Table creation for SeatClasses
CREATE TABLE dbo.SeatClasses (
    SeatClassID int NOT NULL identity(1,1) primary key,
    SeatClassName nvarchar(max) NULL
);


-- Table creation for Stations
CREATE TABLE dbo.Stations (
    StationID int NOT NULL identity(1,1) primary key,
    StationName nvarchar(max) NULL
);



-- Train Table
CREATE TABLE TrainTable (
    TrainNo INT identity(1,1) PRIMARY KEY,
    TrainName VARCHAR(50),
    DeActiveStatus bit DEFAULT 0,
);



-- TrainAvailableDaysTable
CREATE TABLE TrainAvailableDaysTable (
    TrainNo INT,
    DayID INT,
    DeActiveStatus bit DEFAULT 0
);



-- TrainAvailablePerDayTable -- trips
CREATE TABLE TrainAvailablePerDayTable (
    TrainNo INT,
    DayID INT,
	FromStationID INT,
	ToStationID INT,
	TrainStartTime time,
	TrainEndTime time,
	TrainJourneyBasePrice int,
	DeActiveStatus bit DEFAULT 0,
);

-- TrainRouteTable
CREATE TABLE TrainRouteTable (
    TrainNo INT,
	FromStationID INT,
	ToStationID INT,
    StationSeq INT,
	CurrentStation INT,
    ArrivesAt TIME,
    DepartureAt TIME,
	DeActiveStatus bit DEFAULT 0,
);

-- ClassesAvailableInTrain
CREATE TABLE ClassesAvailableInTrain (
    TrainNo INT,
    ClassID INT,
    SeatPrice DECIMAL(10, 2),
	DeActiveStatus bit DEFAULT 0
);

-- SeatsAvailabilityPerClass
CREATE TABLE SeatsAvailabilityPerClass (
    TrainNo INT,
    ClassID INT,
    LowerBerthAvailableSeats INT,
    MiddleBerthAvailableSeats INT,
    UpperBerthAvailableSeats INT,
	DeActiveStatus bit DEFAULT 0
);

CREATE TABLE TransactionTable (
    TransactionID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT,
	BookedCustNo INT,
    TransactionDate DATE,
    RefundAmount DECIMAL(10, 2),
    IRCTCFee DECIMAL(10, 2),
    Remarks NVARCHAR(255)
);
 
 
CREATE TABLE TrainBookingTable1 (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
	BookedCustNo INT ,
    TrainNo INT,    
    BookingDate DATE Default getDate(),
    FromStationID INT,
    ToStationID INT,
    TotalPrice DECIMAL(10, 2),
    CancellationStatus BIT DEFAULT 0
);

CREATE TABLE TrainBookingTable2 (
	BookingID INT,
	ClassID INT,
    UserID INT,
	berth varchar(10),
	SeatPrice Float,
	PassengerName NVARCHAR(MAX),
	CancellationStatus BIT DEFAULT 0,
	FOREIGN KEY (BookingID) REFERENCES TrainBookingTable1(BookingID)
)

create table CustBank(
	UserID int,
	CustName varchar(25),
	AccNo BIGINT,
	AvailBalance float,
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
)


create or alter procedure sp_Update_TrainStatus (@trainNo int,@deActiveStatus bit)
as 
begin
		update TrainTable set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
		update TrainAvailableDaysTable set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
		update TrainAvailablePerDayTable set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
		update TrainRouteTable set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
		update ClassesAvailableInTrain set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
		update SeatsAvailabilityPerClass set DeActiveStatus=@deActiveStatus where TrainNo=@trainNo;
end

CREATE OR ALTER FUNCTION fn_ShowTrainsByFromToStation 
(
    @FromStation INT, 
    @ToStation INT
)
RETURNS @ShowTrainsByFromToStation TABLE (
    TrainNo INT, 
    TrainName VARCHAR(50),
    FromStationID INT,
    FromStationName NVARCHAR(MAX),
    ToStationID INT,
    ToStationName NVARCHAR(MAX)
)
AS
BEGIN

    WITH SourceTrains AS (
        SELECT TrainNo, FromStationID, ToStationID, CurrentStation
        FROM TrainRouteTable
        WHERE CurrentStation = @FromStation and DeActiveStatus!=1
    ),
    DestinationTrains AS (
        SELECT TrainNo, FromStationID, ToStationID, CurrentStation
        FROM TrainRouteTable
        WHERE CurrentStation = @ToStation and DeActiveStatus!=1
    )

    INSERT INTO @ShowTrainsByFromToStation
    SELECT DISTINCT 
        st.TrainNo, 
        tr.TrainName, 
        st.FromStationID, 
        fs.StationName AS FromStationName, 
        st.ToStationID, 
        ts.StationName AS ToStationName
    FROM SourceTrains st
    JOIN DestinationTrains dt
        ON st.TrainNo = dt.TrainNo
    JOIN TrainTable tr
        ON st.TrainNo = tr.TrainNo
    JOIN Stations fs
        ON st.FromStationID = fs.StationID
    JOIN Stations ts
        ON st.ToStationID = ts.StationID
    WHERE 
        st.FromStationID = dt.FromStationID 
        AND st.ToStationID = dt.ToStationID 
        AND (st.CurrentStation = @FromStation OR dt.CurrentStation = @ToStation)
    RETURN;
END;


--select * from fn_ShowTrainsByFromToStation(4,2);
--SELECT * FROM TrainRouteTable


CREATE OR ALTER FUNCTION fn_ShowAvailDays 
(	@TrainNo INT,
    @FromStation INT, 
    @ToStation INT
)
RETURNS @ShowTrainAvailDays TABLE (
    TrainNo INT, 
    TrainName VARCHAR(50),
    DayID INT,
    DayName NVARCHAR(MAX),
	JourneyBasePrice float
)
AS
BEGIN
    
    INSERT INTO @ShowTrainAvailDays
    SELECT DISTINCT 
        tapt.TrainNo, 
        tr.TrainName, 
        dt.DayID, 
        dt.DayName AS Day,
		tapt.TrainJourneyBasePrice
    FROM TrainAvailablePerDayTable tapt
    JOIN Days dt
        ON tapt.DayID = dt.DayID
    JOIN TrainTable tr
        ON tapt.TrainNo = tr.TrainNo
    WHERE 
        tapt.FromStationID = @FromStation 
        AND tapt.ToStationID = @ToStation
        AND tapt.TrainNo=@TrainNo
		AND tapt.DeactiveStatus!=-1
    RETURN;
END;

--select * from fn_ShowAvailDays(1,2,3);
--select * from fn_ShowTrainsByFromToStation(4,9);

 --admin
INSERT INTO dbo.Users (UserName,UserPassword,AdminStatus)VALUES
('admin','admin123',1)

insert into CustBank values(2,'bhuvan',1234567,10000);







