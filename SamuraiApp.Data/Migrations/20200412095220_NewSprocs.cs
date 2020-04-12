using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class NewSprocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"alter procedure dbo.SamuraisWhoSaidAWord
                (@text varchar(20)) 
                as
                begin
	                select  *
	                from    Samurais inner join
	                        Quotes on Samurais.Id = Quotes.SamuraiId
	                WHERE   (Quotes.Text Like '%' +@text+ '%')
                end
                "
                );
            migrationBuilder.Sql(
                @"create procedure dbo.DeleteQuotesForSamurai
                (@samuraiId int)
                as
                begin
	                delete from Quotes
	                where Quotes.SamuraiId = @samuraiId
                end"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
