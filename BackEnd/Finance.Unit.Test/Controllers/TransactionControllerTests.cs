//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Finance.Activities;
//using Finance.Application.Interfaces;
//using Finance.Application.ViewModels.Transaction;
//using Finance.Controllers;
//using Finance.Infrastructure.IoC;
//using Finance.Unit.Test.Controllers.Helpers;
//using FluentAssertions;
//using Limilabs.Client;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using Xunit;

//namespace Finance.Unit.Test.Controllers
//{
//    public class TransactionControllerTests : ControllerTestBase
//    {
//        private Mock<IMediator> _mediator =
//            new Mock<IMediator>();

//        private ServiceCollection services = new ServiceCollection();

//       // private readonly BaseApiController _;

//        private readonly Mock<ITransactionService> _transactionServiceMock =
//            new Mock<ITransactionService>();

//        private readonly TransactionController _transactionController;

//        public TransactionControllerTests()
//        {
//            _transactionController = new TransactionController();
//          //  services.AddMediatR(typeof(TransactionList.Handler).Assembly);
//            services.AddIoCService();
//        }

//        [Fact]
//        public async Task GetAllTransactionsAsync()
//        {
//            //Arrange

//            var transactionViewModelDto = new TransactionListDto();

//            _mediator.Setup(x => x.Send(It.IsAny<TransactionList.Query>(), default(CancellationToken))).ReturnsAsync(transactionViewModelDto);

//            //Act
//            var transaction = _transactionController.GetAllTransactions();

//            //Assert
//            var okResult = transaction.Should().BeOfType<Task<OkObjectResult>>().Subject;
//            okResult.Result.Value.Should().BeAssignableTo<TransactionListDto>();
//        }

//        //[Fact]
//        //public void GetByIdTransactionValidResponse()
//        //{
//        //    //Arrange
//        //    var transactionId = Guid.NewGuid();
//        //    var trasactionTypeId = Guid.NewGuid();

//        //    var transactionViewModelDto = new TransactionViewModel();

//        //    _transactionServiceMock.Setup(x => x.GetByIdTransaction(transactionId)).Returns(transactionViewModelDto);

//        //    //Act
//        //    var transaction = _transactionController.GetTransactionById(transactionId);

//        //    //Assert
//        //    var okResult = transaction.Should().BeOfType<OkObjectResult>().Subject;
//        //    okResult.Value.Should().BeAssignableTo<TransactionViewModel>();
//        //}

//        //[Fact]
//        //public async Task GetByIdTransactionNotFound()
//        //{
//        //    //Arrange Mediator.Send(new TransactionList.Query());
//        //    var transactionId = Guid.NewGuid();

//        //    _mediator.Setup(x => x.GetByIdTransaction(transactionId)).Returns(() => null);

//        //    //Act
//        //    var transaction = await Mediator.Send(new TransactionList.Query());

//        //    //Assert
//        //    var okResult = transaction.Should().BeOfType<NotFoundObjectResult>().Subject;
//        //}

//        //[Fact]
//        //public void GetByIdTransactionBadRequst()
//        //{
//        //    //Arrange
//        //    var transactionId = Guid.NewGuid();

//        //    //Act
//        //    var transaction = _transactionController.GetTransactionById(transactionId);

//        //    //Assert
//        //    var okResult = transaction.Should().BeOfType<BadRequestResult>().Subject;
//        //}



//        //[Fact]
//        //public void GetAllTransactionsInternalServerError()
//        //{
//        //    //Arrange

//        //    var transactionViewModelDto = new TransactionListViewModel();

//        //    _transactionServiceMock.Setup(x => x.GetAllTransactions()).Returns(null);

//        //    //Act
//        //    var transaction = _transactionController.GetAllTransactions();

//        //    //Assert
//        //    var okResult = transaction.Should().BeOfType<ServerException>();
//        //}

//        //        [Fact]
//        //        public void AddTransaction()
//        //        {
//        //            //Arrange
//        //            var transactionId = Guid.NewGuid();
//        //            var trasactionTypeId = Guid.NewGuid();

//        //            var transactionViewModelDto = new TransactionViewModel
//        //            {
//        //                Id = transactionId,
//        //                Money = 5,
//        //                TransactionStatus = true,
//        //                TransactionTypeId = trasactionTypeId
//        //            };

//        //            _transactionServiceMock.Setup(x => x.AddTransaction(transactionViewModelDto))
//        //                .Returns(transactionViewModelDto);

