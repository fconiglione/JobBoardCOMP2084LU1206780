using Microsoft.AspNetCore.Mvc;
using JobBoardCOMP2084LU1206780.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobBoardCOMP2084LU1206780.Data;
using Microsoft.EntityFrameworkCore;
using JobBoardCOMP2084LU1206780.Models;

namespace JobBoardCOMP2084LU1206780Tests
{
    [TestClass]
    public class CompaniesControllerTest
    {
        ApplicationDbContext _context;
        CompaniesController controller;

        // Initializing tests with mock data
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var company = new Company { CompanyId = 71, Name = "Company A", Industry = "IT", Location = "Some Location" };
            _context.Companies.Add(company);

            company = new Company { CompanyId = 191, Name = "Company B", Industry = "Fashion", Location = "Some Location" };
            _context.Companies.Add(company);

            company = new Company { CompanyId = 253, Name = "Company C", Industry = "Lumber", Location = "Some Location" };
            _context.Companies.Add(company);

            _context.SaveChanges();

            controller = new CompaniesController(_context);
        }

        // Delete [GET] Test Methods
        // Note: I could not figure out how to test for user authorization otherwise I would have included this in the test methods

        // Testing Delete() with null id
        [TestMethod]
        public async Task DeleteNullIdReturnsNotFound()
        {
            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsNotNull(result);

            // Ensuring NotFound() is returned
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        // Testing Delete() with valid id
        [TestMethod]
        public async Task DeleteValidIdReturnsNotFound()
        {
            // Act
            var result = await controller.Delete(71);

            // Assert
            Assert.IsNotNull(result);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);

            // Ensure item is properly deleted
            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        // Testing Delete() with invalid id
        [TestMethod]
        public async Task DeleteInvalidIdReturnsNotFound()
        {
            // Act
            var result = await controller.Delete(97);

            // Assert
            Assert.IsNotNull(result);

            // Ensuring NotFound() is returned
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        [TestMethod]
        public async Task DeleteValidIdReturnsCorrectCompany()
        {
            // Act : using a valid CompanyId
            var result = await controller.Details(71);

            // Assert
            Assert.IsNotNull(result);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);

            var model = viewResult.Model as Company;
            Assert.IsNotNull(model);

            // Compare company IDs; ensure correct company is deleted
            Assert.AreEqual(71, model.CompanyId);
        }

        // Delete [POST] Test Methods
        // Note: I would have liked to have made one more test method to test for null company context

        // Testing DeleteConfirmed() with valid CompanyId
        [TestMethod]
        public async Task DeleteCompanyWithValidIdFromDb()
        {
            // Act : using a valid CompanyId
            var result = await controller.DeleteConfirmed(191);

            // Assert
            Assert.IsNotNull(result);

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);

            // Check if the company was deleted from the database
            var deletedCompany = await _context.Companies.FindAsync(191);
            Assert.IsNull(deletedCompany);
        }

        // Testing DeleteConfirmed() with invalid ID
        [TestMethod]
        public async Task DeleteCompanyWithInvalidIdFromDb()
        {
            // Act : using an invalid CompanyId
            var result = await controller.DeleteConfirmed(999);

            // Assert
            Assert.IsNotNull(result);

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);

            // Ensure that no company was deleted from the database
            var notDeletedCompany = await _context.Companies.FindAsync(999);
            Assert.IsNull(notDeletedCompany);
        }
    }
}