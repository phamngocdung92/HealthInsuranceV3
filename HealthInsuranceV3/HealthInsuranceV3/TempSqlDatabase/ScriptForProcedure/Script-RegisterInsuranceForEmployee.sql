USE HealthInsurance; -- Chọn database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.RegisterInsuranceForEmployee') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.RegisterInsuranceForEmployee;
GO

/*##################################################################################################  
♣ PROCEDURE ID : RegisterInsuranceForEmployee
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Register Insurance for an Employee
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.RegisterInsuranceForEmployee @Id = 'employee_id_value', @PackageId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.RegisterInsuranceForEmployee
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
    FROM dbo.InsurancePackage
    WHERE PackageId = @PackageId;

    -- Check if EmployeeID and InsuranceID exist
    IF NOT EXISTS (
        SELECT 1
        FROM dbo.AspNetUsers E
        WHERE E.Id = @Id
           AND EXISTS (
                SELECT 1
                FROM dbo.Insurance I
                WHERE I.InsuranceId = @InsuranceId
            )
    )
    BEGIN
        THROW 50000, 'Employee or Insurance does not exist.', 1;
        RETURN;
    END;

    BEGIN TRANSACTION;

    -- Add new record to InsuranceRegistration
    INSERT INTO dbo.InsuranceRegistration (EmployeeId, InsuranceId, PackageId, PackageName, RegistrationStatus, RegistrationDate)
    VALUES (@Id, @InsuranceId, @PackageId, @PackageName, 'Pending', GETDATE());

    -- Get new RegistrationID 
    SET @RegistrationId = SCOPE_IDENTITY();

    -- Set ApprovalDate and RejectionReasonID to NULL
    UPDATE dbo.InsuranceRegistration
    SET ApprovalDate = NULL, RejectionReasonID = NULL
    WHERE RegistrationID = @RegistrationId;

    COMMIT;
END;
GO