//        //            //Act
//        //            var transaction = _transactionController.AddTransaction(transactionViewModelDto);

//        //            //Assert
//        //            var okResult = transaction.Should().BeOfType<CreatedResult>().Subject;
//        //            var transactionResult = okResult.Value.Should().BeAssignableTo<TransactionViewModel>().Subject;
//        //            transactionResult.Id.Should().Be(transactionId);
//        //            transactionResult.Money.Should().Be(5);
//        //        }

//        //        [Fact]
//        //        public void AddTransactionDiffrentData()
//        //        {
//        //            //Arrange
//        //            var transactionId = Guid.NewGuid();
//        //            var trasactionTypeId = Guid.NewGuid();

//        //            var transactionViewModelDto = new TransactionViewModel
//        //            {
//        //                Id = transactionId,
//        //                Money = 5,
//        //                TransactionStatus = true,
//        //                TransactionTypeId = trasactionTypeId
//        //            };

//        //            var transactionViewModelDtoInvalid = new TransactionViewModel
//        //            {
//        //                Id = transactionId,
//        //                Money = 2,
//        //                TransactionStatus = true,
//        //                TransactionTypeId = trasactionTypeId
//        //            };

//        //            _transactionServiceMock.Setup(x => x.AddTransaction(transactionViewModelDto))
//        //                .Returns(transactionViewModelDto);

//        //            //Act
//        //            var transaction = _transactionController.AddTransaction(transactionViewModelDtoInvalid);

//        //            //Assert
//        //            var okResult = transaction.Should().BeOfType<CreatedResult>().Subject;
//        //            var transactionResult = okResult.Value.Should().BeAssignableTo<TransactionViewModel>().Subject;
//        //            transactionResult.Id.Should().Be(transactionId);
//        //            transactionResult.Money.Should().Be(2);
//        //        }

//        //        [Fact]
//        //        public void EditTransaction()
//        //        {
//        //            //Arrange
//        //            var transactionId = Guid.NewGuid();
//        //            var trasactionTypeId = Guid.NewGuid();

//        //            var transactionViewModelDto = new TransactionViewModel
//        //            {
//        //                Id = transactionId,
//        //                Money = 5,
//        //                TransactionStatus = true,
//        //                TransactionTypeId = trasactionTypeId
//        //            };

//        //            var transactionEditViewModelDto = new TransactionEditViewModel
//        //            {
//        //                Money = 5,
//        //            };

//        //            _transactionServiceMock.Setup(x => x.GetByIdTransaction(transactionId)).Returns(transactionViewModelDto);

//        //            //Act
//        //            var transaction = _transactionController.EditTransaction(transactionViewModelDto.Id, transactionEditViewModelDto);

//        //            //Assert
//        //            var okResult = transaction.Should().BeOfType<OkObjectResult>().Subject;
//        //            var transactionResult = okResult.Value.Should().BeAssignableTo<TransactionEditViewModel>().Subject;
//        //            transactionResult.Money.Should().Be(5);
//        //        }

//        //        [Fact]
//        //        public void DeleteTransactionValidResponse()
//        //        {
//        //            //Arrange
//        //            var transactionId = Guid.NewGuid();
//        //            var trasactionTypeId = Guid.NewGuid();

//        //            var transactionViewModelDto = new TransactionViewModel();

//        //            _transactionServiceMock.Setup(x => x.GetByIdTransaction(transactionId)).Returns(transactionViewModelDto);

//        //            //Act
//        //            var transaction = _transactionController.DeleteTransaction(transactionId);

//        //            //Assert
//        //            var okResult = transaction.Should().BeOfType<NoContentResult>().Subject;
//        //        }

//        //        [Fact]
//        //        public void DeleteTransactionNotFound()
//        //        {
//        //            //Arrange
//        //            var transactionId = Guid.NewGuid();
//        //            var trasactionTypeId = Guid.NewGuid();

//        //            var transactionViewModelDto = new TransactionViewModel();

//        //            _transactionServiceMock.Setup(x => x.GetByIdTransaction(transactionId)).Returns(() => null);

//        //            //Act
//        //            var transaction = _transactionController.DeleteTransaction(transactionId);

//        //            //Assert
//        //            var okResult = transaction.Should().BeOfType<NotFoundObjectResult>().Subject;
//        //        }
//    }
//}