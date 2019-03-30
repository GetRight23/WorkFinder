#!/bin/bash

set -e;

pushd ../WorkFinderFrontEnd;

DIR="../WorkFinderAPI/wwwroot/js/"

if [ "$(ls -A $DIR)" ]; then
     echo "front end folder not empty"
else
    npm install;
	npm run build-dev;
	cp ./public/build/*.js ../WorkFinderAPI/wwwroot/js/;
fi

popd;
