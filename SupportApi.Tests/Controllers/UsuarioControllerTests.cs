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
    public class UsuarioControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResultWithList()
        {
            var mockService = new Mock<IUsuarioService>();
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<UsuarioDto> { new UsuarioDto { Id = Guid.NewGuid(), Nombre = "Test", Email = "test@mail.com", Rol = "user" } });
            var controller = new UsuarioController(mockService.Object);
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<UsuarioDto>>(okResult.Value);
        }
    }
}
