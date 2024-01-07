USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetDepartments') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetDepartments;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetDepartments
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Departments
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetDepartments;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetDepartments
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DepartmentId,
        DepartmentName
    FROM 
        dbo.Department;
END;
GO
