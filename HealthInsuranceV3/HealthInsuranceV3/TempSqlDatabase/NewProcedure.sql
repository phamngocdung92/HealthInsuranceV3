-----VIEW-------
--Get Insurance List
	CREATE PROCEDURE GetInsuranceList
	AS 
	BEGIN 
		SELECT 
			i.InsuranceName, i.Description, i.IconText, i.InsuranceId, ic.CompanyName, ic.ContactInfo, ic.Address, ic.EstablishedYear 
		FROM
			HealthInsurance.dbo.Insurance i 
		INNER JOIN 
			HealthInsurance.dbo.InsuranceCompany ic on i.CompanyId = ic.CompanyId
	END;
	
--End of Get Insurance List

--Get Insurance Detail
	CREATE PROCEDURE GetInsuranceDetail
	@InsuranceId INT
	AS 
	BEGIN 
		SELECT 
			i.IconText, ip.PackageId, ip.PackageName, ip.CoverageDetails, ip.Price, ip.PolicyTermInDay, ic.CompanyName, ic.EstablishedYear, ic.Address, ic.ContactInfo
		FROM
			HealthInsurance.dbo.Insurance i 
		INNER JOIN 
			HealthInsurance.dbo.InsuranceCompany ic on i.CompanyId = ic.CompanyId
		INNER JOIN 
			HealthInsurance.dbo.InsurancePackage ip on i.InsuranceId = ip.InsuranceId
		WHERE
			i.InsuranceId = @InsuranceId;
	END;
	
--End of Get Insurance Detail

--Get Package Detail
	CREATE PROCEDURE GetPackageDetail
	@PackageId INT
	AS 
	BEGIN 
		SELECT 
			i.IconText, ip.PackageId, ip.PackageName, ip.CoverageDetails, ip.Price, ip.PolicyTermInDay, ic.CompanyName, ic.EstablishedYear, ic.Address, ic.ContactInfo
		FROM
			HealthInsurance.dbo.Insurance i 
		INNER JOIN 
			HealthInsurance.dbo.InsuranceCompany ic on i.CompanyId = ic.CompanyId
		INNER JOIN 
			HealthInsurance.dbo.InsurancePackage ip on i.InsuranceId = ip.InsuranceId
		WHERE
			ip.PackageId = @PackageId;
	END;
	
--End of Get Package Detail

--Get GetUserInsuranceRegistrations
	CREATE PROCEDURE GetUserInsuranceRegistrations
    @Id NVARCHAR(450)
	AS
	BEGIN
	    SELECT IR.RegistrationId,
	           IR.EmployeeId,
	           IR.InsuranceId,
	           IR.PackageId,
	           IP.PackageName,
	           IR.RegistrationStatus,
	           IR.RegistrationDate,
	           IR.ApprovalDate,
	           IR.RejectionReasonId
	    FROM InsuranceRegistration IR
	    INNER JOIN InsurancePackage IP ON IR.PackageId = IP.PackageId
	    WHERE IR.EmployeeId = @Id
	    AND IR.DBStatus = 1;
	END;
	
--End of GetUserInsuranceRegistrations
--Get GetEmployeeList
CREATE  PROCEDURE GetEmployeeList
    @Id NVARCHAR(450),
    @IsManager BIT
AS
BEGIN
    IF @IsManager = 1
    BEGIN
        SELECT 
            E.Id,
            E.FirstName,
            E.LastName,
            E.Email,
            E.PhoneNumber,
            D.DepartmentName
        FROM 
            AspNetUsers E
            INNER JOIN DepartmentManager DM ON E.Id = DM.EmployeeId
            LEFT JOIN Department D ON DM.DepartmentId = D.DepartmentId
        WHERE 
            DM.ManagerId = @Id;
    END
    ELSE
    BEGIN
        PRINT 'User is not a manager';
    END
END;
	
--End of GetEmployeeList
--GetNewEmp
CREATE  PROCEDURE GetNewEmp
    @Id NVARCHAR(450),
    @IsManager BIT
AS
BEGIN
    IF @IsManager = 1
    BEGIN
        SELECT 
            E.Id,
            E.FirstName,
            E.LastName,
            E.Email,
            E.PhoneNumber,
            D.DepartmentName
        FROM 
            AspNetUsers E
            LEFT JOIN DepartmentManager DM ON E.Id = DM.EmployeeId
            LEFT JOIN Department D ON DM.DepartmentId = D.DepartmentId
        WHERE 
            DM.DepartmentId IS NULL 
            AND E.Id NOT IN (SELECT EmployeeId FROM DepartmentManager WHERE IsManager = 1);   
    END
    ELSE
    BEGIN
        PRINT 'You are not allow to view this list.';
    END
END;
--End of GetNewEmp
--Get GetInsuranceRegistrations
CREATE PROCEDURE GetInsuranceRegistrations
    @Id NVARCHAR(450)
AS
BEGIN
    SELECT 
        IR.RegistrationId,
        IR.EmployeeId,
        IR.InsuranceId,
        IR.PackageId,
        IR.PackageName,
        IR.RegistrationStatus,
        IR.RegistrationDate,
        IR.ApprovalDate,
        IR.RejectionReasonId,
        AU.FirstName,
        AU.LastName
    FROM 
        InsuranceRegistration IR
    INNER JOIN
        AspNetUsers AU ON IR.EmployeeId = AU.Id
    WHERE 
        IR.EmployeeId = @Id
        AND IR.RegistrationStatus = 'Pending'
        AND IR.DBStatus = 1;
