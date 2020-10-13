using FluentMigrator;
using System;
using System.Collections.Generic;

namespace Migrations
{
    [Migration(4)]
    public class SeedTypes : Migration
    {

            string TableName = "types";

        List<string> listOfTypes = new List<string>()
        {
            "Grass", "Fire", "Water", "Bug", "Normal", "Poison", "Electric",
           "Ground", "Fairy", "Fighting", "Psychic", "Rock", "Ghost", "Ice",
           "Dragon", "Dark", "Steel", "Flying"
        };

        public override void Up()
        {

            foreach (var t in listOfTypes)
            {
                Insert
                    .IntoTable(TableName)
                    .Row(new
                    {
                        id = Guid.NewGuid().ToString(),
                        type = t
                    });
            }

        }



        public override void Down()
        {
            Delete.FromTable(TableName);
        }

    }
}
