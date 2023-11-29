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
        [TestMethod]
        public void IndexReturnsView()
        {
            var results = (ViewResult)controller.Index().Result;
            Assert.AreEqual("Index", results.ViewName);
        }

    }
}