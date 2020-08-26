#!/bin/sh

# Used to adjust the output of the swagger web UI to
# something that can be compared to the stored spec
# used to import into assertible.

set -x

curl https://localhost:44398/swagger/Answer3/swagger.json > ~/xfer/swagger.json

cp ~/xfer/swagger.json swagger-work.json
dos2unix swagger-work.json
patch -p1 < swagger-removebearer.patch || exit
patch -p1 < swagger-addexamples.patch || exit
