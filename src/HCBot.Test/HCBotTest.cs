using HCBot.Runner;
using HCBot.Runner.Menu;
using HCBot.Runner.Schedule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1]; //���������� ��� �����
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("/start",prev.Text);
        }

        [TestMethod]
        public void MenuBackNavigation2Test()
        {
            var menu = new FlatFileMenuProvider("Structure.json").Load();
            menu.currentPosition = menu.root.SubMenu[0].SubMenu[1].SubMenu[1]; //������ 1
            var prev = menu.GetPrevPosition();


            Assert.AreEqual<string>("���������� ��� �����", prev.Text);
        }
    }
}
