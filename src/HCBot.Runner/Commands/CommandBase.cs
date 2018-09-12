using HCBot.Runner.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.Commands
{
    public class CommandBase
    {
        public BotMenuItem Position { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
