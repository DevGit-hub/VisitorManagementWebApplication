INSERT INTO RoleMenus(Role, MenuId)
VALUES
('Admin', (SELECT MenuId FROM Menus WHERE DisplayName='Visitors')),
('Admin', (SELECT MenuId FROM Menus WHERE DisplayName='Users')),
('Admin', (SELECT MenuId FROM Menus WHERE DisplayName='Admins')),

('User', (SELECT MenuId FROM Menus WHERE DisplayName='Visitors'));