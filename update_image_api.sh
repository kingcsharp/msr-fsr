#!/usr/bin/env bash

# Test on MacOSX
#sed -i '' 's/auditflix:.*/auditflix:'"$1"''"$2"'/' docker-compose-dev.yml

# 1=Environment, 2=commit hash, 3=docker-compose file
sed -i 's/msr-api:.*/msr-api:'"$2"'/' $3
sed -i 's/msr-processor:.*/msr-processor:'"$2"'/' $3
sed -i 's/msr-rp:.*/msr-rp:'"$2"'/' $3
sed -i 's/ASPNETCORE_ENVIRONMENT=.*/ASPNETCORE_ENVIRONMENT='"$1"'/' $3
sed -i 's/answer3-api-.*/answer3-api-'"$1"'/' $3
sed -i 's/answer3-processor-.*/answer3-processor-'"$1"'/' $3
sed -i 's/answer3-rp-.*/answer3-rp-'"$1"'/' $3
IMAGE=$(grep 'image' $3)

echo $IMAGE
