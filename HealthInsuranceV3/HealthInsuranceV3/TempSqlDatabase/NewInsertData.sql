-- Insert test data into Department
INSERT INTO Department (DepartmentName, DBStatus) VALUES
('Office of Director', 1),
('Human Resources', 1),
('Finance', 1),
('Marketing', 1),
('Operations', 1),
('Customer Support', 1);

-- Insert test data into RejectionReason
INSERT INTO RejectionReason (ReasonText, DBStatus) VALUES
('Too Expensive', 1),
('I dont like you', 1),
('Kiss my ass', 1),
('Use your own money', 1),
('Other', 1);

-- Insert test data into InsuranceCompany
INSERT INTO InsuranceCompany (CompanyName, ContactInfo, Address, EstablishedYear, DBStatus) VALUES
('MamaBoy Inc.', 'info@MamaBoy.com', '123 Health St, City, Country', '2019-01-01', 1),
('SafeGuard Insurance', 'info@safeguard.com', '456 Safety Ave, City, Country', '2009-01-01', 1),
('Bully Company.', 'info@BullyCo.com', '555 Life St, City, Country', '1992-01-01', 1);

-- Insert test data into Insurance
INSERT INTO Insurance (InsuranceName, IconText, CompanyId, Description, DBStatus) VALUES
('Health Insurance A', 'IconText1', 1, 'Premium health coverage', 1),
('Dental Insurance', 'IconText2', 1, 'Watch your mouth next time', 1),
('Health Insurance B', 'IconText3', 2, 'Good luck to you', 1),
('Accident Insurance', 'IconText4', 2, 'Poor you', 1),
('Life Insurance', 'IconText5', 3, 'Dont die too soon', 1),
('School Insurance', 'IconText6', 3, 'No more homeworks', 1);

-- Insert test data into InsurancePackage
INSERT INTO InsurancePackage (InsuranceId, PackageName, CoverageDetails, PolicyTermInDay, Price, DBStatus) VALUES
(2, 'Premium Health Package', 'Full health coverage with additional benefits', 365, 500.00, 1),
(3, 'Dental Care Plus', 'Buy 1 golden tooth get 2 silver teeth', 180, 100.00, 1),
(4, 'Basic Health Package', 'The very basic health coverage for essential needs', 365, 250.00, 1),
(5, 'Accident Shield', 'Coverage for accidental injuries', 365, 150.00, 1),
(6, 'Gangster for life', 'Coverage for life events and your homies', 730, 750.00, 1),
(7, 'Homework 24 Seven', 'Coverage for lazy student and failed the exam', 180, 75.00, 1);


