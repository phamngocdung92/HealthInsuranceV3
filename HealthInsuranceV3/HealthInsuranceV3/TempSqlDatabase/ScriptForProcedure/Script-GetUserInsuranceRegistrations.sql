USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetUserInsuranceRegistrations') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetUserInsuranceRegistrations;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetUserInsuranceRegistrations
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get User Insurance Registrations based on EmployeeId
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetUserInsuranceRegistrations @Id = 'employee_id_value';
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetUserInsuranceRegistrations
    @Id NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IR.RegistrationId,
        IR.EmployeeId,
        IR.InsuranceId,
        IR.PackageId,
        IP.PackageName,
        IR.RegistrationStatus,
        IR.RegistrationDate,
        IR.ApprovalDate,
        IR.RejectionReasonId
    FROM 
        dbo.InsuranceRegistration IR
    INNER JOIN 
        dbo.InsurancePackage IP ON IR.PackageId = IP.PackageId
    WHERE 
        IR.EmployeeId = @Id
        AND IR.DBStatus = 1;
END;
GO
