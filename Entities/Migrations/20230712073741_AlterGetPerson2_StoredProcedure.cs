using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class AlterGetPerson2_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string getAllPersonsProcedure = @"
                
                    ALTER PROCEDURE [dbo].[GetAllPersons]
                    AS BEGIN
                    SELECT PersonID,PersonName,Email,DateOfBirth,Gender,CountryID,Address,ReceiveNewsLetters FROM [dbo].
                    [Persons]
                    END
             ";

            migrationBuilder.Sql(getAllPersonsProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string getAllPersonsProcedure = @"
                
                    DROP PROCEDURE [dbo].[GetAllPersons]
                   
             ";

            migrationBuilder.Sql(getAllPersonsProcedure);
        }
    }
}