END;
	
--End of GetInsuranceRegistrations

--GetRejectionReasons
CREATE PROCEDURE GetRejectionReasons
AS
BEGIN
    SELECT ReasonId, ReasonText
    FROM RejectionReason
    WHERE DBStatus = 1;
END;
--End of GetRejectionReasons
-----End of View---------------
-----CREATE-------	
--CREATE A Registration
CREATE PROCEDURE RegisterInsuranceForEmployee
    @Id NVARCHAR(450),
    @PackageId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsuranceId INT;
    DECLARE @RegistrationId INT;
    DECLARE @PackageName NVARCHAR(255);

    -- Get InsuranceID from InsurancePackage
    SELECT @InsuranceId = InsuranceId, @PackageName = PackageName
    FROM InsurancePackage
    WHERE PackageId = @PackageId;

    -- check EmployeeID and InsuranceID are existed or not
    IF NOT EXISTS (
        SELECT 1
        FROM AspNetUsers E
        WHERE E.Id = @Id
           AND EXISTS (
                SELECT 1
                FROM Insurance I
                WHERE I.InsuranceId = @InsuranceId
            )
    )
    BEGIN
        THROW 50000, 'Employee or Insurance does not exist.', 1;
        RETURN;
    END;

    BEGIN TRANSACTION;

    -- add new to InsuranceRegistration
    INSERT INTO InsuranceRegistration (EmployeeId, InsuranceId, PackageId, PackageName, RegistrationStatus, RegistrationDate)
    VALUES (@Id, @InsuranceId, @PackageId, @PackageName, 'Pending', GETDATE());

    -- Get new RegistrationID 
    SET @RegistrationId = SCOPE_IDENTITY();

    -- set ApprovalDate and RejectionReasonID NULL
    UPDATE InsuranceRegistration
    SET ApprovalDate = NULL, RejectionReasonID = NULL
    WHERE RegistrationID = @RegistrationId;

    COMMIT;
END;

--END of CREATE A Registration
--Get DeleteUserInsuranceRegistration
	CREATE PROCEDURE DeleteUserInsuranceRegistration
    @Id NVARCHAR(450),
    @RegistrationId INT
	AS
	BEGIN
	    DECLARE @Status INT
	
	    -- Kiểm tra điều kiện để update DBStatus
	    SELECT @Status = IR.DBStatus
	    FROM InsuranceRegistration IR
	    WHERE IR.RegistrationId = @RegistrationId
	      AND IR.EmployeeId = @Id
	
	    IF @Status = 1
	    BEGIN
	        UPDATE InsuranceRegistration
	        SET DBStatus = 0
	        WHERE RegistrationId = @RegistrationId
	          AND EmployeeId = @Id
	    END
	    ELSE
	    BEGIN
        THROW 51000, 'Cannot delete the registration. Either it is not in pending status or DBStatus is not 1, or the registration does not belong to the specified user.', 1;
    	END
	END;

	
--End of DeleteUserInsuranceRegistration
--GetDepartments
CREATE PROCEDURE GetDepartments
AS
BEGIN
    SELECT 
        DepartmentId,
        DepartmentName
    FROM 
        Department;
END;
-----End of GetDepartments
--GetManagers
CREATE PROCEDURE GetManagers
AS
BEGIN
    SELECT 
        U.FirstName,
        U.LastName,
        DM.EmployeeId
    FROM 
        AspNetUsers U
        INNER JOIN DepartmentManager DM ON U.Id = DM.EmployeeId
    WHERE 
        DM.IsManager = 1;
END;
-----GetManagers---------------	
-----End of CREATE---------------	
-----UPDATE-------	
--ApproveInsuranceRegistration
CREATE PROCEDURE ApproveInsuranceRegistration
    @RegistrationId INT,
    @EmployeeId NVARCHAR(450)
AS
BEGIN
    UPDATE InsuranceRegistration
    SET RegistrationStatus = 'Approved',
        ApprovalDate = GETDATE()
    WHERE RegistrationId = @RegistrationId
      AND EmployeeId = @EmployeeId;
END;
--END of ApproveInsuranceRegistration

--RejectInsuranceRegistration
CREATE PROCEDURE RejectInsuranceRegistration
    @RegistrationId INT,
    @EmployeeId NVARCHAR(450),
    @RejectionReasonId INT
AS
BEGIN
    UPDATE InsuranceRegistration
    SET RegistrationStatus = 'Rejected',
        RejectionReasonId = @RejectionReasonId
    WHERE RegistrationId = @RegistrationId
      AND EmployeeId = @EmployeeId;
END;
--END of RejectInsuranceRegistration

--UpdateEmployeeDepartment
CREATE PROCEDURE UpdateEmployeeDepartment
    @Id NVARCHAR(450), --selected employee to assign
    @EmployeeId NVARCHAR(450), --selected manager for that employee
    @DepartmentId INT
AS
BEGIN
    
    IF EXISTS (SELECT 1 FROM DepartmentManager WHERE EmployeeId = @EmployeeId AND IsManager = 1)
    BEGIN
        
        UPDATE DepartmentManager
        SET ManagerId = @EmployeeId,
            DepartmentId = @DepartmentId
        WHERE EmployeeId = @Id;

        PRINT 'Update completed.';
    END
    ELSE
    BEGIN
        
        PRINT 'You are not manager.';
    END
END;
--END of UpdateEmployeeDepartment
-----End of UPDATE---------------	