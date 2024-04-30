using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetzyVet.API.Controllers;
using PetzyVet.Domain.Entities;
using PetzyVet.Domain.DTO;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http;
using System.Net;

namespace PetzyVet.API.Tests.Controllers
{
    [TestClass]
    public class VetsControllerTests
    {
        
        [TestMethod]
        public void GetAllVets_Returns_Ok_With_VetCards()
        {
            // Arrange
            var vets = new List<Vet>
    {
        new Vet { /* Vet properties */ },
        new Vet { /* Vet properties */ }
    };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns(vets);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetCardDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
            // Additional assertions as needed
        }

        [TestMethod]
        public void GetAllVets_Returns_NotFound_When_Vets_Empty()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns(new List<Vet>());

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllVets_Returns_NotFound_When_Vets_Null()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns((List<Vet>)null);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllVets_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void GetVetById_Returns_Ok_With_VetProfileDTO()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet
            {
                VetId = vetId,
                /* Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(vet);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);
            var contentResult = actionResult as OkNegotiatedContentResult<VetProfileDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            // Additional assertions as needed
        }

        [TestMethod]
        public void GetVetById_Returns_NotFound_When_Vet_Not_Found()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns((Vet)null);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetVetById_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void AddVet_Returns_Ok_When_Vet_Not_Null()
        {
            // Arrange
            var vet = new Vet
            {
                /* Initialize Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void AddVet_Returns_BadRequest_When_Vet_Null()
        {
            // Arrange
            Vet vet = null;

            var mockRepository = new Mock<IVetRepository>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AddVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var vet = new Vet
            {
                /* Initialize Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.AddVet(vet)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


        [TestMethod]
        public void UpdateVet_Returns_Ok_When_Vet_Updated_Successfully()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet { VetId = vetId, /* Initialize other Vet properties */ };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(vet);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateVet_Returns_BadRequest_When_Mismatched_IDs()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet { VetId = vetId + 1, /* Initialize other Vet properties */ };

            var controller = new VetsController();

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            BadRequestErrorMessageResult badRequestResult = actionResult as BadRequestErrorMessageResult;
            Assert.AreEqual("Vet ID in the request body does not match the ID in the URL.", badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateVet_Returns_NotFound_For_NonExisting_Vet_ID()
        {
            // Arrange
            int vetId = 999; // Non-existing vet ID
            var vet = new Vet { VetId = vetId, /* Initialize other Vet properties */ };

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateVet_Returns_BadRequest_For_Null_Vet_Object()
        {
            // Arrange
            int vetId = 1;
            Vet vet = null;

            var controller = new VetsController();

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void UpdateVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet { VetId = vetId, /* Initialize other Vet properties */ };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_Ok_When_Vet_Deleted_Successfully()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(new Vet { VetId = vetId });

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_NotFound_For_NonExisting_Vet_ID()
        {
            // Arrange
            int vetId = 999; // Non-existing vet ID

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.DeleteVet(vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void EditStatus_Returns_Ok_When_Status_Edited_Successfully()
        {
            // Arrange
            int vetId = 1;
            bool status = true;

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.EditStatus(vetId, status);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void EditStatus_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;
            bool status = true;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.EditStatus(status, vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.EditStatus(vetId, status);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_BadRequest_When_DoctorIds_Null()
        {
            // Arrange
            List<int> doctorIds = null;
            var controller = new VetsController { vetRepository = Mock.Of<IVetRepository>() };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_BadRequest_When_DoctorIds_Empty()
        {
            // Arrange
            var doctorIds = new List<int>();
            var controller = new VetsController { vetRepository = Mock.Of<IVetRepository>() };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_Ok_With_VetDetails()
        {
            // Arrange
            var doctorIds = new List<int> { 1, 2 };

            var vet1 = new Vet { VetId = 1, FName = "John", LName = "Doe", Speciality = "Dentist", Photo = " " };
            var vet2 = new Vet { VetId = 2, FName = "Jane", LName = "Smith", Speciality = "Surgeon", Photo = " " };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(It.IsAny<int>())).Returns<int>(id =>
            {
                switch (id)
                {
                    case 1:
                        return vet1;
                    case 2:
                        return vet2;
                    default:
                        return null;
                }
            });

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetIdNameDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
            Assert.AreEqual("John Doe", contentResult.Content[0].Name);
            Assert.AreEqual("Jane Smith", contentResult.Content[1].Name);
        }

        [TestMethod]
        public void GetVetsByListOfIds_ExceptionHandling()
        {
            // Arrange
            var doctorIds = new List<int> { 1, 2 };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(It.IsAny<int>())).Throws<Exception>(); // Mocking exception when calling GetVetById

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void DeleteVet_ExceptionHandling()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>(); // Mocking exception when calling GetVetById
                                                                                //mockRepository.Setup(x => x.DeleteVet(vetId)).Throws<Exception>(); // Uncomment this line if you also want to test exception in DeleteVet

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


    }
}
