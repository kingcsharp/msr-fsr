CREATE VIEW [dbo].[Portal_UserView]
AS

SELECT
 u.[Id]
,u.[Email]
,u.[EmailConfirmed]
,u.[PasswordHash]
,u.[SecurityStamp]
,u.[PhoneNumber] AS Phone
,u.Phone2
,u.[PhoneNumberConfirmed]
,u.[TwoFactorEnabled]
,u.[LockoutEndDateUtc]
,u.[LockoutEnabled]
,u.[AccessFailedCount]
,u.[UserName]
,u.[FirstName]
,u.[LastName]
,u.[FirstName]+ ' ' +u.[LastName] AS FullName
,u.[IsActive]
,u.[CreatedDate]
,u.[TimeZone]
,r.Id AS RoleId
FROM [AspNetUsers] u 
LEFT JOIN AspNetUserRoles ur ON ur.UserId = u.Id
LEFT JOIN AspNetRoles r ON r.Id = ur.RoleId
GO


