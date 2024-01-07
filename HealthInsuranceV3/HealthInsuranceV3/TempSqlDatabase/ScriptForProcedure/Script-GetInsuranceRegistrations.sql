USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetInsuranceRegistrations') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetInsuranceRegistrations;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetInsuranceRegistrations
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Insurance Registrations for a specific Employee with 'Pending' status
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetInsuranceRegistrations @Id = 'employee_id_value';
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetInsuranceRegistrations
    @Id NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

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
        dbo.InsuranceRegistration IR
    INNER JOIN
        dbo.AspNetUsers AU ON IR.EmployeeId = AU.Id
    WHERE 
        IR.EmployeeId = @Id
        AND IR.RegistrationStatus = 'Pending'
        AND IR.DBStatus = 1;
END;
GO
