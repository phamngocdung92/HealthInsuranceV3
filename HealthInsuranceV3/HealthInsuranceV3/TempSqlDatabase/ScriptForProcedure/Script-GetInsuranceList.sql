USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetInsuranceList') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetInsuranceList;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetInsuranceList
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Insurance List
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer    Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetInsuranceList;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetInsuranceList
AS 
BEGIN 
    SET NOCOUNT ON;

    SELECT 
        i.InsuranceName, i.Description, i.IconText, i.InsuranceId, ic.CompanyName, ic.ContactInfo, ic.Address, ic.EstablishedYear 
    FROM
        dbo.Insurance i 
    INNER JOIN 
        dbo.InsuranceCompany ic ON i.CompanyId = ic.CompanyId;
END;
GO
