using HCBot.Runner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HCBot.Test
{
    [TestClass]
    public class HCBotTest
    {
        [TestMethod]
        public void LoadMenuTest()
        {
            var currentMenu = BotMenu.LoadFromFile("Structure.json");
            Assert.IsNotNull(currentMenu);
        }

        [TestMethod]
        public void MenuBackNavigation1Test()
        {
            var menu = BotMenu.LoadFromFile("Structure.json");
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1]; //Тренировки для детей
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("/start",prev.Text);
        }

        [TestMethod]
        public void MenuBackNavigation2Test()
        {
            var menu = BotMenu.LoadFromFile("Structure.json");
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1].SubMenu[1]; //Филиал 1
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("Тренировки для детей", prev.Text);
        }
    }
}
