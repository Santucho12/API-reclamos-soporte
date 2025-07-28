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
    public class RespuestaControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResultWithList()
        {
            var mockService = new Mock<IRespuestaService>();
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<RespuestaDto> { new RespuestaDto { Id = Guid.NewGuid(), Contenido = "Test", UsuarioId = Guid.NewGuid() } });
            var controller = new RespuestaController(mockService.Object);
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<RespuestaDto>>(okResult.Value);
        }
    }
}
