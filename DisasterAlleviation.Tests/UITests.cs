using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class UITests
{
    [Fact]
    public void HomePage_ShouldDisplayWelcomeText()
    {
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://localhost:5001");
        Assert.Contains("Welcome", driver.PageSource);
    }
}
