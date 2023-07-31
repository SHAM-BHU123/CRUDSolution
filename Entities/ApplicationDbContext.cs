
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base
        (dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Person>().ToTable("Persons");

            /*Seed data to Countries */

            string countryjson = File.ReadAllText("countries.json");
            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countryjson);

            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }



            /*Seed data to Persons */

            string personjson = File.ReadAllText("persons.json");
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personjson);

            foreach (Person person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }


            /* Fluent API part-1*/
           /* modelBuilder.Entity<Person>().Property(temp => temp.TIN)
           .HasColumnName("TaxIdentificationNumber")
           .HasColumnType("varchar(8)")
           .HasDefaultValue("ABC123");*/

            /*Fluent API part-2*/
            /*modelBuilder.Entity<P\erson>().HasIndex(temp => temp.TIN).IsUnique();*/
/*
            modelBuilder.Entity<Person>().
            HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber])=8");*/


            /* Table Relations*/
            /* modelBuilder.Entity<Person>(entity =>
             {
                 entity.HasOne<Country>(c => c.Country)
                 .WithMany(p => p.Persons)
                 .HasForeignKey(p => p.CountryID);
             });*/
        }


        public List<Person> StoredProcedureGetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]")
           .ToList();
        }

        public async Task<int> StoreProcedureInsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonID",person.PersonID),
                new SqlParameter("@PersonName",person.PersonName),
                new SqlParameter("@Email",person.Email),
                new SqlParameter("@DateOfBirth",person.DateOfBirth),
                new SqlParameter("@Gender",person.Gender),
                new SqlParameter("@CountryID",person.CountryID),
                new SqlParameter("@Address",person.Address),
                new SqlParameter("@ReceiveNewsLetters",person.ReceiveNewsLetters)

            };

            return await Database.ExecuteSqlRawAsync("EXECUTE [dbo].[InsertPerson] @PersonID,@PersonName,@Email,@DateOfBirth,@Gender,@CountryID,@Address,@ReceiveNewsLetters", parameters);

        }

        public int StoreProcedureUpdatePerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonID",person.PersonID),
                new SqlParameter("@PersonName",person.PersonName),
                new SqlParameter("@Email",person.Email),
                new SqlParameter("@DateOfBirth",person.DateOfBirth),
                new SqlParameter("@Gender",person.Gender),
                new SqlParameter("@CountryID",person.CountryID),
                new SqlParameter("@Address",person.Address),
                new SqlParameter("@ReceiveNewsLetters",person.ReceiveNewsLetters)
            };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[UpdatePerson] @PersonID,@PersonName,@Email,@DateOfBirth,@Gender,@CountryID,@Address,@ReceiveNewsLetters", parameters);
        }

        public int StoreProcedureDeletePerson(Guid? PersonID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonID",PersonID),

            };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[DeletePerson] @PersonID", parameters);
        }
    }
}
