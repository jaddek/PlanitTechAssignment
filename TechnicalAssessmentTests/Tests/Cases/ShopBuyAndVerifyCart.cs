using System.Globalization;
using Microsoft.Playwright;
using TechnicalAssessmentTests.Entity;
using TechnicalAssessmentTests.Pages;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 3:
// 1. Buy 2 Stuffed Frog, 5 Fluffy Bunny, 3 Valentine Bear
// 2. Go to the cart page
// 3. Verify the subtotal for each product is correct
// 4. Verify the price for each product
// 5. Verify that total = sum(subtotals)
public class ShopBuyAndVerifyCart : AbstractCase
{
    [Theory(DisplayName = "Cart displays correct subtotals and total")]
    [MemberData(nameof(PurchaseListData))]
    public async Task GivenMultipleItemsAddedToCart_WhenNavigatedToCartPage_ShouldDisplayCorrectSubtotalsAndTotal(
        Dictionary<string, Item> purchaseList)
    {
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
        
        try
        {
            var shopPage = new ShopPage(Page);
            await shopPage.GoToAsync();
            await shopPage.StockItemsCollection.AddToCartAsync(purchaseList);

            await shopPage.MenuComponent.TopRightMenu.CartTab.ClickAsync();

            var cartPage = new CartPage(Page);
            var cartItemsLocator = cartPage.CartItemsCollection.GetItems();

            await Expect(cartItemsLocator).ToHaveCountAsync(purchaseList.Count);

            var subtotalSum = 0m;
            var itemCount = await cartItemsLocator.CountAsync();

            for (var i = 0; i < itemCount; i++)
            {
                var row = cartItemsLocator.Nth(i).Locator("td");

                /*
                    0 - title
                    1 - price
                    2 - quantity
                    3 - subtotal
                 */
                var cellsTextTasks = new[]
                {
                    row.Nth(0).InnerTextAsync(),
                    row.Nth(1).InnerTextAsync(),
                    row.Nth(2).Locator("[ng-model='item.count']").InputValueAsync(),
                    row.Nth(3).InnerTextAsync()
                };

                var cellsTextResults = await Task.WhenAll(cellsTextTasks);

                var title = cellsTextResults[0].Trim();
                var price = ParseCurrency(cellsTextResults[1]);
                var quantity = int.Parse(cellsTextResults[2].Trim());
                var subtotal = ParseCurrency(cellsTextResults[3]);

                Assert.True(purchaseList.TryGetValue(title, out var item), $"Unexpected item: {title}");
                Assert.Equal(item.Qty, quantity);
                Assert.Equal(item.Price, price);

                var expectedSubtotal = price * quantity;
                Assert.Equal(expectedSubtotal, subtotal, precision: 2);

                subtotalSum += subtotal;
            }

            var totalText = await Page.Locator(".total").InnerTextAsync();
            var totalValue = ParseCurrency(totalText.Replace("Total:", ""));
            Assert.Equal(subtotalSum, totalValue, precision: 2);
        }
        catch (Exception)
        {
            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = "tests/ShopBuyAndVerifyCart/GivenMultipleItemsAddedToCart_WhenNavigatedToCartPage_ShouldDisplayCorrectSubtotalsAndTotal.zip"
            });
            throw; 
        }
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

    private static decimal ParseCurrency(string input) =>
        decimal.Parse(input.Replace("$", "").Trim(), CultureInfo.InvariantCulture);
}