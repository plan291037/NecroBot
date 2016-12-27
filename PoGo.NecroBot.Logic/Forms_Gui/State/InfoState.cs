﻿#region using directives

using System.Threading;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Forms_Gui.Tasks;

#endregion

namespace PoGo.NecroBot.Logic.Forms_Gui.State
{
    public class InfoState : IState
    {
        public async Task<IState> Execute(ISession session, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await DisplayPokemonStatsTask.Execute(session);
            return new FarmState();
        }
    }
}