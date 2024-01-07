USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.DeleteUserInsuranceRegistration') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.DeleteUserInsuranceRegistration;
GO

/*##################################################################################################  
♣ PROCEDURE ID : DeleteUserInsuranceRegistration
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Delete User Insurance Registration based on EmployeeId and RegistrationId
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.DeleteUserInsuranceRegistration @Id = 'employee_id_value', @RegistrationId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.DeleteUserInsuranceRegistration
    @Id NVARCHAR(450),
    @RegistrationId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Status INT;
    
    SELECT @Status = IR.DBStatus
    FROM dbo.InsuranceRegistration IR
    WHERE IR.RegistrationId = @RegistrationId
      AND IR.EmployeeId = @Id;

    IF @Status = 1
    BEGIN
        UPDATE dbo.InsuranceRegistration
        SET DBStatus = 0
        WHERE RegistrationId = @RegistrationId
          AND EmployeeId = @Id;
    END
    ELSE
    BEGIN
        THROW 51000, 'Cannot delete the registration. Either it is not in pending status or DBStatus is not 1, or the registration does not belong to the specified user.', 1;
    END
END;
GO
