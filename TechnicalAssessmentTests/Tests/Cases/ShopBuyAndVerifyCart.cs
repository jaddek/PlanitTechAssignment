using TechnicalAssessmentTests.Entity;
using TechnicalAssessmentTests.Pages;
using Xunit.Abstractions;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 3:
// 1. Buy 2 Stuffed Frog, 5 Fluffy Bunny, 3 Valentine Bear
// 2. Go to the cart page
// 3. Verify the subtotal for each product is correct
// 4. Verify the price for each product
// 5. Verify that total = sum(subtotals)
[Collection("Cart tests")]
[Trait("Group", "CartCheckout")]
public class ShopBuyAndVerifyCart(ITestOutputHelper output) : AbstractCase(output)
{
    [Theory(DisplayName = "Cart displays correct subtotals and total")]
    [MemberData(nameof(PurchaseListData))]
    public async Task GivenMultipleItemsAddedToCart_WhenNavigatedToCartPage_ShouldDisplayCorrectSubtotalsAndTotal(
        Dictionary<string, Item> purchaseList)
    {
        var shopPageScenarioManager = new ShopPageAssertScenarioManager(new ShopPage(Page));
        await shopPageScenarioManager.GoToShopPageAndAddToCartAsync(purchaseList);


        var cartPageScenarioManager = new CartPageAssertScenarioManager(new CartPage(Page));
        await cartPageScenarioManager.CartPageValidateDataAsync(purchaseList);
    }

    public static IEnumerable<object[]> PurchaseListData =>
    [
        [
            new Dictionary<string, Item>
            {
                { "Stuffed Frog", new Item(title: "Stuffed Frog", qty: 2) },
                { "Fluffy Bunny", new Item(title: "Fluffy Bunny", qty: 5) },
                { "Valentine Bear", new Item(title: "Valentine Bear", qty: 3) }
            }
        ]
    ];
}