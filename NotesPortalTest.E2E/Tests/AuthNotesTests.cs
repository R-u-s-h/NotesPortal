using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NotesPortalTest.E2E.Common;
using NotesPortalTest.E2E.Selectors.Notes;
using SeleniumExtras.WaitHelpers;

namespace NotesPortalTest.E2E.Tests;

public class AuthNotesTests
{
    private IWebDriver _webDriver = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _webDriver = new ChromeDriver();
    }

    [Test]
    public void Login()
    {
        _webDriver.Url = $"{Constants.NOTES_BASE_URL}";

        _webDriver
            .FindElement(LayoutNotes.LoginLink)
            .Click();

        _webDriver
            .FindElement(LoginPageNotes.UserNameInput)
            .SendKeys(TestNotesConfiguration.TestAdminLogin);

        _webDriver
            .FindElement(LoginPageNotes.PasswordInput)
            .SendKeys(TestNotesConfiguration.TestAdminPassword);

        _webDriver
            .FindElement(LoginPageNotes.LoginButton)
            .Click();

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));
        var greetingsText = wait
            .Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".nickname")))
            .Text;

        Assert.That(greetingsText.Contains(TestNotesConfiguration.TestAdminLogin));
    }

    [OneTimeTearDown]
    public void Tear()
    {
        _webDriver.Close();
    }
}

