using HCBot.Runner;
using HCBot.Runner.Menu;
using HCBot.Runner.Schedule;
using HCBot.Runner.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HCBot.Test
{
    [TestClass]
    public class HCBotTest
    {
        [TestMethod]
        public void LoadMenuTest()
        {
            var menu = new FlatFileMenuProvider("Structure.json").Load();
            Assert.IsNotNull(menu);
        }

        [TestMethod]
        public void MenuBackNavigation1Test()
        {
            var menu = new FlatFileMenuProvider("Structure.json").Load();
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1]; //Тренировки для детей
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("/start",prev.Text);
        }

        [TestMethod]
        public void MenuBackNavigation2Test()
        {
            var menu = new FlatFileMenuProvider("Structure.json").Load();
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1].SubMenu[1]; //Филиал 1
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("Тренировки для детей", prev.Text);
        }

        [TestMethod]
        public void TestGetMskToday()
        {
            var utc = DateTime.UtcNow;
            var msk = DateTimeHelper.UtcToMsk(utc);
            Assert.AreEqual(3, msk.Subtract(utc).Hours);
        }
    }
}
