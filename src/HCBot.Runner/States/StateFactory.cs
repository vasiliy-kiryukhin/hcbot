using System;
using Microsoft.Extensions.DependencyInjection;


namespace HCBot.Runner.States
{
    public class StateFactory
    {
        IServiceProvider serviceProvider;
        public StateFactory(IServiceProvider sp)
        {
            serviceProvider = sp;
        }
        public UserState CreateState(UserBotState state)
        {
            if (state == UserBotState.SerfMenu)
                return new SerfMenuState(serviceProvider);
            if (state == UserBotState.Enroll)
                return new EnrollTrainigState(serviceProvider);

            throw new NotImplementedException();
        }
    }
}
