CREATE TABLE testDB.dbo.[User] (
	Id			int IDENTITY(1,1)									NOT NULL,
	[UserName]   nvarchar(500) COLLATE Chinese_Taiwan_Stroke_CI_AS	NOT NULL,	
	[Date]		datetime										    NOT NULL,	
	CONSTRAINT User_PK PRIMARY KEY (Id)
);

INSERT INTO testDB.dbo.[User]
(UserName, [Date])
VALUES('Peter Parker', '2021-12-7');