USE [ColossusPortal]

DELETE [Security].[User] WHERE UserID > 1
DELETE [Person].[Contact] WHERE ContactId > 1
DELETE [Portal].[EntityBase] WHERE EntityId > 1
DELETE [Security].[Login] WHERE LoginId > 1