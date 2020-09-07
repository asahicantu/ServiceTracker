update SubPortfolio 
set Name = t.new_name
from 
(
SELECT 
s.Id,
P.Name + '-'+ S.Name  new_name
FROM Portfolio P INNER JOIN SubPortfolio S ON P.Id = S.PortfolioId
) t

where SubPortfolio.id = t.Id
SELECT * FROM SubPortfolio
