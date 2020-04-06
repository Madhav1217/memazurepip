update md
set Value = 'AK, HI, MD, ME, MT, ND, PR, SD, VT, WY, WA, DC, TX, CO, NH'
FROM MasterData md
WHERE [key] = 'EnrollmentNotAllowedStates';

-- POR-4270
update bl 
set statecode = 'MO'
From BrokerLicense bl where bl.LicenseNumber = 'dummy'
and StateCode = 'MT';
