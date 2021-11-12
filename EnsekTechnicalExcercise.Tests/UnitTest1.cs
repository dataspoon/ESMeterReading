using EnsekTechnicalExercise.Api.Controllers;
using EnsekTechnicalExercise.Api.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace EnsekTechnicalExcercise.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "";
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var processorMock = new Mock<IMeterReadingProcessor>();
            processorMock.Setup(s => s.processMeterReadings(It.IsAny<IFormFile>())).Returns(new ProcessMeterReadingsResults { FailedReadings = 1, SuccessfulReadings = 1 });

            var sut = new MeterReadingController(processorMock.Object);
            var file = fileMock.Object;

            //Act
            var result = await sut.MeterReadingUpload(file);

            //Assert
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}