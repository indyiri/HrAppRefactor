using HrApp.Controllers;
using HrApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Controllers
{
    public class EmployeeControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAllEmployees()
        {
            // arrange
            int numberOfEmployees = 3;
            var mockRepository = RepositoryMocks.GetEmployeeRepository(numberOfEmployees);
            var controller = new EmployeeController(mockRepository.Object);

            // act
            var result = await controller.Index();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.ViewData.Model);
            Assert.Equal(numberOfEmployees, model.Count());
        }
    }
}
