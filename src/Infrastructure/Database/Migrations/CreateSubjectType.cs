using FluentMigrator;

namespace Infrastructure.Database.Migrations;

[Migration(1, description: "Create subject_type enum")]
public class CreateSubjectType : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TYPE subject_type AS ENUM (
                        'driver',
                        'passenger',
                        'unknown'
                    );
                    """);
    }

    public override void Down()
    {
        Execute.Sql("""
                    DROP TYPE IF EXISTS subject_type;
                    """);
    }
}