using Xunit;
using Moq;
using SupportApi.Services;
using SupportApi.Repositories;
using SupportApi.DTOs;
using SupportApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportApi.Tests.Services
{
    public class UsuarioServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Usuario> { new Usuario { Id = Guid.NewGuid(), Nombre = "Test", Email = "test@mail.com", Rol = "user" } });
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<UsuarioDto>>(It.IsAny<IEnumerable<Usuario>>())).Returns(new List<UsuarioDto> { new UsuarioDto { Id = Guid.NewGuid(), Nombre = "Test", Email = "test@mail.com", Rol = "user" } });
            var service = new UsuarioService(mockRepo.Object, mockMapper.Object);
            var result = await service.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
