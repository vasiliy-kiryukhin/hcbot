using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.Menu
{
    public interface IMenuProvider
    {
        BotMenu Load();
    }
}
