
SET IDENTITY_INSERT AppTypes ON 


INSERT INTO dbo.AppTypes (Id, Name, Remark, IsDeleted, AddTime, LastModifyTime) VALUES (1, '系统123', NULL, '0', '2016-12-15 10:51:26.053', '2016-12-25 16:57:34.287');
INSERT INTO dbo.AppTypes (Id, Name, Remark, IsDeleted, AddTime, LastModifyTime) VALUES (2, '测试', NULL, '1', '2016-12-15 10:52:01.000', '2016-12-25 16:57:34.287');
INSERT INTO dbo.AppTypes (Id, Name, Remark, IsDeleted, AddTime, LastModifyTime) VALUES (3, '测试', NULL, '0', '2016-12-15 16:03:38.327', '2016-12-25 16:57:34.287');

SET IDENTITY_INSERT AppTypes OFF 