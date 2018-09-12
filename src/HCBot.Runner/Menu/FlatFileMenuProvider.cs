using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HCBot.Runner.Menu
{
    public class FlatFileMenuProvider : IMenuProvider
    {
        readonly string menuFilePath;
        public FlatFileMenuProvider(string path)
        {
            menuFilePath = path;
        }

        public BotMenu Load()
        {
            var menu = new BotMenu();
            string menuJson = File.ReadAllText(Path.Combine(menuFilePath, "Structure.json"));
            var items = JsonConvert.DeserializeObject<BotMenuItem>(menuJson);
            menu.root = menu.currentPosition = items;
            return menu;
        }
    }
}
