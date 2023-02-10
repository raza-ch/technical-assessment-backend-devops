using Demo_API.Controllers;
using Demo_API.Models;
using Demo_API.Services.BaseHttpReqester;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace DemoApiTest
{
    [TestClass]
    public class TargetAssetControllerTest
    {
        TargetAssetController targetAssetController;
        IBaseHttpReqester baseHttpReqester;

        List<TargetAsset> dummyList;

        public TargetAssetControllerTest()
        {
            dummyList = new List<TargetAsset>
            {
                new TargetAsset {
                    id = 1,
                    isStartable = true,
                    location = "Berlin",
                    owner = "jon.wayne@example.com",
                    createdBy = "christian.bale@example.com",
                    name = "SRVDEV01",
                    status = "Running",
                    tags = new List<string>(),
                    cpu = 4,
                    ram = 6442450944,
                    parentId = 2
                },
                  new TargetAsset {
                    id = 2,
                    isStartable = true,
                    location = "Paris",
                    owner = "max.power@example.com",
                    createdBy = "christian.bale@example.com",
                    name = "SRVTEST01",
                    status = "Running",
                    tags = new List<string>(),
                    cpu = 2,
                    ram = 6442450944,
                    parentId = null
                  },
                  new TargetAsset {
                    id = 3,
                    isStartable = true,
                    location = "Rome",
                    owner = "peter.parker@example.com",
                    createdBy = null,
                    name = "SRVPROD01",
                    status = "Stopped",
                    tags = new List<string>(),
                    cpu = 8,
                    ram = 8589934592,
                    parentId = 1
                  },
                  new TargetAsset {
                    id = 4,
                    isStartable = false,
                    location = "Paris",
                    owner = "max.power@example.com",
                    createdBy = "christian.bale@example.com",
                    name = "SRVTEST01 (Copy 1)",
                    status = "MigrationFailed",
                    tags = new List<string>(),
                    cpu = 2,
                    ram = 6442450944,
                    parentId = 6
                  },
                  new TargetAsset {
                    id = 5,
                    isStartable = null,
                    location = "Paris",
                    owner = "max.power@example.com",
                    createdBy = "christian.bale@example.com",
                    name = "SRVTEST02 (Copy 2)",
                    status = "Unknown",
                    tags = new List<string>(),
                    cpu = 2,
                    ram = 6442450944,
                    parentId = 4
                  },
                  new TargetAsset {
                    id = 6,
                    isStartable = false,
                    location = "Paris",
                    owner = "max.power@example.com",
                    createdBy = "christian.bale@example.com",
                    name = "SRVTEST02 (Copy 3)",
                    status = "MigrationFailed",
                    tags = new List<string>(),
                    cpu = 2,
                    ram = 6442450944,
                    parentId = 5
                  }
            };
        }

        [TestMethod]
        public async Task TestGet_Request()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dummyList), Encoding.UTF8, "application/json")
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            baseHttpReqester = new BaseHttpReqester(mockHttpClientFactory.Object);

            string url = "https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/targetAsset";

            var test = await baseHttpReqester.Get_Request(url);

            Assert.IsNotNull(test);
            Assert.IsTrue(test.IsSuccessStatusCode);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, test.StatusCode);
        }

        [TestMethod]
        public async Task TestGet_RequestNotFound()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            baseHttpReqester = new BaseHttpReqester(mockHttpClientFactory.Object);

            string url = "https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/targetAsset";

            var test = await baseHttpReqester.Get_Request(url);

            Assert.IsNotNull(test);
            Assert.AreEqual(HttpStatusCode.NotFound, test.StatusCode);
        }

        [TestMethod]
        public async Task TestGet()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dummyList), Encoding.UTF8, "application/json")
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            baseHttpReqester = new BaseHttpReqester(mockHttpClientFactory.Object);

            var logger = new Mock<ILogger<TargetAssetController>>();

            targetAssetController = new TargetAssetController(baseHttpReqester, logger.Object);

            var r = await targetAssetController.Get();

            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(List<EnrichedTargetAsset>));
        }

        [TestMethod]
        public async Task TestGetHttpRequestException()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(JsonConvert.SerializeObject(dummyList), Encoding.UTF8, "application/json")
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            baseHttpReqester = new BaseHttpReqester(mockHttpClientFactory.Object);


            var logger = new Mock<ILogger<TargetAssetController>>();

            targetAssetController = new TargetAssetController(baseHttpReqester, logger.Object);


            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => targetAssetController.Get());
        }
    }
}