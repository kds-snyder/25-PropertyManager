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

namespace PropertyManager.Tests
{
    [TestClass]
    public class PropertyControllerTests : BaseTest
    {
        [TestMethod]
        public void GetPropertiesReturnsProperties()
        {

            //Arrange: Instantiate PropertiesController so its methods can be called
            using (var propertyController = new PropertiesController())
            {
                //Act: Call the GetProperties method
                IEnumerable<PropertyModel> properties = propertyController.GetProperties();

                //Assert: Verify that an array was returned with at least one element
                Assert.IsTrue(properties.Count() > 0);
            }               
        }

        [TestMethod]
        public void GetPropertyReturnsProperty()
        {
            int PropertyIdForTest = 1;

            //Arrange: Instantiate PropertiesController so its methods can be called
            var propertyController = new PropertiesController();

            //Act: Call the GetProperty method
            IHttpActionResult result = propertyController.GetProperty(PropertyIdForTest);

            //Assert: 
            // Verify that HTTP status code is OK
            // Verify that returned property ID is correct
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<PropertyModel>));

            OkNegotiatedContentResult<PropertyModel> contentResult =
                (OkNegotiatedContentResult<PropertyModel>)result;
            Assert.IsTrue(contentResult.Content.PropertyId == PropertyIdForTest);
        }

        [TestMethod]
        public void PostPropertyCreatesProperty()
        {
           //Arrange: Instantiate PropertiesController so its methods can be called
            var propertyController = new PropertiesController();

            //Act: 
            // Create a PropertyModel object populated with test data,
            //  and call PostProperty
            var newProperty = new PropertyModel
            {
                Name="Huge Penthouse",
                Address1="Some address",
                City="New York",
                State="NY"
            };
            IHttpActionResult result = propertyController.PostProperty(newProperty);

            //Assert:
            // Verify that the HTTP result is CreatedAtRouteNegotiatedContentResult
            // Verify that the HTTP result body contains a nonzero property ID
            Assert.IsInstanceOfType
                (result, typeof(CreatedAtRouteNegotiatedContentResult<PropertyModel>));
            CreatedAtRouteNegotiatedContentResult<PropertyModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<PropertyModel>)result;
            Assert.IsTrue(contentResult.Content.PropertyId != 0);

            // Delete the test property 
            result = propertyController.DeleteProperty(contentResult.Content.PropertyId);
        }

        [TestMethod]
        public void PutPropertyUpdatesProperty()
        {
            int propertyIdForTest = 1;
            string propertyNameForTest = "Lofts";
            string address1ForTest = "234 Smith Street";

            //Arrange: Instantiate PropertiesController so its methods can be called
            var propertyController = new PropertiesController();

            //Act: 
            // Get an existing property, change it, and
            //  pass it to PutProperty          

            IHttpActionResult result = propertyController.GetProperty(propertyIdForTest);
            OkNegotiatedContentResult<PropertyModel> contentResult =
                (OkNegotiatedContentResult<PropertyModel>)result;
            PropertyModel updatedProperty = (PropertyModel)contentResult.Content;

            string propertyNameBeforeUpdate = updatedProperty.Name;
            string propertyAddress1BeforeUpdate = updatedProperty.Address1;

            updatedProperty.Name = propertyNameForTest;
            updatedProperty.Address1 = address1ForTest;

            result = propertyController.PutProperty
                                     (updatedProperty.PropertyId, updatedProperty);

            //Assert: 
            // Verify that HTTP status code is OK
            // Get the property and verify that it was updated

            var statusCode = (StatusCodeResult)result;

            Assert.IsTrue(statusCode.StatusCode == System.Net.HttpStatusCode.NoContent);

            result = propertyController.GetProperty(propertyIdForTest);

            Assert.IsInstanceOfType(result,
                typeof(OkNegotiatedContentResult<PropertyModel>));

            OkNegotiatedContentResult<PropertyModel> readContentResult =
                (OkNegotiatedContentResult<PropertyModel>)result;
            updatedProperty = (PropertyModel)readContentResult.Content;

            Assert.IsTrue(updatedProperty.Name == propertyNameForTest);
            Assert.IsTrue(updatedProperty.Address1 == address1ForTest);

            updatedProperty.Name = propertyNameBeforeUpdate;
            updatedProperty.Address1 = propertyAddress1BeforeUpdate;          

            result = propertyController.PutProperty
                                 (updatedProperty.PropertyId, updatedProperty);
        }

        [TestMethod]
        public void DeletePropertyDeletesProperty()
        {

            //Arrange:                       
            // Instantiate PropertiesController so its methods can be called
            // Create a new property to be deleted, and get its property ID
           
            var propertyController = new PropertiesController();

            var property = new PropertyModel
            {
                Name = "Office Space",
                Address1 = "101 Broadway",
                City = "San Francisco",
                State = "CA"              
            };
            IHttpActionResult propertyResult = propertyController.PostProperty(property);
            CreatedAtRouteNegotiatedContentResult<PropertyModel> contentResult =
                (CreatedAtRouteNegotiatedContentResult<PropertyModel>)propertyResult;

            int propertyIdToDelete = contentResult.Content.PropertyId;

            // Add a lease corresponding to the property
            int createdLeaseId;
            using (var leaseController = new LeasesController())
            {
                var lease = new LeaseModel
                {
                    CreatedDate = new DateTime(2014, 9, 30),
                    PropertyId = propertyIdToDelete,
                    TenantId = 1,
                    StartDate = new DateTime(2015, 1, 30),
                    Rent = 800,
                    LeaseType = Constants.RentPeriod.Monthly
                };
                IHttpActionResult leaseResult = leaseController.PostLease(lease);
                CreatedAtRouteNegotiatedContentResult<LeaseModel> leaseContentResult =
                    (CreatedAtRouteNegotiatedContentResult<LeaseModel>)leaseResult;

                createdLeaseId = leaseContentResult.Content.LeaseId;
            }           

            //Act: Call DeleteProperty
            propertyResult = propertyController.DeleteProperty(propertyIdToDelete);

            //Assert: 
            // Verify that HTTP result is OK
            // Verify that reading deleted property returns result not found
            Assert.IsInstanceOfType(propertyResult, typeof(OkNegotiatedContentResult<PropertyModel>));

            propertyResult = propertyController.GetProperty(propertyIdToDelete);
            Assert.IsInstanceOfType(propertyResult, typeof(NotFoundResult));

            // Verify that the lease created above was deleted
            using (var leaseController = new LeasesController())
            {
                IHttpActionResult leaseResult = leaseController.GetLease(createdLeaseId);
                Assert.IsInstanceOfType(leaseResult, typeof(NotFoundResult));
            }

        }
    }
}
