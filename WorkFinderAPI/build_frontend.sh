#!/bin/bash

set -e;

pushd ../WorkFinderFrontEnd;

npm install;
npm run build-dev;

popd;

cd ./wwwroot/js
rm *.js
cd ../../

cp ../WorkFinderFrontEnd/public/build/*.js wwwroot/js/;
