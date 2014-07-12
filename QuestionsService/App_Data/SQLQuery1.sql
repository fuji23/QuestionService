
CREATE TRIGGER MyTrig
ON dbo.Questions
AFTER INSERT 
AS
BEGIN
print 'Hello'
END

GO
