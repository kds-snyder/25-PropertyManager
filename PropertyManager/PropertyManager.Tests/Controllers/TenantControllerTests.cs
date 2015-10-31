using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyManager.Core.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using PropertyManager.Tests.Infrastructure;
using PropertyManager.Core.Constant;
using PropertyManager.Controllers;
using PropertyManager.Core.Models;

namespace PropertyManager.Tests.Controllers
{
    [TestClass]
    public class TenantControllerTests : BaseTest
    {
        [TestMethod]
        public void GetTenantsReturnsTenants()
        {

            //Arrange: Instantiate TenantsController so its methods can be called
            using (var tenantController = new TenantsController())
            {
                //Act: Call the GetTenants method
                IEnumerable<TenantModel> tenants = tenantController.GetTenants();

                //Assert: Verify that an array was returned with at least one element
                Assert.IsTrue(tenants.Count() > 0);
            }
        }

        [TestMethod]
        public void GetTenantReturnsTenant()
        {
            int TenantIdForTest = 1;

            //Arrange: Instantiate TenantsController so its methods can be called
            var tenantController = new TenantsController();

            //Act: Call the GetTenant method
            IHttpActionResult result = tenantController.GetTenant(TenantIdForTest);

            //Assert: 
            // Verify that HTTP status code is OK
            // Verify that returned tenant ID is correct
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<TenantModel>));

            OkNegotiatedContentResult<TenantModel> contentResult =
                (OkNegotiatedContentResult<TenantModel>)result;
            Assert.IsTrue(contentResult.Content.TenantId == TenantIdForTest);
        }

        [TestMethod]
        public void PostTenantCreatesTenant()
        {
            //Arrange: Instantiate TenantsController so its methods can be called
            var tenantController = new TenantsController();

            //Act: 
            // Create a TenantModel object populated with test data,
            //  and call PostTenant
            var newTenant = new TenantModel
            {
                FirstName = "Testy",
                LastName = "McTest",
                Telephone = "555-1212",
                EmailAddress = "test@test.com"
            };
            IHttpActionResult result = tenantController.PostTenant(newTenant);

            //Assert:
            // Verify that the HTTP result is CreatedAtRouteNegotiatedContentResult
            // Verify that the HTTP result body contains a nonzero tenant ID
            Assert.IsInstanceOfType
                (result, typeof(CreatedAtRouteNegotiatedContentResult<TenantModel>));
            CreatedAtRouteNegotiatedContentResult<TenantModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<TenantModel>)result;
            Assert.IsTrue(contentResult.Content.TenantId != 0);

            // Delete the test tenant 
            result = tenantController.DeleteTenant(contentResult.Content.TenantId);
        }

        [TestMethod]
        public void PutTenantUpdatesTenant()
        {
            int tenantIdForTest = 1;
            string tenantFirstNameForTest = "Testy";
            string tenantLastNameForTest = "Testerson";
          
            //Arrange: Instantiate TenantsController so its methods can be called
            var tenantController = new TenantsController();

            //Act: 
            // Get an existing tenant, change it, and
            //  pass it to PutTenant          

            IHttpActionResult result = tenantController.GetTenant(tenantIdForTest);
            OkNegotiatedContentResult<TenantModel> contentResult =
                (OkNegotiatedContentResult<TenantModel>)result;
            TenantModel updatedTenant = (TenantModel)contentResult.Content;

            string tenantFirstNameBeforeUpdate = updatedTenant.FirstName;
            string tenantLastNameBeforeUpdate = updatedTenant.LastName;

            updatedTenant.FirstName = tenantFirstNameForTest;
            updatedTenant.LastName = tenantLastNameForTest;

            result = tenantController.PutTenant
                                     (updatedTenant.TenantId, updatedTenant);

            //Assert: 
            // Verify that HTTP status code is OK
            // Get the tenant and verify that it was updated

            var statusCode = (StatusCodeResult)result;

            Assert.IsTrue(statusCode.StatusCode == System.Net.HttpStatusCode.NoContent);

            result = tenantController.GetTenant(tenantIdForTest);

            Assert.IsInstanceOfType(result,
                typeof(OkNegotiatedContentResult<TenantModel>));

            OkNegotiatedContentResult<TenantModel> readContentResult =
                (OkNegotiatedContentResult<TenantModel>)result;
            updatedTenant = (TenantModel)readContentResult.Content;

            Assert.IsTrue(updatedTenant.FirstName == tenantFirstNameForTest);
            Assert.IsTrue(updatedTenant.LastName == tenantLastNameForTest);

            updatedTenant.FirstName = tenantFirstNameBeforeUpdate;
            updatedTenant.LastName = tenantLastNameBeforeUpdate;

            result = tenantController.PutTenant
                                 (updatedTenant.TenantId, updatedTenant);
        }

        [TestMethod]
        public void DeleteTenantDeletesTenant()
        {

            //Arrange:                       
            // Instantiate TenantsController so its methods can be called
            // Create a new tenant to be deleted, and get its tenant ID
            var tenantController = new TenantsController();

            var tenant = new TenantModel
            {
                FirstName = "Testy",
                LastName = "Testering",
                Telephone = "555-1212",
                EmailAddress = "test@test.com"
            };
            IHttpActionResult result = tenantController.PostTenant(tenant);
            CreatedAtRouteNegotiatedContentResult<TenantModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<TenantModel>)result;

            int tenantIdToDelete = contentResult.Content.TenantId;           

            //Act: Call DeleteTenant
            result = tenantController.DeleteTenant(tenantIdToDelete);

            //Assert: 
            // Verify that HTTP result is OK
            // Verify that reading deleted tenant returns result not found
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<TenantModel>));

            result = tenantController.GetTenant(tenantIdToDelete);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
           
        }

    }
}
