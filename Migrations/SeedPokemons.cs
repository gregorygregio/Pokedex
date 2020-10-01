using FluentMigrator;
using System;
using System.Collections.Generic;

namespace Migrations
{
    [Migration(5)]
    public class SeedPokemons : Migration
    {

        string TableName = "pokemons";

     
        public override void Up()
        {

            Insert
                .IntoTable(TableName)
                .Row(new
                {
                    id = Guid.NewGuid().ToString(),
                    pokedex_index = 1,
                    name = "Bulbasaur",
                    hp = 45,
                    attack = 49,
                    defense = 49,
                    special_attack = 65,
                    special_defense = 65,
                    speed = 45,
                    generation = 1
                });
            

        }



        public override void Down()
        {
            Delete.FromTable(TableName);
        }

    }
}
