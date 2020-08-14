#!/usr/bin/env bash

# Test on MacOSX
#sed -i '' 's/auditflix:.*/auditflix:'"$1"''"$2"'/' docker-compose-dev.yml

# 1=Environment, 2=commit hash, 3=docker-compose file
sed -i 's/msr-api:.*/msr-api:'"$2"'/' $3
sed -i 's/msr-rp:.*/msr-rp:'"$2"'/' $3
IMAGE=$(grep 'image' $3)

echo $IMAGE
