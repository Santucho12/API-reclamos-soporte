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
    public class ReclamoServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            var mockRepo = new Mock<IReclamoRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Reclamo> { new Reclamo { Id = Guid.NewGuid(), Titulo = "Test", Descripcion = "Desc" } });
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<IEnumerable<ReclamoDto>>(It.IsAny<IEnumerable<Reclamo>>())).Returns(new List<ReclamoDto> { new ReclamoDto { Id = Guid.NewGuid(), Titulo = "Test", Descripcion = "Desc" } });
            var service = new ReclamoService(mockRepo.Object, mockMapper.Object);
            var result = await service.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
