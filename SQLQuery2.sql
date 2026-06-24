ALTER TABLE Menus ADD ParentMenuId INT NULL;

UPDATE Menus SET ParentMenuId = NULL WHERE MenuId IN (1,2,4);

UPDATE Menus SET ParentMenuId = 2 WHERE MenuId IN (3);
UPDATE Menus SET ParentMenuId = 4 WHERE MenuId IN (5,6,7);
