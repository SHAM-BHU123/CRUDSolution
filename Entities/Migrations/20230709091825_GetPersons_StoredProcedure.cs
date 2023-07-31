using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class GetPersons_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string getAllPersonsProcedure = @"
                
                    CREATE PROCEDURE [dbo].[GetAllPersons]
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
                
                   
             ";

            migrationBuilder.Sql(getAllPersonsProcedure);
        }
    }
}
