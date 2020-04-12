create function [dbo].[EarliestBattleFoundBySamurai](@samuraiId int)
returns char(30) as
begin
	declare @ret char(30)
	select Top(1) @ret=Name
	from Battles
	where Battles.Id in (select BattleId from SamuraiBattles where SamuraiId=@samuraiId)
	order by StartDate
	return @ret
end

