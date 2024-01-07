USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetNewEmp') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetNewEmp;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetNewEmp
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get New Employees who are not assigned to any department
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetNewEmp @Id = 'employee_id_value', @IsManager = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetNewEmp
    @Id NVARCHAR(450),
    @IsManager BIT
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
            LEFT JOIN dbo.DepartmentManager DM ON E.Id = DM.EmployeeId
            LEFT JOIN dbo.Department D ON DM.DepartmentId = D.DepartmentId
        WHERE 
            DM.DepartmentId IS NULL 
            AND E.Id NOT IN (SELECT EmployeeId FROM dbo.DepartmentManager WHERE IsManager = 1);   
    END
    ELSE
    BEGIN
        PRINT 'You are not allowed to view this list.';
    END
END;
GO
