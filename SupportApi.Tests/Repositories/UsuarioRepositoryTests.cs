using Xunit;
using SupportApi.Repositories;
using SupportApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SupportApi.Tests.Repositories
{
    public class UsuarioRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_DuplicateEmail_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<SupportDbContext>().UseInMemoryDatabase(databaseName: "TestDbUsuarios").Options;
            using var context = new SupportDbContext(options);
            var repo = new UsuarioRepository(context);
            var usuario1 = new Usuario { Id = Guid.NewGuid(), Nombre = "Test", Email = "test@mail.com", Rol = "user" };
            var usuario2 = new Usuario { Id = Guid.NewGuid(), Nombre = "Test2", Email = "test@mail.com", Rol = "user" };
            await repo.CreateAsync(usuario1);
            var result = await repo.CreateAsync(usuario2);
            Assert.Null(result);
        }
    }
}
