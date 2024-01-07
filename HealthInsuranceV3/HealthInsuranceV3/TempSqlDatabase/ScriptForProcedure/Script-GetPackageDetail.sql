USE HealthInsurance; -- Select database

IF EXISTS (SELECT * FROM HealthInsurance.dbo.sysobjects WHERE id = object_id(N'dbo.GetPackageDetail') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.GetPackageDetail;
GO

/*##################################################################################################  
♣ PROCEDURE ID : GetPackageDetail
♣ PROGRAM NAME : Health Insurance
♣ DESCRIPTION : 
			ENG: Get Package Details based on PackageId
			KOR:  
			VIE: 
---------------------------------------------------------------------------------------------  
♣ Date      Writer             Change History
---------------------------------------------------------------------------------------------  
   2024-01-07   PhamNgocDung     Created  

---------------------------------------------------------------------------------------------  
♣ Sample Execute : 
EXEC dbo.GetPackageDetail @PackageId = 1;
♣ Note : 

####################################################################################################*/ 
CREATE PROCEDURE dbo.GetPackageDetail
    @PackageId INT
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
        ip.PackageId = @PackageId;
END;
GO
