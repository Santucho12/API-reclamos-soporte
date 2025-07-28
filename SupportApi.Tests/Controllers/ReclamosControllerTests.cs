using Xunit;
using Moq;
using SupportApi.Controllers;
using SupportApi.Services;
using SupportApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportApi.Tests.Controllers
{
    public class ReclamosControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResultWithList()
        {
            var mockService = new Mock<IReclamoService>();
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<ReclamoDto> { new ReclamoDto { Id = Guid.NewGuid(), Titulo = "Test", Descripcion = "Desc" } });
            var controller = new ReclamosController(mockService.Object);
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<ReclamoDto>>(okResult.Value);
        }
    }
}
