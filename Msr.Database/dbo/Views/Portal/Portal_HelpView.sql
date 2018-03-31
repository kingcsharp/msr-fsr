 CREATE view [dbo].[Portal_HelpView]
AS
select
Ph.id,
Ph.title,
Ph.FriendlyUrl,
Ph.Content,
ph.roles,
Ph.Category,
(
select distinct +','+PR.RoleName
from Portal_RolesView PR
where ','+Ph.Roles+',' like '%,'+PR.Root+'%'
for xml path(''), type
).value('substring(text()[1], 2)', 'varchar(max)') as RoleName
FROM Portal_HelpPage Ph;

GO
