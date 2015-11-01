use PropertyManager;
GO

DELETE FROM Leases WHERE
PropertyId=(SELECT PropertyId FROM Properties WHERE NAME LIKE 'Office Space');
DELETE FROM Properties WHERE
NAME LIKE 'Office Space';

select * from Properties;
select * from Tenants;
select * from Leases;
