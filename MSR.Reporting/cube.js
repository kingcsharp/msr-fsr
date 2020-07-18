const AWSHandlers = require('@cubejs-backend/serverless-aws');
const MSSQLDriver = require('@cubejs-backend/mssql-driver');

//module.exports = require('@cubejs-backend/serverless');
module.exports = new AWSHandlers({
    externalDbType: 'mssql',
    externalDriverFactory: () => new MSSQLDriver({
        host: process.env.CUBEJS_EXT_DB_HOST,
        database: process.env.CUBEJS_EXT_DB_NAME,
        port: process.env.CUBEJS_EXT_DB_PORT,
        user: process.env.CUBEJS_EXT_DB_USER,
        password: process.env.CUBEJS_EXT_DB_PASS,
    })
});