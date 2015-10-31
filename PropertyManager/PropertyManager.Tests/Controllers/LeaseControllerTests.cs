using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyManager.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using PropertyManager.Controllers;
using PropertyManager.Core.Domain;
using AutoMapper;
using PropertyManager.Tests.Infrastructure;
using PropertyManager.Core.Constant;

namespace PropertyManager.Tests.Controllers
{
    [TestClass]
    public class LeaseControllerTests : BaseTest
    {
        [TestMethod]
        public void GetLeasesReturnsLeases()
        {

            //Arrange: Instantiate LeasesController so its methods can be called
            using (var leaseController = new LeasesController())
            {
                //Act: Call the GetLeases method
                IEnumerable<LeaseModel> leases = leaseController.GetLeases();

                //Assert: Verify that an array was returned with at least one element
                Assert.IsTrue(leases.Count() > 0);
            }
        }

        [TestMethod]
        public void GetLeaseReturnsLease()
        {
            int LeaseIdForTest = 1;

            //Arrange: Instantiate LeasesController so its methods can be called
            var leaseController = new LeasesController();

            //Act: Call the GetLease method
            IHttpActionResult result = leaseController.GetLease(LeaseIdForTest);

            //Assert: 
            // Verify that HTTP status code is OK
            // Verify that returned lease ID is correct
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<LeaseModel>));

            OkNegotiatedContentResult<LeaseModel> contentResult =
                (OkNegotiatedContentResult<LeaseModel>)result;
            Assert.IsTrue(contentResult.Content.LeaseId == LeaseIdForTest);
        }

        [TestMethod]
        public void PostLeaseCreatesLease()
        {
            //Arrange: Instantiate LeasesController so its methods can be called
            var leaseController = new LeasesController();

            //Act: 
            // Create a LeaseModel object populated with test data,
            //  and call PostLease
            var newLease = new LeaseModel
            {
                CreatedDate = new DateTime(2014, 9, 30),
                PropertyId = 1,
                TenantId = 1,
                StartDate = new DateTime(2015, 1, 30),
                Rent = 800,
                LeaseType = Constants.RentPeriod.Monthly
            };
            IHttpActionResult result = leaseController.PostLease(newLease);

            //Assert:
            // Verify that the HTTP result is CreatedAtRouteNegotiatedContentResult
            // Verify that the HTTP result body contains a nonzero lease ID
            Assert.IsInstanceOfType
                (result, typeof(CreatedAtRouteNegotiatedContentResult<LeaseModel>));
            CreatedAtRouteNegotiatedContentResult<LeaseModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<LeaseModel>)result;
            Assert.IsTrue(contentResult.Content.LeaseId != 0);

            // Delete the test lease 
            result = leaseController.DeleteLease(contentResult.Content.LeaseId);
        }

        [TestMethod]
        public void PutLeaseUpdatesLease()
        {
            int leaseIdForTest = 1;
            decimal leaseRentForTest = 987654321;
            Constants.RentPeriod leaseTypeForTest = Constants.RentPeriod.Daily;

            //Arrange: Instantiate LeasesController so its methods can be called
            var leaseController = new LeasesController();

            //Act: 
            // Get an existing lease, change it, and
            //  pass it to PutLease          

            IHttpActionResult result = leaseController.GetLease(leaseIdForTest);
            OkNegotiatedContentResult<LeaseModel> contentResult =
                (OkNegotiatedContentResult<LeaseModel>)result;
            LeaseModel updatedLease = (LeaseModel)contentResult.Content;

            decimal leaseRentBeforeUpdate = updatedLease.Rent;
            Constants.RentPeriod leaseTypeBeforeUpdate = updatedLease.LeaseType;
           
            updatedLease.Rent = leaseRentForTest;
            updatedLease.LeaseType = leaseTypeForTest;

            result = leaseController.PutLease
                                     (updatedLease.LeaseId, updatedLease);

            //Assert: 
            // Verify that HTTP status code is OK
            // Get the lease and verify that it was updated

            var statusCode = (StatusCodeResult)result;

            Assert.IsTrue(statusCode.StatusCode == System.Net.HttpStatusCode.NoContent);

            result = leaseController.GetLease(leaseIdForTest);

            Assert.IsInstanceOfType(result,
                typeof(OkNegotiatedContentResult<LeaseModel>));

            OkNegotiatedContentResult<LeaseModel> readContentResult =
                (OkNegotiatedContentResult<LeaseModel>)result;
            updatedLease = (LeaseModel)readContentResult.Content;

            Assert.IsTrue(updatedLease.Rent == leaseRentForTest);
            Assert.IsTrue(updatedLease.LeaseType == leaseTypeForTest);

            updatedLease.Rent = leaseRentBeforeUpdate;
            updatedLease.LeaseType = leaseTypeBeforeUpdate;

            result = leaseController.PutLease
                                 (updatedLease.LeaseId, updatedLease);
        }

        [TestMethod]
        public void DeleteLeaseDeletesLease()
        {

            //Arrange:                       
            // Instantiate LeasesController so its methods can be called
            // Create a new lease to be deleted, and get its lease ID
            var leaseController = new LeasesController();

            var lease = new LeaseModel
            {
                CreatedDate = new DateTime(2014, 9, 30),
                PropertyId = 1,
                TenantId = 1,
                StartDate = new DateTime(2015, 1, 30),
                Rent = 800,
                LeaseType = Constants.RentPeriod.Monthly
            };
            IHttpActionResult result = leaseController.PostLease(lease);
            CreatedAtRouteNegotiatedContentResult<LeaseModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<LeaseModel>)result;

            int leaseIdToDelete = contentResult.Content.LeaseId;

            
            //Act: Call DeleteLease
            result = leaseController.DeleteLease(leaseIdToDelete);

            //Assert: 
            // Verify that HTTP result is OK
            // Verify that reading deleted lease returns result not found
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<LeaseModel>));

            result = leaseController.GetLease(leaseIdToDelete);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            
        }

    }
}
