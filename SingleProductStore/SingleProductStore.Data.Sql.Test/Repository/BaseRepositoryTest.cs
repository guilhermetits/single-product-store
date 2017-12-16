using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;
using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository;
using SingleProductStore.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SingleProductStore.Data.Sql.Test.Repository
{
    public class BaseRepositoryTest
    {
        [Fact]
        public async Task Insert_AutoCommitNotEnabled_ShouldNotSaveAsync()
        {
            var contextMock = new Mock<IDbContext>();
            var setMock = new Mock<DbSet<Product>>();
            contextMock.Setup(x => x.Set<Product>()).Returns(setMock.Object);
            var repository = new ProductRepository(contextMock.Object);

            repository.AutoCommitEnabled = false;
            await repository.Insert(new Product());

            setMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
            contextMock.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public async Task Insert_AutoCommitEnabled_ShouldNotSaveAsync()
        {
            var contextMock = new Mock<IDbContext>();
            var setMock = new Mock<DbSet<Product>>();
            contextMock.Setup(x => x.Set<Product>()).Returns(setMock.Object);
            var repository = new ProductRepository(contextMock.Object);

            await repository.Insert(new Product());

            setMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        [Fact]
        public async Task InsertRange_AutoCommitEnabled_ShouldSaveOnceAsync()
        {
            var contextMock = new Mock<IDbContext>();
            var setMock = new Mock<DbSet<Product>>();
            contextMock.Setup(x => x.Set<Product>()).Returns(setMock.Object);
            var repository = new ProductRepository(contextMock.Object);

            var data = new List<Product> { new Product(), new Product(), new Product() };
            await repository.InsertRange(data);

            setMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
            setMock.Verify(x => x.AddRange(data), Times.Once);
            contextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

    }
}
