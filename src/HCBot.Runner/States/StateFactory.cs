using System;
using System.Collections.Generic;
using System.Text;

namespace HCBot.Runner.States
{
    public class StateFactory
    {
        public UserState CreateState(UserBotState state)
        {
            if (state == UserBotState.SerfMenu)
                return new SerfMenuState();
            if (state == UserBotState.Enroll)
                return null;

            throw new NotImplementedException();
        }
    }
}
