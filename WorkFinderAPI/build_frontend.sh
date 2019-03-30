#!/bin/bash

set -e;

pushd ../WorkFinderFrontEnd;

npm install;
npm run build-dev;

cp ./public/build/*.js ../WorkFinderAPI/wwwroot/js/;

popd;
