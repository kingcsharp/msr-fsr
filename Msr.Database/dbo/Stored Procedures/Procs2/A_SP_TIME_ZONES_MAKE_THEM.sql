

CREATE    PROCEDURE dbo.A_SP_TIME_ZONES_MAKE_THEM
AS
declare @strID as char(10)
declare @cnt as int
set @cnt = 1
exec A_SP_TIME_ZONE_CREATE_ONE 'International Date Line West','-12',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Midway Island, Samoa','-11',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Hawaii','-10',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Alaska','-9',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Pacific Time (US & Canada);Tijuana','-8',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Arizona','-7',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Chihuahua, La Paz, Mazatlan','-7',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Mountain Time (US & Canada)','-7',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Central America','-6',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Central Time (US & Canada)','-6',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Guadalajara, Mexico City, Monterey','-6',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Saskatchewan','-6',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Bogota, Lima, Quito','-5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Eastern Time (US & Canada)','-5',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Indiana (East)','-5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Atlantic Time (Canada)','-4',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Caracas, La Paz','-4',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Santiago','-4',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Newfoundland','-3.5',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Brasilia','-3',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Buenos Aires, Georgetown','-3',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Greenland','-3',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Mid-Atlantic','-2',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Azores','-1',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Cape Verde Is.','-1',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Casablanca','0',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London','0',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Amsterdam, Berlin, Bern, Rome, Stockholm','+1',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Belgrade, Bratislava, Budapest, Ljubljana, Prague','+1',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Brussels, Copenhagen, Madrid, Paris','+1',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Sarajevo, Skopje, Warsaw, Zagreb','+1',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'West Central Africa','+1',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Athens, Beirut, Istanbul, Minsk','+2',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Bucharest','+2',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Cairo','+2',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Harare, Pretoria','+2',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Helsinki, Kyiv, Riga, Sofia, Tallinn','+2',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Jerusalem','+2',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Baghdad','+3',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Kuwait, Riyadh','+3',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Moscow, St. Petersburgh, Volgograd','+3',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Nairobi','+3',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Tehran','+3.5',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Abu Dhabi, Muscat','+4',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Baku, Tbilisi, Yerevan','+4',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Kabul','+4.5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Ekaterinburg','+5',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Islamabad, Karachi, Tashkent','+5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Chennai, Kolkata, Mumbai, New Delhi','+5.5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Kathmandu','+5.75',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Almaty, Novosibirsk','+6',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Astana, Dhaka','+6',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Sri Jayawardenepura','+6',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Rangoon','+6.5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Bagkok, Hanoi, Jakarta','+7',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Krasnoyarsk','+7',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Beijing, Chongqing, Hong Kong, Urumqi','+8',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Irkutsk, Ulaan Bataar','+8',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Kuala Lumpur, Singapore','+8',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Perth','+8',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Taipei','+8',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Osaka, Sapporo, Tokyo','+8',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Seoul','+9',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Yakutsk','+9',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Adelaide','+9.5',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Darwin','+9.5',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Brisbane','+10',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Canberra, Melbourne, Sydney','+10',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Guam, Port Moresbby','+10',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Hobart','+10',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Vladivostok','+10',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Magadan, Soloman Is., New Caledoria','+11',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Auckland, Wellington','+12',@cnt,'1'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Fiji, Kamchatka, Marshall Is.','+12',@cnt,'0'
set @cnt = @cnt + 1
exec A_SP_TIME_ZONE_CREATE_ONE 'Nuku''alofa','+13',@cnt,'0'
set @cnt = @cnt + 1







