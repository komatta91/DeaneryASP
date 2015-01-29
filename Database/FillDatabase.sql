USE [NTR2014]
GO

DELETE FROM GradeValues
DELETE FROM Registrations
DELETE FROM Grades
DELETE FROM Realisations
DELETE FROM Semesters
DELETE FROM Subjects
DELETE FROM Students
DELETE FROM Groups
DELETE FROM Users
GO

SET IDENTITY_INSERT Groups ON
GO
INSERT INTO Groups  (GroupID, Name)   VALUES(1, 'S2I')
INSERT INTO Groups  (GroupID, Name)   VALUES(2, 'R2I')
SET IDENTITY_INSERT Groups OFF
GO


SET IDENTITY_INSERT Users ON
GO
INSERT INTO Users  (UserID, FirstName, LastName, UID, PWD)   VALUES(1, 'Jan', 'Kowalski','jk','jk')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(2, 'Adam', 'Nowak','an','an')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(3, 'Zosia', 'Samosia','zs','zs')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(4, 'Angela', 'Merkel','am','am')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(5, 'Barak', 'Obama','bo','bo')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(6, 'Margaret', 'Tatcher','mt','mt')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(7, 'Adam', 'Ma³ysz','ama','ama')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(8, 'Pawe³', 'Abacki','pa','pa')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(9, 'Gawel', 'Babacki','gb','gb')

INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(1000, 'Adaœ', 'Administrator','Admin','Admin')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(1001, 'Pawe³', 'Wyk³adniczy','belfer1','belfer1')
INSERT INTO Users  (UserID, FirstName, LastName,UID, PWD)   VALUES(1002, 'Rafa³', 'Logarytmiczny','belfer2','belfer2')
SET IDENTITY_INSERT Users OFF
GO


INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(1, 1,'100001')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(2, 1,'100002')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(3, 1,'100003')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(4, 2,'100021')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(5, 2,'100022')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(6, 2,'100023')
INSERT INTO Students  (StudentID, GroupID, IndexNo)   VALUES(7, 2,'100024')


SET IDENTITY_INSERT dbo.Subjects ON
GO
INSERT INTO Subjects  (SubjectID, Name,Conspect,url)   VALUES(1000, 'NTR','Narzêdzia Typu RAD','ntr@ii.pw.edu.pl')
INSERT INTO Subjects  (SubjectID, Name,Conspect,url)   VALUES(2000, 'POBR','Przetwarzanie Obrazów',NULL)
INSERT INTO Subjects  (SubjectID, Name,Conspect,url)   VALUES(3000, 'IOP','In¿ynieria Oprogramowania',NULL)
INSERT INTO Subjects  (SubjectID, Name,Conspect,url)   VALUES(4000, 'SOI','Systemy Operacyjne',NULL)
SET IDENTITY_INSERT dbo.Subjects OFF
GO

SET IDENTITY_INSERT dbo.Semesters ON
GO
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(100, '12L','N')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(200, '12Z','N')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(300, '13L','N')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(400, '13Z','N')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(500, '14L','N')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(600, '14Z','Y')
INSERT INTO Semesters  (SemesterID, Name,Active)   VALUES(700, '15L','Y')
SET IDENTITY_INSERT dbo.Semesters OFF
GO

SET IDENTITY_INSERT dbo.Realisations ON
GO
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID, Ver)   VALUES(1100, 1000,100,'B') -- NTR 12L
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(2100, 2000,100) -- POBR 12L
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(3100, 3000,100) -- IOP 12L
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(4100, 4000,100) -- SOI 12L
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(1200, 1000,200) -- NTR 12Z
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(2300, 2000,300) -- POBR 13L
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(2400, 2000,400) -- POBR 13Z
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID)   VALUES(3300, 3000,300) -- IOP 13L

INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID, UserID)   VALUES(2600, 2000,600,1001) -- POBR 14Z
INSERT INTO Realisations  (RealisationID, SubjectID,  SemesterID, UserID)   VALUES(1600, 1000,600,1002) -- NTR 14Z

SET IDENTITY_INSERT dbo.Realisations OFF
GO

