#!/usr/bin/env bash

# Test on MacOSX
#sed -i '' 's/auditflix:.*/auditflix:'"$1"''"$2"'/' docker-compose-dev.yml

# 1=Environment, 2=commit hash, 3=docker-compose file
sed -i 's/msr-portal-ui:.*/msr-portal-ui:'"$4"''"$2"'/' $3
sed -i 's/answer3-portal-ui-.*/answer3-portal-ui-'"$1"'/' $3

IMAGE=$(grep 'image' $3)

echo $IMAGE
