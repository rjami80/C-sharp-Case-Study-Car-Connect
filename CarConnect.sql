create database CarConnect;
use CarConnect;
-- Customer Table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    PhoneNumber VARCHAR(20),
    Address VARCHAR(255),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255), -- Assuming hashed passwords are 255 characters long
    RegistrationDate DATE
);

-- Vehicle Table
CREATE TABLE Vehicle (
    VehicleID INT PRIMARY KEY IDENTITY(1,1),
    Model VARCHAR(50),
    Make VARCHAR(50),
    Year INT,
    Color VARCHAR(50),
    RegistrationNumber VARCHAR(20) UNIQUE,
    Availability BIT,
    DailyRate DECIMAL(10, 2) -- Assuming a decimal for daily rate with 2 decimal places
);

-- Reservation Table
CREATE TABLE Reservation (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    VehicleID INT,
    StartDate DATETIME,
    EndDate DATETIME,
    TotalCost DECIMAL(10, 2), -- Assuming a decimal for total cost with 2 decimal places
    Status VARCHAR(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)on delete set null,
    FOREIGN KEY (VehicleID) REFERENCES Vehicle(VehicleID)on delete set null
);

-- Admin Table
CREATE TABLE Admin  (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    PhoneNumber VARCHAR(20),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255), -- Assuming hashed passwords are 255 characters long
    Role VARCHAR(50),
    JoinDate DATE
);


-- Inserting sample data into the Customer table
INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate)
VALUES
    ('Kiran', 'Kumar', 'kiran_kumar@gmail.com', '286-3192', '123 Main St, Cityville', 'kiran_kumar', 'kiran@123', '2023-01-01'),
    ('Uday', 'kiran', 'uday_kiran@gmail.com', '200-1564', '456 Oak St, Townsville', 'uday_kiran', 'uday@123', '2023-02-15'),
    ('Murali', 'Mukesh', 'murali_mukesh@gmail.com', '555-9876', '789 Pine St, Villageton', 'murali_mukesh', 'mukesh@123', '2023-03-20'),
    ('Bhaskar', 'Ram', 'bhaskar_ram@gmail.com', '555-4321', '101 Cedar St, Hamletville', 'bhaskar_ram', 'bhaskar@123', '2023-04-10'),
    ('Vasanth', 'Adithya', 'vasanth_adithya', '555-8765', '202 Elm St, Riverside', 'vasanth_adithya', 'vasanth@123', '2023-05-05');

-- Inserting sample data into the Vehicle table
INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES
    ('CyberTruck', 'Tesla', 2019, 'Silver', 'LEO007', 1, 500.00),
    ('Thar', 'Mahindra', 2017, 'Red', 'ABC123', 0, 550.00),
    ('Challenger', 'Ford', 2023, 'Matt Black', 'BH0078', 1, 700.00),
    ('Vista', 'Honda', 2022, 'Silver', 'GHI789', 1, 60.00),
    ('Benz', 'Mercedes', 2021, 'Grey', 'JKL012', 0, 555.00);

-- Inserting sample data into the Reservation table
INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES
    (1, 3, '2023-01-01 10:00:00', '2023-02-06 15:00:00', 2800.00, 'Confirmed'),
    (2, 1, '2023-02-10 08:30:00', '2023-03-24 12:00:00', 2750.00, 'Pending'),
    (3, 5, '2023-03-20 14:00:00', '2023-04-27 18:30:00', 2705.00, 'Completed'),
    (4, 2, '2023-04-05 09:00:00', '2023-05-04 17:45:00', 3030.00, 'Confirmed'),
    (5, 4, '2023-06-15 11:30:00', '2023-06-20 16:15:00', 3050.00, 'Pending');

-- Inserting sample data into the Admin table
INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
VALUES
    ('MacBook', 'Pro', 'anish_raj@gmail.com', '444-1234', 'anish_1', 'anish_password_1', 'Super Admin', '2023-02-05'),
    ('Windows', 'Ultra', 'aatish@gmail.com', '123-7896', 'aatish_2', 'aatish_password_2', 'Fleet Manager', '2023-01-18'),
    ('Linux', 'Three', 'kaushik@gmail.com', '852-7894', 'kaushik_3', 'kaushik_password_3', 'Super Admin', '2023-11-27'),
    ('Microsoft', 'Surface', 'Shrikumar@gmail.com', '147-8956', 'Shri_4', 'Shri_password_4', 'Fleet Manager', '2023-01-17'),
    ('Chrome', 'Book', 'rithvik@gmail.com', '203-1563', 'rithvik_5', 'rithvik_password_5', 'Super Admin', '2023-12-16');

	select * from Customer;
		select * from Admin;
			select * from Reservation;
				select * from Vehicle;
