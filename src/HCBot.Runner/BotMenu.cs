using HCBot.Runner.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace HCBot.Runner
{

    public class BotMenu
    {
        public BotMenuItem currentPosition;
        public BotMenuItem root;

        public BotMenuItem GetPrevPosition()
        {
            return GetPrevPosition(root, root);
        }
        private BotMenuItem GetPrevPosition(BotMenuItem position, BotMenuItem prev)
        {
            if (currentPosition == position)
                return prev;

            if (position.SubMenu != null)
            {
                foreach (var m in position.SubMenu)
                {
                    var r = GetPrevPosition(m, position);
                    if (r != null)
                        return r;
                }
            }
            return null;
        }

        public static BotMenu LoadFromFile(string path)
        {
            var menu = new BotMenu();
            string menuJson = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<BotMenuItem>(menuJson);
            menu.root = menu.currentPosition = items;
            return menu;
        }

        public ICommand GetCommand(string commandName)
        {
            if (commandName == "Вернуться")
                return new GoBackMenuCommand();

            var cmdPosition = currentPosition?.SubMenu?.FirstOrDefault(m => m.Text == commandName);

            if (cmdPosition == null)
                return new StartCommand();

            var cn = cmdPosition?.CommandName;
            
            var commandType = Type.GetType("HCBot.Runner.Commands." + cn + "Command") ?? Type.GetType("HCBot.Runner.Commands.ShowMenuCommand");
            var command = (ICommand)Activator.CreateInstance(commandType);
            command.Position = cmdPosition;

            return command;
        }
    }

    public class BotMenuItem
    {
        [JsonProperty(propertyName: "Command")]
        public string CommandName;

        [JsonProperty(propertyName:"Text")]
        public string Text;

        [JsonProperty(propertyName: "NextMenu")]
        public BotMenuItem[] SubMenu;

        [JsonProperty(propertyName: "Url")]
        public string[] Urls;
    }
}
