create view dbo.SamuraiBattlesStats
as
select Samurais.Name,
COUNT(dbo.SamuraiBattles.BattleId) as 'NumberOfBattle',
dbo.EarliestBattleFougthBySamurai(MIN(Samurais.Id))as 'EarliestBattle'
from dbo.SamuraiBattles 
join dbo.Samurais on Samurais.Id=SamuraiBattles.SamuraiId
group by dbo.Samurais.Name,dbo.SamuraiBattles.BattleId

