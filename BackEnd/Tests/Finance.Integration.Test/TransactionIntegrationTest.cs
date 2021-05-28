using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DtoModels.Transaction;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Serilog;
using Xunit;

namespace Finance.Integration.Test
{
    public class TransactionIntegrationTest
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private readonly Guid trasactionTypeId = Guid.Parse("D21C9520-F320-474F-8C3D-27199C361488");

        public TransactionIntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseSerilog()
                .UseEnvironment("Development")
                .UseSetting("ConnectionStrings:Finance",
                    "server=DESKTOP-8U2HCIL;database=Finance;Trusted_Connection=true;MultipleActiveResultSets=True;")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task AddTransaction()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };

            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Transaction", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var transaction = JsonConvert.DeserializeObject<TransactionDto>(responseString);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            transaction.Id.Should().Be(transactionId);
        }

        [Fact]
        public async Task AddTransactionInternalServerError()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = Guid.NewGuid()
            };

            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Transaction", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task AddTransactionBadRequest()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
            };

            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Transaction", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditTransaction()
        {
            //Arrage
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };

            var transactionViewModelEditDto = new TransactionDto
            {
                Money = 5,
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var contentEdited = JsonConvert.SerializeObject(transactionViewModelEditDto);
            var stringContentEdited = new StringContent(contentEdited, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.PutAsync($"/api/Transaction?id={transactionId}", stringContentEdited);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var transaction = JsonConvert.DeserializeObject<TransactionDto>(responseString);
            transaction.Money.Should().Be(5);
        }

        [Fact]
        public async Task EditTransactionBadRequest()
        {
            //Arrage
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };

            var transactionViewModelEditDto = new TransactionDto
            {
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var contentEdited = JsonConvert.SerializeObject(transactionViewModelEditDto);
            var stringContentEdited = new StringContent(contentEdited, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.PutAsync($"/api/Transaction?id={transactionViewModelEditDto.Id}",
                stringContentEdited);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditTransactionInternalServerError()
        {
            //Arrage
            var transactionId = Guid.NewGuid();
            var invalidTransactionId = "D21C9520-F320-474F-8C3D-27199C361489";

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };

            var transactionViewModelEditDto = new TransactionDto
            {
                Money = 5,
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var contentEdited = JsonConvert.SerializeObject(transactionViewModelEditDto);
            var stringContentEdited = new StringContent(contentEdited, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.PutAsync($"/api/Transaction?id={invalidTransactionId}", stringContentEdited);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task DeleteTransaction()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.DeleteAsync($"/api/Transaction?id={transactionId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(string.Empty);
        }

        [Fact]
        public async Task DeleteTransactionNotFound()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"/api/Transaction?id={transactionId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteTransactionBadRequest()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.DeleteAsync($"/api/Transaction?id=");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        //[Fact]
        //public async Task DeleteTransactionInvalidRequest()
        //{
        //    // Arrange
        //    var transactionId = Guid.NewGuid();

        //    var transactionViewModelDto = new TransactionViewModel
        //    {
        //        Id = transactionId,
        //        Money = 5,
        //        TransactionStatus = true,
        //        TransactionTypeId = trasactionTypeId
        //    };
        //    var content = JsonConvert.SerializeObject(transactionViewModelDto);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    await _client.PostAsync("/api/Transaction", stringContent);
        //    _transactionServiceMock.Setup(x => x.AddTransaction(transactionViewModelDto))
        //                   .Returns(transactionViewModelDto);

        //    // Act
        //    var response = await _client.DeleteAsync($"/api/Transaction?id=");

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        //}

        [Fact]
        public async Task GetAllTransactions()
        {
            //Arragenge
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.GetAsync("/api/Transaction");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var transaction = JsonConvert.DeserializeObject<TransactionDto>(responseString);
            // transaction.Transactions.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetById()
        {
            //Arrage
            var transactionId = Guid.NewGuid();

            var transactionViewModelDto = new TransactionDto
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId
            };
            var content = JsonConvert.SerializeObject(transactionViewModelDto);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Transaction", stringContent);

            // Act
            var response = await _client.GetAsync($"/api/Transaction/{transactionId}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var transaction = JsonConvert.DeserializeObject<TransactionDto>(responseString);
            transaction.Id.Should().Be(transactionId);
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            // Arrange
            var transactionId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/Transaction/{transactionId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByIdBadRequest()
        {
            // Act
            var response = await _client.GetAsync($"/api/Transaction/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}