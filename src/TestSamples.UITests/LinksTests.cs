using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Selenium.Core;

namespace TestSamples.UITests
{
    [TestClass]
    public class LinksTests : SeleniumTest
    {
        [TestMethod]
        public void Control_DotvvmRouteLink()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("dot-links");
                browser.Wait();

                var links = browser.FindElements("a");
                links.ThrowIfDifferentCountThan(6);

                links[0].CheckAttribute("href", a => a.EndsWith("/asp-links-test"));
                links[1].CheckAttribute("href", a => a.EndsWith("/asp-links-test/15/title-15"));
                links[2].CheckAttribute("href", a => a.EndsWith("/asp-links-test/20/title-20"));
                links[3].CheckAttribute("href", a => a.EndsWith("/asp-links-test/25"));
                links[4].CheckAttribute("href", a => a.EndsWith("/asp-links-test/10/title-10"));
                links[5].CheckAttribute("href", a => a.EndsWith("/asp-links-test/40/title-40"));
            });
        }

        [TestMethod]
        public void Control_WebFormsRouteLink()
        {
            this.RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("asp-links");
                browser.Wait();

                var links = browser.FindElements("a");
                links.ThrowIfDifferentCountThan(5);

                links[0].CheckAttribute("href", a => a.EndsWith("/dot-links-test"));
                links[1].CheckAttribute("href", a => a.EndsWith("/dot-links-test/15/title-15"));
                links[2].CheckAttribute("href", a => a.EndsWith("/dot-links-test/25/default"));
                links[3].CheckAttribute("href", a => a.EndsWith("/dot-links-test/10/title-10"));
                links[4].CheckAttribute("href", a => a.EndsWith("/dot-links-test/40/title-40"));
            });
        }
    }
}
