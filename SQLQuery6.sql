-- ADMIN
INSERT INTO RoleMenus(Role, MenuId)
SELECT 'Admin', MenuId FROM Menus;

-- USER (limited access)
INSERT INTO RoleMenus(Role, MenuId)
SELECT 'User', MenuId FROM Menus
WHERE DisplayName IN ('Dashboard','Visitors','Add Visitor');