using Microsoft.Playwright;
using TechnicalAssessmentTests.Components.Collection;
using TechnicalAssessmentTests.Entity;
using static Microsoft.Playwright.Assertions;
using System.Globalization;


namespace TechnicalAssessmentTests.Pages;

public sealed class CartPage(IPage page) : AbstractPage(page)
{
    protected override string Fragment => "/#/cart";

    public CartItemsCollection CartItemsCollection { get; } = new(page);
}

public class CartPageAssertScenarioManager(CartPage page)
{
    public async Task CartPageValidateDataAsync(
        Dictionary<string, Item> purchaseList
    )
    {
        var cartItemsLocator = page.CartItemsCollection.Node;
        await Expect(cartItemsLocator).ToHaveCountAsync(purchaseList.Count, new() { Timeout = 10000 });

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

        var totalText = await page.CartItemsCollection.Total.InnerTextAsync();
        var totalValue = ParseCurrency(totalText.Replace("Total:", ""));
        Assert.Equal(subtotalSum, totalValue, precision: 2);
    }
    
    private static decimal ParseCurrency(string input) =>
        decimal.Parse(input.Replace("$", "").Trim(), CultureInfo.InvariantCulture);
}
