﻿#region using directives

using System.Linq;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Mini.Event;
using PoGo.NecroBot.Logic.Mini.State;
using PoGo.NecroBot.Logic.Mini.Utils;

#endregion

namespace PoGo.NecroBot.Logic.Mini.Tasks
{
    public class EvolveSpecificPokemonTask
    {
        public static async Task Execute(ISession session, ulong pokemonId)
        {
            var all = await session.Inventory.GetPokemons();
            var pokemons = all.OrderByDescending(x => x.Cp).ThenBy(n => n.StaminaMax);
            var pokemon = pokemons.FirstOrDefault(p => p.Id == pokemonId);

            if (pokemon == null) return;

            var evolveResponse = await session.Client.Inventory.EvolvePokemon(pokemon.Id);

            session.EventDispatcher.Send(new PokemonEvolveEvent
            {
                Id = pokemon.PokemonId,
                Exp = evolveResponse.ExperienceAwarded,
                Result = evolveResponse.Result
            });
            DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}