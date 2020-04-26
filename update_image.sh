#!/usr/bin/env bash

# Test on MacOSX
#sed -i '' 's/auditflix:.*/auditflix:'"$1"''"$2"'/' docker-compose-dev.yml

# 1=Environment, 2=Build Number, 3=docker-compose file
sed -i 's/msr-ui:.*/msr-ui:'"$1"''"$2"'/' $3

IMAGE=$(grep 'image' $3)

echo $IMAGE
