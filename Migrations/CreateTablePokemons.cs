using FluentMigrator;

namespace Migrations
{
    [Migration(2)]
    public class CreateTablePokemons : Migration
    {

        string TableName = "pokemons";
            public override void Up()
            {
                Create.Table(TableName)
                    .WithColumn("id").AsString(36).PrimaryKey()
                    .WithColumn("pokedex_index").AsInt32().NotNullable().Unique()
                    .WithColumn("name").AsString(26).NotNullable().Unique()
                    .WithColumn("hp").AsInt32().NotNullable()
                    .WithColumn("attack").AsInt32().NotNullable()
                    .WithColumn("defense").AsInt32().NotNullable()
                    .WithColumn("special_attack").AsInt32()
                    .WithColumn("special_defense").AsInt32()
                    .WithColumn("speed").AsInt32().NotNullable()
                    .WithColumn("generation").AsInt32().NotNullable()
                ;


            }



            public override void Down()
            {
                Delete.Table(TableName);
            }

    }
}
