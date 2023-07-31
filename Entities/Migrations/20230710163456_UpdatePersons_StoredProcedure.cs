using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class UpdatePersons_StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_UpdatePerson = @"
            CREATE PROCEDURE [dbo].[UpdatePerson]
            (@PersonID uniqueidentifier, @PersonName nvarchar(50),
            @Email nvarchar(50),@DateOfBirth datetime2(7),@Gender nvarchar(10),
            @CountryID uniqueidentifier,@Address nvarchar(200),@ReceiveNewsLetters bit)

            AS BEGIN
            UPDATE [dbo].[Persons]
            SET
                PersonName = @PersonName,
                Email = @Email,
                DateOfBirth = @DateOfBirth,
                Gender = @Gender,
                CountryID = @CountryID,
                Address = @Address,
                ReceiveNewsLetters = @ReceiveNewsLetters
            WHERE
                PersonID = @PersonID;
             END
         ";

            migrationBuilder.Sql(sp_UpdatePerson);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_UpdatePerson = @"
            DROP PROCEDURE [dbo].[UpdatePerson]
           ";

            migrationBuilder.Sql(sp_UpdatePerson);
        }


       
    }
  
}
