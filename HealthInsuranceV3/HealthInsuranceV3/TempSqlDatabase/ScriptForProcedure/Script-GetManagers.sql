USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetManagers') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetManagers;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetManagers
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Managers
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetManagers;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetManagers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.FirstName,
        U.LastName,
        DM.EmployeeId
    FROM 
        dbo.AspNetUsers U
        INNER JOIN dbo.DepartmentManager DM ON U.Id = DM.EmployeeId
    WHERE 
        DM.IsManager = 1;
END;
GO
