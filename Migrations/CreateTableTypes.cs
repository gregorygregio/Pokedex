using FluentMigrator;

namespace Migrations
{
    [Migration(1)]
    public class CreateTableTypes : Migration
    {

        string TableName = "types";
            public override void Up()
            {
                Create.Table(TableName)
                    .WithColumn("id").AsString(36).PrimaryKey()
                    .WithColumn("type").AsString(36).NotNullable().Unique()
                ;


            }



            public override void Down()
            {
                Delete.Table(TableName);
            }

    }
}
