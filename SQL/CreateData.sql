use PropertyManager;
GO
-- Insert properties
INSERT INTO Properties
(Name, Address1, Address2, City, State, Zipcode) 
VALUES ('Vantaggio Suites', '1736 State Street', 'Room 264', 'San Diego', 'CA', '92101');
GO

select * from Properties;