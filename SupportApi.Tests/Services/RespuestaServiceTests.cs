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
    public class RespuestaServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            var mockRepo = new Mock<IRespuestaRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Respuesta> { new Respuesta { Id = Guid.NewGuid(), Contenido = "Test", UsuarioId = Guid.NewGuid() } });
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<RespuestaDto>>(It.IsAny<IEnumerable<Respuesta>>())).Returns(new List<RespuestaDto> { new RespuestaDto { Id = Guid.NewGuid(), Contenido = "Test", UsuarioId = Guid.NewGuid() } });
            var service = new RespuestaService(mockRepo.Object, mockMapper.Object);
            var result = await service.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
