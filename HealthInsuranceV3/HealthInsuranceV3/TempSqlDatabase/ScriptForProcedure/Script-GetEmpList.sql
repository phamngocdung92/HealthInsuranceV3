USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetEmployeeList') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetEmployeeList;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetEmployeeList
♣ PROGRAM NAME : HealthInsurance
♣ DESCRIPTION : 
			ENG: Get Employee List based on the input parameters
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer    Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Modified  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetEmployeeList @Id = 'employee_id_value', @IsManager = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetEmployeeList
    @Id NVARCHAR(450),
    @IsManager BIT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

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
            dbo.AspNetUsers E
            INNER JOIN dbo.DepartmentManager DM ON E.Id = DM.EmployeeId
            LEFT JOIN dbo.Department D ON DM.DepartmentId = D.DepartmentId
        WHERE 
            DM.ManagerId = @Id;
    END
    ELSE
    BEGIN
        PRINT 'User is not a manager';
    END
END;
GO
