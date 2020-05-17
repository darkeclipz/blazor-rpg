using DungeonRpg.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonRpg.Services
{
    public class RaceService : Service<Race>
    {
        public override void InitializeInitialData()
        {
            if (Entities.Count == 0)
            {
                Entities.Add(new Race
                {
                    Id = Guid.NewGuid(),
                    Name = "Human",
                    MaleTileId = 1954,
                    FemaleTileId = 1953
                });

                Entities.Add(new Race
                {
                    Id = Guid.NewGuid(),
                    Name = "High Elf",
                    MaleTileId = 1998,
                    FemaleTileId = 1997
                });

                Entities.Add(new Race
                {
                    Id = Guid.NewGuid(),
                    Name = "Orc",
                    MaleTileId = 2023,
                    FemaleTileId = 2022
                });

                Entities.Add(new Race
                {
                    Id = Guid.NewGuid(),
                    Name = "Draconian",
                    MaleTileId = 2021,
                    FemaleTileId = 2020
                });
            }
        }
    }
}
