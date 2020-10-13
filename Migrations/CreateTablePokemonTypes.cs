using FluentMigrator;

namespace Migrations
{
    [Migration(3)]
    public class CreateTablePokemonTypes : Migration
    {

        string TableName = "pokemon_types";
            public override void Up()
            {
                Create.Table(TableName)
                    .WithColumn("id").AsString(36).PrimaryKey()
                    .WithColumn("pokemon_id").AsString(36).NotNullable()
                    .WithColumn("type_id").AsString(36).NotNullable()
                ;



                Create.ForeignKey()
                .FromTable(TableName).ForeignColumn("pokemon_id")
                .ToTable("pokemons").PrimaryColumn("id");


                Create.ForeignKey()
                .FromTable(TableName).ForeignColumn("type_id")
                .ToTable("types").PrimaryColumn("id");

            }



        public override void Down()
            {
                Delete.Table(TableName);
            }

    }
}
