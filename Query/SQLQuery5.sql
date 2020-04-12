create function [dbo].[EarliestBattleFougthBySamurai](@samuraiId int)
returns char(30) as 
begin 
	declare @ref char(30)
	select Top(1) @ref=Name from dbo.Battles 
	where Battles.Id 
	in (select BattleId  from dbo.SamuraiBattles where SamuraiId=@samuraiId)
	order by StartDate
	return @ref
end