using HCBot.Runner.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.States
{
    public class UserStateBag
    {
        public UserBotState UserState {get; set;}
        public BotMenu BotMenu { get; set; }
    }
}
