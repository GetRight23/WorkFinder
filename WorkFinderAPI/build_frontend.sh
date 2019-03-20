#!/bin/bash

set -e;

pushd ../WorkFinderFrontEnd;

npm install;
npm run build;

popd;

cp ../WorkFinderFrontEnd/public/build/*.js wwwroot/js/;
