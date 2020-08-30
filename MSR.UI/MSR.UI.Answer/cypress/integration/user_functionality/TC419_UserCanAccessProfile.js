describe('Parts Functionality', () => {
    it('TC71_TC320_PartFilterGrid', () => {
        cy.server();
        cy.route({
            method: 'GET',
            url: '/v1/User?Id=134',
            response: {
                "object": [{
                    "id": 134,
                    "isActive": true,
                    "userRoleId": null,
                    "userName": "admin",
                    "firstName": "Admin",
                    "lastName": "Msrfsr",
                    "fullName": "Admin Msrfsr",
                    "title": "Admin",
                    "email": "admin-msrfsr@cmhworks.com",
                    "securityStamp": null,
                    "phone": "5552221212",
                    "supervisorId": 45,
                    "supervisorName": "Mike Harvey",
                    "locationId": 0,
                    "locationName": null,
                    "isAnswerUser": true,
                    "customerId": 0,
                    "lockoutEndDateUtc": null,
                    "lockoutEnabled": false,
                    "accessFailedCount": 0,
                    "timeZoneId": 0,
                    "lastUpdatedOn": "2020-06-05T23:16:32.637",
                    "timeZone": null,
                    "lastUpdatedBy": 134,
                    "createdOn": "2020-06-05T19:13:53",
                    "createdBy": 68,
                    "roles": [{
                        "id": 27,
                        "name": "Administrator",
                        "isCertificationRole": null,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }],
                    "fileModel": null
                }],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });
        cy.route({
            method: 'GET',
            url: '/v1/Timezone',
            response: {
                "object": [{
                    "id": 80,
                    "description": "ALASKA",
                    "offset": -8
                }, {
                    "id": 81,
                    "description": "Pacific Time (US & Canada);Tijuana",
                    "offset": -7
                }, {
                    "id": 82,
                    "description": "Arizona",
                    "offset": -7
                }, {
                    "id": 83,
                    "description": "Chihuahua, La Paz, Mazatlan",
                    "offset": -6
                }, {
                    "id": 84,
                    "description": "Mountain Time (US & Canada)",
                    "offset": -6
                }, {
                    "id": 85,
                    "description": "Central America",
                    "offset": -6
                }, {
                    "id": 86,
                    "description": "Central Time (US & Canada)",
                    "offset": -5
                }, {
                    "id": 87,
                    "description": "Guadalajara, Mexico City, Monterey",
                    "offset": -5
                }, {
                    "id": 88,
                    "description": "Saskatchewan",
                    "offset": -6
                }, {
                    "id": 89,
                    "description": "Bogota, Lima, Quito",
                    "offset": -5
                }, {
                    "id": 90,
                    "description": "Eastern Time (US & Canada)",
                    "offset": -4
                }, {
                    "id": 91,
                    "description": "Indiana (East)",
                    "offset": -5
                }, {
                    "id": 92,
                    "description": "Atlantic Time (Canada)",
                    "offset": -3
                }, {
                    "id": 93,
                    "description": "Caracas, La Paz",
                    "offset": -4
                }, {
                    "id": 95,
                    "description": "Santiago",
                    "offset": -3
                }, {
                    "id": 96,
                    "description": "Newfoundland",
                    "offset": -2.5
                }, {
                    "id": 97,
                    "description": "Brasilia",
                    "offset": -2
                }, {
                    "id": 98,
                    "description": "Buenos Aires, Georgetown",
                    "offset": -3
                }, {
                    "id": 99,
                    "description": "Greenland",
                    "offset": -2
                }, {
                    "id": 100,
                    "description": "Mid-Atlantic",
                    "offset": -1
                }, {
                    "id": 11436,
                    "description": "International Date Line West",
                    "offset": -12
                }, {
                    "id": 11437,
                    "description": "Midway Island, Samoa",
                    "offset": -11
                }, {
                    "id": 11438,
                    "description": "Hawaii",
                    "offset": -10
                }, {
                    "id": 11439,
                    "description": "Azores",
                    "offset": 0
                }, {
                    "id": 11440,
                    "description": "Cape Verde Is.",
                    "offset": -1
                }, {
                    "id": 11441,
                    "description": "Casablanca",
                    "offset": 0
                }, {
                    "id": 11442,
                    "description": "Greenwich Mean Time : Dublin, Edinburgh, Lisbon, L",
                    "offset": 1
                }, {
                    "id": 11443,
                    "description": "Amsterdam, Berlin, Bern, Rome, Stockholm",
                    "offset": 2
                }, {
                    "id": 11444,
                    "description": "Belgrade, Bratislava, Budapest, Ljubljana, Prague",
                    "offset": 2
                }, {
                    "id": 11445,
                    "description": "Brussels, Copenhagen, Madrid, Paris",
                    "offset": 2
                }, {
                    "id": 11446,
                    "description": "Sarajevo, Skopje, Warsaw, Zagreb",
                    "offset": 2
                }, {
                    "id": 11447,
                    "description": "West Central Africa",
                    "offset": 1
                }, {
                    "id": 11448,
                    "description": "Athens, Beirut, Istanbul, Minsk",
                    "offset": 3
                }, {
                    "id": 11449,
                    "description": "Bucharest",
                    "offset": 3
                }, {
                    "id": 11450,
                    "description": "Cairo",
                    "offset": 3
                }, {
                    "id": 11451,
                    "description": "Harare, Pretoria",
                    "offset": 2
                }, {
                    "id": 11452,
                    "description": "Helsinki, Kyiv, Riga, Sofia, Tallinn",
                    "offset": 3
                }, {
                    "id": 11453,
                    "description": "Jerusalem",
                    "offset": 2
                }, {
                    "id": 11454,
                    "description": "Baghdad",
                    "offset": 4
                }, {
                    "id": 11455,
                    "description": "Kuwait, Riyadh",
                    "offset": 3
                }, {
                    "id": 11456,
                    "description": "Moscow, St. Petersburgh, Volgograd",
                    "offset": 4
                }, {
                    "id": 11457,
                    "description": "Nairobi",
                    "offset": 3
                }, {
                    "id": 11458,
                    "description": "Tehran",
                    "offset": 4.5
                }, {
                    "id": 11459,
                    "description": "Abu Dhabi, Muscat",
                    "offset": 4
                }, {
                    "id": 11460,
                    "description": "Baku, Tbilisi, Yerevan",
                    "offset": 5
                }, {
                    "id": 11461,
                    "description": "Kabul",
                    "offset": 4.5
                }, {
                    "id": 11462,
                    "description": "Ekaterinburg",
                    "offset": 6
                }, {
                    "id": 11463,
                    "description": "Islamabad, Karachi, Tashkent",
                    "offset": 5
                }, {
                    "id": 11464,
                    "description": "Chennai, Kolkata, Mumbai, New Delhi",
                    "offset": 5.5
                }, {
                    "id": 11465,
                    "description": "Kathmandu",
                    "offset": 5.75
                }, {
                    "id": 11466,
                    "description": "Almaty, Novosibirsk",
                    "offset": 7
                }, {
                    "id": 11467,
                    "description": "Astana, Dhaka",
                    "offset": 6
                }, {
                    "id": 11468,
                    "description": "Sri Jayawardenepura",
                    "offset": 6
                }, {
                    "id": 11469,
                    "description": "Rangoon",
                    "offset": 6.5
                }, {
                    "id": 11470,
                    "description": "Bagkok, Hanoi, Jakarta",
                    "offset": 7
                }, {
                    "id": 11471,
                    "description": "Krasnoyarsk",
                    "offset": 8
                }, {
                    "id": 11472,
                    "description": "Beijing, Chongqing, Hong Kong, Urumqi",
                    "offset": 8
                }, {
                    "id": 11473,
                    "description": "Irkutsk, Ulaan Bataar",
                    "offset": 9
                }, {
                    "id": 11474,
                    "description": "Kuala Lumpur, Singapore",
                    "offset": 8
                }, {
                    "id": 11475,
                    "description": "Perth",
                    "offset": 8
                }, {
                    "id": 11476,
                    "description": "Taipei",
                    "offset": 8
                }, {
                    "id": 11477,
                    "description": "Osaka, Sapporo, Tokyo",
                    "offset": 8
                }, {
                    "id": 11478,
                    "description": "Seoul",
                    "offset": 9
                }, {
                    "id": 11479,
                    "description": "Yakutsk",
                    "offset": 10
                }, {
                    "id": 11480,
                    "description": "Adelaide",
                    "offset": 10.5
                }, {
                    "id": 11481,
                    "description": "Darwin",
                    "offset": 9.5
                }, {
                    "id": 11482,
                    "description": "Brisbane",
                    "offset": 10
                }, {
                    "id": 11483,
                    "description": "Canberra, Melbourne, Sydney",
                    "offset": 11
                }, {
                    "id": 11484,
                    "description": "Guam, Port Moresbby",
                    "offset": 10
                }, {
                    "id": 11485,
                    "description": "Hobart",
                    "offset": 11
                }, {
                    "id": 11486,
                    "description": "Vladivostok",
                    "offset": 11
                }, {
                    "id": 11487,
                    "description": "Magadan, Soloman Is., New Caledoria",
                    "offset": 11
                }, {
                    "id": 11488,
                    "description": "Auckland, Wellington",
                    "offset": 13
                }, {
                    "id": 11489,
                    "description": "Fiji, Kamchatka, Marshall Is.",
                    "offset": 12
                }, {
                    "id": 11490,
                    "description": "Nuku'alofa",
                    "offset": 13
                }],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });
        cy.beginWebConsoleTracking();
        cy.login('admin');

        cy.get('[data-cy=account-a]').click();
        cy.get('[data-cy=profile-link]').click();
        cy.get('[data-cy=firstNameValue]').contains('Admin');
        cy.get('[data-cy=lastNameValue]').contains('Msrfsr');
        cy.get('[data-cy=emailValue]').contains('admin-msrfsr@cmhworks.com');
        cy.get('[data-cy=titleValue]').contains('Admin');
        cy.get('[data-cy=phoneValue]').contains('5552221212');
        cy.get('[data-cy=supervisorNameValue]').contains('Mike Harvey');
        cy.get('[data-cy=createdOnValue]').contains('6/5/20, 7:13 PM');
        cy.get('[data-cy=isActiveValue]').contains('Yes');
        cy.get('[data-cy=isAnswerUserValue]').contains('Yes');
        cy.get('[data-cy=rolesValue]').contains('Administrator');
        cy.get('[data-cy=locationNameValue]').contains('');
        //not set in this profile.
        // cy.get('[data-cy=timezone-dropdown]').contains('');
     });
});


