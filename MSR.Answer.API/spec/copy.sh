#!/bin/sh

# Used to adjust the output of the swagger web UI to
# something that can be compared to the stored spec
# used to import into assertible.

set -x

curl https://localhost:44398/swagger/Answer3/swagger.json > ~/xfer/swagger.json

cp ~/xfer/swagger.json swagger-work.json
dos2unix swagger-work.json
patch -p0 < swagger-removebearer.patch || exit
# diff -u swagger-work.json.orig swagger-work.json > swagger-removebearer.patch
cp swagger-work.json swagger-work.json.nobearer
patch -p0 < swagger-addexamples.patch || exit
# diff -u swagger-work.json.orig swagger-work.json > swagger-addexamples.patch

# report difference
diff -q swagger-work.json swagger.json
