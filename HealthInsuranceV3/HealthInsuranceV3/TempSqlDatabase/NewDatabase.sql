-- Bảng Department
CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(255) NOT NULL,
    DBStatus INT DEFAULT 1
);

-- Bảng DepartmentManager
CREATE TABLE DepartmentManager (
    EmployeeId NVARCHAR(450) PRIMARY KEY NOT NULL,
    ManagerId NVARCHAR(450) REFERENCES AspNetUsers (Id),
    DepartmentId INT REFERENCES Department (DepartmentId),
    IsManager BIT NOT NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    DBStatus BIT DEFAULT 1
);
-- Bảng RejectionReason
CREATE TABLE RejectionReason (
    ReasonId INT PRIMARY KEY IDENTITY(1,1),
    ReasonText NVARCHAR(MAX) NOT NULL,
    DBStatus BIT DEFAULT 1
);

-- Bảng Insurance
CREATE TABLE Insurance (
    InsuranceId INT PRIMARY KEY IDENTITY(1,1),
    InsuranceName NVARCHAR(255) NOT NULL,
    IconText NVARCHAR(255),
    CompanyId INT,
    Description NVARCHAR(MAX),
    DBStatus BIT DEFAULT 1
);

ALTER TABLE Insurance
ADD CONSTRAINT FK_Insurance_Company
FOREIGN KEY (CompanyID)
REFERENCES InsuranceCompany(CompanyID);

-- Bảng InsurancePackage
CREATE TABLE InsurancePackage (
    PackageId INT PRIMARY KEY IDENTITY(1,1),
    InsuranceId INT FOREIGN KEY REFERENCES Insurance(InsuranceId),
    PackageName NVARCHAR(255) NOT NULL,
    CoverageDetails NVARCHAR(MAX),
    PolicyTermInDay INT,
    Price DECIMAL(18, 2),
    DBStatus BIT DEFAULT 1
);

-- Bảng InsuranceRegistration
CREATE TABLE InsuranceRegistration (
    RegistrationId INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers (Id),
    InsuranceId INT FOREIGN KEY REFERENCES Insurance(InsuranceId),
    PackageId INT,
    PackageName NVARCHAR(255),
    RegistrationStatus NVARCHAR(50),
    RegistrationDate DATE,
    ApprovalDate DATE,
    RejectionReasonId INT FOREIGN KEY REFERENCES RejectionReason(ReasonId),
    DBStatus BIT DEFAULT 1
);

-- Bảng InsuranceCompany
CREATE TABLE InsuranceCompany (
    CompanyId INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(255) NOT NULL,
    ContactInfo NVARCHAR(MAX),
    Address NVARCHAR(MAX),
    EstablishedYear DATE,
    DBStatus BIT DEFAULT 1
);
-- Bảng PaymentStatus
CREATE TABLE PaymentStatus (
    PaymentStatusId INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    DBStatus BIT DEFAULT 1
);
-- Bảng Payment
CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    RegistrationId INT FOREIGN KEY REFERENCES InsuranceRegistration(RegistrationId),
    PaymentAmount DECIMAL(18, 2),
    PaymentDate DATE,
    PaymentStatusId INT FOREIGN KEY REFERENCES PaymentStatus(PaymentStatusId),
    DBStatus BIT DEFAULT 1
);
