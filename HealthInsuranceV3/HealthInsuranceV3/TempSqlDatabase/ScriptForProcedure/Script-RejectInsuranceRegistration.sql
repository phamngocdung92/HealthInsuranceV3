USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.RejectInsuranceRegistration') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.RejectInsuranceRegistration;
GO

/*##################################################################################################  
♣ PROCEDURE ID : RejectInsuranceRegistration
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Reject Insurance Registration for a specific Employee
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.RejectInsuranceRegistration @RegistrationId = 1, @EmployeeId = 'employee_id_value', @RejectionReasonId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.RejectInsuranceRegistration
    @RegistrationId INT,
    @EmployeeId NVARCHAR(450),
    @RejectionReasonId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.InsuranceRegistration
    SET RegistrationStatus = 'Rejected',
        RejectionReasonId = @RejectionReasonId
    WHERE RegistrationId = @RegistrationId
      AND EmployeeId = @EmployeeId;
END;
GO