SET IDENTITY_INSERT dbo.Grades ON
GO
-- NTR 12L
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(11100,1100, 'L1','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(21100,1100, 'L2','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(31100,1100, 'L3','0;1;2;3;4;5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(41100,1100, 'L4','10')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(51100,1100, 'L5','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(61100,1100, 'L6','15')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(71100,1100, 'L7','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(81100,1100, 'L8','15')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(91100,1100, 'L9','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(101100,1100, 'K1','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(111100,1100, 'K2','20')
-- POBR 12L
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(12100,2100, 'L1','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(22100,2100, 'L2','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(32100,2100, 'L3','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(42100,2100, 'K1','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(52100,2100, 'K2','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(62100,2100, 'P','-10;-9;-8;-7;-6;-5;-4;-3;-2;-1;0;1;2;3;4;5;6;7;8;9;10')


-- NTR 14Z
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(11600,1600, 'L1','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(21600,1600, 'L2','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(31600,1600, 'L3','0;1;2;3;4;5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(41600,1600, 'L4','10')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(51600,1600, 'L5','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(61600,1600, 'L6','15')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(71600,1600, 'L7','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(81600,1600, 'L8','15')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(91600,1600, 'L9','5')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(101600,1600, 'k1','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(111600,1600, 'k2','20')
-- POBR 14Z
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(12600,2600, 'L1','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(22600,2600, 'L2','N;Z')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(42600,2600, 'k1','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(52600,2600, 'k2','20')
INSERT INTO Grades  (GradeID, RealisationID, Name, MaxValue)   VALUES(62600,2600, 'P','-10;-9;-8;-7;-6;-5;-4;-3;-2;-1;0;1;2;3;4;5;6;7;8;9;10')

SET IDENTITY_INSERT dbo.Grades OFF
GO


SET IDENTITY_INSERT dbo.Registrations ON
GO
-- NTR 12L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1101,1,1100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1102,2,1100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1103,3,1100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1104,4,1100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1105,5,1100)

--POBR 12L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2101,1,2100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2102,2,2100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2103,3,2100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2104,4,2100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2105,5,2100)

-- IOP 12L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3101,1,3100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3102,2,3100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3103,3,3100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3104,4,3100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3105,5,3100)

 -- SOI 12L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(4101,1,4100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(4102,2,4100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(4103,3,4100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(4104,4,4100)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(4105,5,4100)

-- NTR 12Z
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1206,6,1200)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(1207,7,1200)

-- POBR 13L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2306,6,2300)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2307,7,2300)

-- POBR 13Z
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2406,6,2400)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2407,7,2400)

-- IOP 13L
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3306,6,3300)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(3307,7,3300)


-- POBR 14Z
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2606,6,2600)
INSERT INTO Registrations  (RegistrationID,StudentID,RealisationID) VALUES(2607,7,2600)


SET IDENTITY_INSERT dbo.Registrations OFF
GO




SET IDENTITY_INSERT dbo.GradeValues ON
GO
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(111001101,11100,1101,'Z','2012-11-11')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(211001101,21100,1101,'5','2012-11-12')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(311001101,31100,1101,'4','2012-11-13')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(411001101,41100,1101,'7','2012-11-14')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(511001101,51100,1101,'5','2012-11-15')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(611001101,61100,1101,'12','2012-11-16')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(711001101,71100,1101,'5','2012-11-17')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(811001101,81100,1101,'15','2012-11-18')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(911001101,91100,1101,'5','2012-11-19')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(1011001101,101100,1101,'18','2012-11-20')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(1111001101,111100,1101,'17','2012-11-21')

INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(111001102,11100,1102,'N','2012-11-11')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(211001102,21100,1102,'4','2012-11-12')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(311001102,31100,1102,'2','2012-11-13')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(411001102,41100,1102,'6','2012-11-14')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(511001102,51100,1102,'5','2012-11-15')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(611001102,61100,1102,'10','2012-11-16')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(711001102,71100,1102,'2','2012-11-17')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(811001102,81100,1102,'5','2012-11-18')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(911001102,91100,1102,'2','2012-11-19')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(1011001102,101100,1102,'11','2012-11-20')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(1111001102,111100,1102,'16','2012-11-21')

INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(111001103,11100,1103,'Z','2012-11-21')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(211001103,21100,1103,'4','2012-11-22')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(311001103,31100,1103,'1','2012-11-23')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(411001103,41100,1103,'3','2012-11-24')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(511001103,51100,1103,'1','2012-11-25')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(611001103,61100,1103,'11','2012-11-26')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(711001103,71100,1103,'3','2012-11-27')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(811001103,81100,1103,'5','2012-11-28')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(911001103,91100,1103,'4','2012-11-29')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(1011001103,101100,1103,'4','2012-11-20')




INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(121002101,12100,2101,'Z','2012-10-01')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(221002101,22100,2101,'Z','2012-10-02')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(321002101,32100,2101,'N','2012-10-03')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(421002101,42100,2101,'11','2012-10-04')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(521002101,52100,2101,'7','2012-10-05')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(621002101,62100,2101,'-5','2012-10-06')

INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(121002102,12100,2102,'Z','2012-11-01')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(221002102,22100,2102,'Z','2012-11-02')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(321002102,32100,2102,'Z','2012-11-03')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(421002102,42100,2102,'20','2012-11-04')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(521002102,52100,2102,'20','2012-11-05')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(621002102,62100,2102,'7','2012-11-06')

INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(221002103,22100,2103,'Z','2012-12-02')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(321002103,32100,2103,'Z','2012-12-03')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(421002103,42100,2103,'3','2012-12-04')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(521002103,52100,2103,'2','2012-12-05')
INSERT INTO GradeValues  (GradeValueID,GradeID,RegistrationID,Value,Date) VALUES(621002103,62100,2103,'-7','2012-12-06')




SET IDENTITY_INSERT dbo.GradeValues OFF
GO

