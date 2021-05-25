// using System;
// using System.Collections.Generic;
// using System.Linq;
// using AutoMapper;
// using Finance.Application.Interfaces;
// using Finance.Application.Services;
// using Finance.Application.ViewModels.Transaction;
// using Finance.Domain.Interfaces.Base;
// using Finance.Domain.Models;
// using Finance.Infrastructure.Data.Interfaces.Base;
// using Moq;
// using Xunit;
//
// namespace Finance.Unit.Test.Services
// {
//     public class TransactionServiceTests
//     {
//         private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
//
//         private readonly Mock<IBaseRepository<Transaction>> _transactionRepoMock =
//             new Mock<IBaseRepository<Transaction>>();
//
//         private readonly ITransactionService _transactionService;
//
//         public TransactionServiceTests()
//         {
//             _transactionService = new TransactionService(_transactionRepoMock.Object, _mapper.Object);
//         }
//
//         [Fact]
//         public void GetByIdTransactionValidResponse()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionViewModel
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.GetById(transactionId)).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);
//
//             //Act
//             var transaction = _transactionService.GetByIdTransaction(transactionId);
//
//             //Assert
//             Assert.Equal(transactionDto.Id, transactionViewModelDto.Id);
//             Assert.Equal(transactionDto.Money, transaction.Result.Money);
//             Assert.Equal(transactionDto.TransactionStatus, transaction.Result.TransactionStatus);
//             Assert.Equal(transactionDto.TransactionTypeId, transaction.Result.TransactionTypeId);
//         }
//
//         [Fact]
//         public void GetByIdTransactionWrongID()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionViewModel
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.GetById(transactionId)).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);
//
//             //Act
//             var transaction = _transactionService.GetByIdTransaction(transactionId);
//
//             //Assert
//             Assert.NotEqual(transactionDto.Id, transactionViewModelDto.Id);
//             Assert.Equal(transactionDto.Money, transaction.Result.Money);
//             Assert.Equal(transactionDto.TransactionStatus, transaction.Result.TransactionStatus);
//             Assert.Equal(transactionDto.TransactionTypeId, transaction.Result.TransactionTypeId);
//         }
//
//         [Fact]
//         public void GetByIdTransactionMissingMoney()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionViewModel
//             {
//                 Id = transactionId,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.GetById(transactionId)).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);
//
//             //Act
//             var transactionViewModel = _transactionService.GetByIdTransaction(transactionId);
//
//             //Assert
//             Assert.Equal(transactionDto.Id, transactionViewModelDto.Id);
//             Console.WriteLine(transactionViewModel.Result.Money);
//             Assert.True(transactionViewModel.Result.Money == null);
//             Assert.Equal(transactionDto.TransactionStatus, transactionViewModel.Result.TransactionStatus);
//             Assert.Equal(transactionDto.TransactionTypeId, transactionViewModel.Result.TransactionTypeId);
//         }
//
//         [Fact]
//         public void GetAllTransaction()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new TransactionList<Transaction>
//             {
//                 new Transaction
//                 {
//                     Id = transactionId,
//                     Money = 5,
//                     DateTransaction = DateTime.Now,
//                     TransactionStatus = true,
//                     TransactionTypeId = trasactionTypeId
//                 }
//             };
//
//             var transactionViewModelDto = new TransactionList<TransactionViewModel>
//             {
//                 new TransactionViewModel
//                 {
//                     Id = transactionId,
//                     Money = 5,
//                     TransactionStatus = true,
//                     TransactionTypeId = trasactionTypeId
//                 }
//             };
//
//             _transactionRepoMock.Setup(x => x.GetAll()).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<IEnumerable<TransactionViewModel>>(transactionDto))
//                 .Returns(transactionViewModelDto);
//
//             //Act
//             var transactionViewModel = _transactionService.GetAllTransactions();
//
//             //Assert
//             Assert.Equal(transactionDto.Count(), transactionViewModelDto.Count());
//         }
//
//         [Fact]
//         public void GetAllTransactionInvalid()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new TransactionList<Transaction>
//             {
//                 new Transaction
//                 {
//                     Id = transactionId,
//                     Money = 5,
//                     DateTransaction = DateTime.Now,
//                     TransactionStatus = true,
//                     TransactionTypeId = trasactionTypeId
//                 }
//             };
//
//             var transactionViewModelDto = new TransactionList<TransactionViewModel>();
//
//             _transactionRepoMock.Setup(x => x.GetAll()).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<IEnumerable<TransactionViewModel>>(transactionDto))
//                 .Returns(transactionViewModelDto);
//
//             //Act
//             var transactionViewModel = _transactionService.GetAllTransactions();
//
//             //Assert
//             Assert.NotEqual(transactionDto.Count(), transactionViewModelDto.Count());
//         }
//
//         [Fact]
//         public void AddTransaction()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionViewModel
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.Post(transactionDto)).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);
//             _mapper.Setup(x => x.Map<Transaction>(transactionViewModelDto)).Returns(transactionDto);
//
//             //Act
//             var transactionViewModel = _transactionService.AddTransaction(transactionViewModelDto);
//
//             //Assert
//             Assert.Equal(transactionDto.Id, transactionViewModelDto.Id);
//             Assert.Equal(transactionDto.Money, transactionViewModelDto.Money);
//             Assert.Equal(transactionDto.TransactionStatus, transactionViewModel.Result.TransactionStatus);
//             Assert.Equal(transactionDto.TransactionTypeId, transactionViewModel.Result.TransactionTypeId);
//         }
//
//         [Fact]
//         public void AddTransactionInvalid()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionViewModel
//             {
//                 Money = 8,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.Post(transactionDto)).ReturnsAsync(transactionDto);
//             _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);
//             _mapper.Setup(x => x.Map<Transaction>(transactionViewModelDto)).Returns(transactionDto);
//
//             //Act
//             var transactionViewModel = _transactionService.AddTransaction(transactionViewModelDto);
//
//             //Assert
//             Assert.NotEqual(transactionDto.Id, transactionViewModelDto.Id);
//             Assert.NotEqual(transactionDto.Money, transactionViewModelDto.Money);
//         }
//
//         [Fact]
//         public void EditTransaction()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             var transactionViewModelDto = new TransactionEditViewModel
//             {
//                 Id = transactionId,
//                 Money = 5
//             };
//
//             _mapper.Setup(x => x.Map<Transaction>(transactionViewModelDto)).Returns(transactionDto);
//
//             //Act
//             _transactionService.EditTransaction(transactionViewModelDto);
//
//             //Assert
//             Assert.Equal(1, 1);
//         }
//
//         [Fact]
//         public void DeleteTransaction()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//             var trasactionTypeId = Guid.NewGuid();
//             var transactionDto = new Transaction
//             {
//                 Id = transactionId,
//                 Money = 5,
//                 DateTransaction = DateTime.Now,
//                 TransactionStatus = true,
//                 TransactionTypeId = trasactionTypeId
//             };
//
//             _transactionRepoMock.Setup(x => x.GetById(transactionId)).ReturnsAsync(transactionDto);
//
//             //Act
//             var transactionStatus = _transactionService.DeleteTransaction(transactionId);
//
//             //Assert
//             Assert.True(transactionStatus.Result);
//         }
//
//         [Fact]
//         public void DeleteTransactionInvalid()
//         {
//             //Arrange
//             var transactionId = Guid.NewGuid();
//
//             //Act
//             var transactionStatus = _transactionService.DeleteTransaction(transactionId);
//
//             //Assert
//             Assert.False(transactionStatus.Result);
//         }
//     }
// }