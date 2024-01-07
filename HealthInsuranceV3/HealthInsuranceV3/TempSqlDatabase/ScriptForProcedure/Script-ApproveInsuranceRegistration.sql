USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.ApproveInsuranceRegistration') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.ApproveInsuranceRegistration;
GO

/*##################################################################################################  
♣ PROCEDURE ID : ApproveInsuranceRegistration
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Approve Insurance Registration for a specific Employee
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.ApproveInsuranceRegistration @RegistrationId = 1, @EmployeeId = 'employee_id_value';
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.ApproveInsuranceRegistration
    @RegistrationId INT,
    @EmployeeId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.InsuranceRegistration
    SET RegistrationStatus = 'Approved',
        ApprovalDate = GETDATE()
    WHERE RegistrationId = @RegistrationId
      AND EmployeeId = @EmployeeId;
END;
GO
