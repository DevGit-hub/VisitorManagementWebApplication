DELETE FROM Menus
WHERE MenuId = 10;

UPDATE Menus
SET ParentMenuId = 8
WHERE MenuId = 9;

UPDATE Menus
SET ParentMenuId = NULL
WHERE MenuId = 8;

INSERT INTO RoleMenus(Role, MenuId)
VALUES ('Admin', 9)

DELETE FROM RoleMenus
WHERE RoleMenuId IN (15,16,17,18,19,20,21,22,23,24,8);

INSERT INTO RoleMenus(Role, MenuId)
VALUES ('Admin', 8)

INSERT INTO RoleMenus(Role, MenuId)
VALUES ('User', 1)