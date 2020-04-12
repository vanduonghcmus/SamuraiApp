create procedure dbo.SamuraisWhoSaidAWord
@text varchar(20) 
as
begin
	select s.Id,s.Name,s.ClanId
	from Samurais as s
	join Quotes on Samurais.Id=SamuraiId
	where(Quotes.Text Like '%' +@text+ '%')
end
go
create procedure dbo.DeleteQuotesForSamurai
@samuraiId int
as
begin
	delete from Quotes
	where Quotes.SamuraiId=@samuraiId
end