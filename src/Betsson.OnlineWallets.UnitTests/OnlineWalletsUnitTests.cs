using Betsson.OnlineWallets.Data.Models;
using Betsson.OnlineWallets.Data.Repositories;
using Betsson.OnlineWallets.Models;
using Betsson.OnlineWallets.Services;
using Moq;
using NUnit.Framework;

namespace Betsson.OnlineWallets.UnitTests;

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
    

    [Test]
    public async Task TestGetBalanceAsync_OnlineWalletEntryIsDefault_ReturnsZero()
    {
       
        _mockRepository.Setup(mock => mock.GetLastOnlineWalletEntryAsync()).ReturnsAsync((OnlineWalletEntry?)null);
        
        Balance balance = await _onlineWalletService.GetBalanceAsync();

       
        Assert.That(balance.Amount, Is.Zero);
    }
    
    [Test]
    public async Task TestGetBalanceAsync_OnlineWalletEntryIsDefault_()
    {
        var onlineWalletEntry = new OnlineWalletEntry
        {
            Amount = 12,        
            BalanceBefore = 12
        };
       
        _mockRepository.Setup(mock => mock.GetLastOnlineWalletEntryAsync()).ReturnsAsync(onlineWalletEntry);
        
        Balance balance = await _onlineWalletService.GetBalanceAsync();

       
        Assert.That(balance.Amount.Equals(24));
    }
    
}