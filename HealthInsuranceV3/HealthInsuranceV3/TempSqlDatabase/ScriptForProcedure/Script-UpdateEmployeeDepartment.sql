USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.UpdateEmployeeDepartment') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.UpdateEmployeeDepartment;
GO

/*##################################################################################################  
♣ PROCEDURE ID : UpdateEmployeeDepartment
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Update Employee's Department and Manager
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.UpdateEmployeeDepartment @Id = 'selected_employee_id', @EmployeeId = 'selected_manager_id', @DepartmentId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.UpdateEmployeeDepartment
    @Id NVARCHAR(450), --selected employee to assign
    @EmployeeId NVARCHAR(450), --selected manager for that employee
    @DepartmentId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.DepartmentManager WHERE EmployeeId = @EmployeeId AND IsManager = 1)
    BEGIN
        UPDATE dbo.DepartmentManager
        SET ManagerId = @EmployeeId,
            DepartmentId = @DepartmentId
        WHERE EmployeeId = @Id;

        PRINT 'Update completed.';
    END
    ELSE
    BEGIN
        PRINT 'You are not a manager.';
    END
END;
GO
