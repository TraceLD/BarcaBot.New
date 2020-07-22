﻿using System.Collections.Generic;
using System.Linq;
using BarcaBot.Core.Models.Dto.ApiFootball;
using BarcaBot.Core.Models.Player;

namespace BarcaBot.Infrastructure.Extensions
{
    public static class ConvertExtensions
    {
        public static IEnumerable<Player> AsPlayers(this IEnumerable<PlayerDto> playerDtos)
            => playerDtos.GroupBy(x => x.PlayerId)
                .Select(y => new Player
                {
                    Id = y.Key,
                    Name = Utils.NormalizePlayerName(y.First().PlayerName),
                    Number = y.First().Number,
                    Position = y.First().Position,
                    Age = y.First().Age,
                    Nationality = y.First().Nationality,
                    Height = y.First().Height,
                    Weight = y.First().Weight,
                    Statistics = new Statistics
                    {
                        Rating = y.Average(x => x.Rating),
                        Shots = new Shots
                        {
                            Total = y.Sum(x => x.Shots.Total),
                            OnTarget = y.Sum(x => x.Shots.OnTarget)
                        },
                        Goals = new Goals
                        {
                            Total = y.Sum(x => x.Goals.Total),
                            Conceded = y.Sum(x => x.Goals.Conceded),
                            Assists = y.Sum(x => x.Goals.Assists)
                        },
                        Passes = new Passes
                        {
                            Total = y.Sum(x => x.Passes.Total),
                            Key = y.Sum(x => x.Passes.Key),
                            Accuracy = y.Average(x => x.Passes.Accuracy)
                        },
                        Tackles = new Tackles
                        {
                            Total = y.Sum(x => x.Tackles.Total),
                            Blocks = y.Sum(x => x.Tackles.Blocks),
                            Interceptions = y.Sum(x => x.Tackles.Interceptions)
                        },
                        Duels = new Duels
                        {
                            Total = y.Sum(x => x.Duels.Total),
                            Won = y.Sum(x => x.Duels.Won)
                        },
                        Dribbles = new Dribbles
                        {
                            Attempts = y.Sum(x => x.Dribbles.Attempts),
                            Success = y.Sum(x => x.Dribbles.Success)
                        },
                        Fouls = new Fouls
                        {
                            Committed = y.Sum(x => x.Fouls.Committed),
                            Drawn = y.Sum(x => x.Fouls.Drawn)
                        },
                        Cards = new Cards
                        {
                            Yellow = y.Sum(x => x.Cards.Yellow),
                            YellowRed = y.Sum(x => x.Cards.YellowRed),
                            Red = y.Sum(x => x.Cards.Red)
                        },
                        Penalties = new Penalties
                        {
                            Won = y.Sum(x => x.Penalties.Won),
                            Committed = y.Sum(x => x.Penalties.Committed),
                            Success = y.Sum(x => x.Penalties.Success),
                            Missed = y.Sum(x => x.Penalties.Missed),
                            Saved = y.Sum(x => x.Penalties.Saved)
                        },
                        Games = new Games
                        {
                            Appearances = y.Sum(x => x.Games.Appearances),
                            MinutesPlayed = y.Sum(x => x.Games.MinutesPlayed),
                            StartingEleven = y.Sum(x => x.Games.StartingEleven)
                        },
                        Substitutes = new Substitutes
                        {
                            In = y.Sum(x => x.Substitutes.In),
                            Out = y.Sum(x => x.Substitutes.Out),
                            Bench = y.Sum(x => x.Substitutes.Bench)
                        }
                    }
                });
    }
}