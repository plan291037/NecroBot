﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Mini.Common;
using PoGo.NecroBot.Logic.Mini.Logging;
using PoGo.NecroBot.Logic.Mini.State;

namespace PoGo.NecroBot.Logic.Mini.Tasks
{
    internal class GetPokeDexCount
    {
        public static async Task Execute(ISession session, CancellationToken cancellationToken)
        {
            var PokeDex = await session.Inventory.GetPokeDexItems();
            var _totalUniqueEncounters =
                PokeDex.Select(
                    i =>
                        new
                        {
                            Pokemon = i.InventoryItemData.PokedexEntry.PokemonId,
                            Captures = i.InventoryItemData.PokedexEntry.TimesCaptured
                        });
            var _totalCaptures = _totalUniqueEncounters.Count(i => i.Captures > 0);
            var _totalData = PokeDex.Count();

            Logger.Write(session.Translation.GetTranslation(TranslationString.AmountPkmSeenCaught, _totalData,
                _totalCaptures));
        }
    }
}