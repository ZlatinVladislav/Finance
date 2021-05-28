using AutoMapper;
using Finance.Application.Services;
using Finance.Application.ViewModels.Transaction;
using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using Moq;
using System;
using Xunit;

namespace Finance.Tests
{
    public class TransactionServiceTests
    {
        private readonly TransactionService _transactionService;

        private readonly Mock<IBaseRepository<Transaction>> _transactionRepoMock =
            new Mock<IBaseRepository<Transaction>>();

        private readonly Mock<TransactionType> _transactionType =
           new Mock<TransactionType>();

        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public TransactionServiceTests()
        {
            _transactionService = new TransactionService(_transactionRepoMock.Object, _mapper.Object);
        }

        [Fact]
        public void GetByIdTransactionValidResponse()
        {
            //Arrange
            var transactionId = Guid.NewGuid();
            var trasactionTypeId = Guid.NewGuid();
            var transactionDto = new Transaction()
            {
                Id = transactionId,
                Money = 5,
                DateTransaction = DateTime.Now,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId,
            };

            var transactionViewModelDto = new TransactionViewModel()
            {
                Id = transactionId,
                Money = 5,
                TransactionStatus = true,
                TransactionTypeId = trasactionTypeId,
            };

            _transactionRepoMock.Setup(x => x.GetById(transactionId)).Returns(transactionDto);
            _mapper.Setup(x => x.Map<TransactionViewModel>(transactionDto)).Returns(transactionViewModelDto);

            //Act
            TransactionViewModel transaction = _transactionService.GetByIdTransaction(transactionId);

            //Assert
            Assert.Equal(transactionDto.Id, transactionViewModelDto.Id);
            Assert.Equal(transactionDto.Money, transaction.Money);
            Assert.Equal(transactionDto.TransactionStatus, transaction.TransactionStatus);
            Assert.Equal(transactionDto.TransactionTypeId, transaction.TransactionTypeId);
        }
    }
}
