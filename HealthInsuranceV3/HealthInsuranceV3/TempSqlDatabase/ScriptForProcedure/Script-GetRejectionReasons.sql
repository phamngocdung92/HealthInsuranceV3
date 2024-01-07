USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetRejectionReasons') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetRejectionReasons;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetRejectionReasons
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Rejection Reasons with DBStatus = 1
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetRejectionReasons;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetRejectionReasons
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ReasonId, 
        ReasonText
    FROM 
        dbo.RejectionReason
    WHERE 
        DBStatus = 1;
END;
GO
