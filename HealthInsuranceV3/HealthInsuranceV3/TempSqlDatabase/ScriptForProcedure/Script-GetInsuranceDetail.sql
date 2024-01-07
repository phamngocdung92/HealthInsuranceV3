USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetInsuranceDetail') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetInsuranceDetail;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetInsuranceDetail
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Insurance Details based on InsuranceId
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetInsuranceDetail @InsuranceId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetInsuranceDetail
    @InsuranceId INT
AS 
BEGIN 
    SET NOCOUNT ON;

    SELECT 
        i.IconText, ip.PackageId, ip.PackageName, ip.CoverageDetails, ip.Price, ip.PolicyTermInDay, ic.CompanyName, ic.EstablishedYear, ic.Address, ic.ContactInfo
    FROM
        dbo.Insurance i 
    INNER JOIN 
        dbo.InsuranceCompany ic ON i.CompanyId = ic.CompanyId
    INNER JOIN 
        dbo.InsurancePackage ip ON i.InsuranceId = ip.InsuranceId
    WHERE
        i.InsuranceId = @InsuranceId;
END;
GO
