using IMS.Application.Exceptions;
using IMS.Application.Features.Products.Commands.CreateProduct;
using IMS.Application.Repositories;
using IMS.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Tests.Features.Products.Command
{
    public class CreateProductCommandTest
    {


        [Fact]
        public async Task ShouldThrowValidationExceptionWhenTitleIsGreaterThan40()
        {
            //Arrange
            var mockRepository = new Mock<IProductRepository>();

            var command = new CreateProductCommand()
            {
                Title = "Vivobook 16 R1605ZA - Vivobook 16 R1605ZA - Vivobook 16 R1605ZA - Vivobook 16 R1605ZA",
                Price = 1000.0M,
                Discount = 20
            };

            var handler=new CreateProductCommandHandler(mockRepository.Object);

            var exception = await Assert.ThrowsAsync<IMSValidationException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            }); 

            Assert.Equal("Title must not exceed 40 characters.", exception.ValdationErrors.First());
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionWhenTitleIsNotUnique()
        {
            //Arrange
            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.IsProductTitleUnique(It.IsAny<string>())).Returns(Task.FromResult(true));

            var command = new CreateProductCommand()
            {
                Title = "Vivobook 16 R1605ZA",
                Price = 1000.0M,
                Discount = 20
            };

            var handler = new CreateProductCommandHandler(mockRepository.Object);

            var exception = await Assert.ThrowsAsync<IMSValidationException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal("Product's title already exists.", exception.ValdationErrors.First());
        }
    }
}
