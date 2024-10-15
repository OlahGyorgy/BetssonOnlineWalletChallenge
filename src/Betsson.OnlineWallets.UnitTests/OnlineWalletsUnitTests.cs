using Betsson.OnlineWallets.Data.Models;
using Betsson.OnlineWallets.Data.Repositories;
using Betsson.OnlineWallets.Models;
using Betsson.OnlineWallets.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Exceptions;

namespace Betsson.OnlineWallets.UnitTests
{
    [TestFixture]
    public class OnlineWalletsUnitTests
    {
        private IOnlineWalletService _onlineWalletService;
        private Mock<IOnlineWalletRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IOnlineWalletRepository>();
            _onlineWalletService = new OnlineWalletService(_mockRepository.Object);
        }

        private void SetupMockRepository(OnlineWalletEntry? onlineWalletEntry)
        {
            _mockRepository.Setup(mock => mock.GetLastOnlineWalletEntryAsync())
                           .ReturnsAsync(onlineWalletEntry);
        }

        private void VerifyRepositoryInsert(OnlineWalletEntry onlineWalletEntry, decimal amount)
        {
            _mockRepository.Verify(mock => mock.InsertOnlineWalletEntryAsync(It.Is<OnlineWalletEntry>(entry =>
                entry.Amount == amount &&
                entry.BalanceBefore == onlineWalletEntry.BalanceBefore &&
                entry.EventTime <= DateTimeOffset.UtcNow
            )), Times.Once);
        }

        [Test]
        public async Task TestGetBalanceAsync_OnlineWalletEntryIsDefault_ReturnsZero()
        {
            // Given
            SetupMockRepository(null);

            // When
            Balance balance = await _onlineWalletService.GetBalanceAsync();

            // Then
            Assert.That(balance.Amount, Is.Zero);
        }

        [Test]
        public async Task TestGetBalanceAsync_OnlineWalletEntryIsNotDefault()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = 12
            };
            SetupMockRepository(onlineWalletEntry);

            // When
            Balance balance = await _onlineWalletService.GetBalanceAsync();

            // Then
            Assert.That(balance.Amount, Is.EqualTo(12));
        }

        [Test]
        public async Task TestDepositFundsAsync_DepositFunds()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = 200
            };
            SetupMockRepository(onlineWalletEntry);

            var deposit = new Deposit { Amount = 150 };

            // When
            Balance newBalance = await _onlineWalletService.DepositFundsAsync(deposit);

            // Then
            decimal expectedNewBalance = onlineWalletEntry.BalanceBefore + deposit.Amount;
            Assert.That(newBalance.Amount, Is.EqualTo(expectedNewBalance));
            
            VerifyRepositoryInsert(onlineWalletEntry, deposit.Amount);
        }
        
        [Test]
        public async Task TestDepositFundsAsync_DepositMaximumAmount()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = decimal.MaxValue - 1000 
            };
            SetupMockRepository(onlineWalletEntry);

            var deposit = new Deposit { Amount = 1000 };

            // When
            Balance newBalance = await _onlineWalletService.DepositFundsAsync(deposit);

            // Then
            decimal expectedNewBalance = onlineWalletEntry.BalanceBefore + deposit.Amount;
            Assert.That(newBalance.Amount, Is.EqualTo(expectedNewBalance));
            
            VerifyRepositoryInsert(onlineWalletEntry, deposit.Amount);
        }

        [Test]
        public async Task TestDepositFundsAsync_DepositAboveMaximumFunds_ThrowsException()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = decimal.MaxValue
            };
            SetupMockRepository(onlineWalletEntry);

            var deposit = new Deposit { Amount = 1 };

            // When & Then
            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _onlineWalletService.DepositFundsAsync(deposit)
            );

            Assert.That(exception.Message, Is.EqualTo("The deposit would exceed the maximum allowable balance."));
        }
        
        [Test]
        public async Task TestWithdrawFundsAsync_WithdrawFunds()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = 500 
            };
            SetupMockRepository(onlineWalletEntry);

            var withdrawal = new Withdrawal { Amount = 100 }; 

            // When
            Balance newBalance = await _onlineWalletService.WithdrawFundsAsync(withdrawal);

            // Then
            decimal expectedNewBalance = onlineWalletEntry.BalanceBefore - withdrawal.Amount;
            Assert.That(newBalance.Amount, Is.EqualTo(expectedNewBalance));
            
            VerifyRepositoryInsert(onlineWalletEntry, -withdrawal.Amount);
        }

        [Test]
        public void TestWithdrawFundsAsync_WithdrawMoreThanBalance_ThrowsInsufficientBalanceException()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = 50 
            };
            SetupMockRepository(onlineWalletEntry);

            var withdrawal = new Withdrawal { Amount = 100 }; 

            // When & Then
            var exception = Assert.ThrowsAsync<InsufficientBalanceException>(
                async () => await _onlineWalletService.WithdrawFundsAsync(withdrawal)
            );
            
            Assert.That(exception, Is.TypeOf<InsufficientBalanceException>());

            _mockRepository.Verify(mock => mock.InsertOnlineWalletEntryAsync(It.IsAny<OnlineWalletEntry>()), Times.Never);
        }

        [Test]
        public async Task TestWithdrawFundsAsync_WithdrawAllFunds()
        {
            // Given
            var onlineWalletEntry = new OnlineWalletEntry
            {
                Amount = 0,
                BalanceBefore = 200 
            };
            SetupMockRepository(onlineWalletEntry);

            var withdrawal = new Withdrawal { Amount = 200 }; 

            // When
            Balance newBalance = await _onlineWalletService.WithdrawFundsAsync(withdrawal);

            // Then
            Assert.That(newBalance.Amount, Is.Zero);
            
            VerifyRepositoryInsert(onlineWalletEntry, -withdrawal.Amount); 
        }
    }
}
