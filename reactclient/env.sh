#!/bin/bash

# Recreate conf file
rm -rf ./env-config.js
touch ./env-config.js

# Add attribution
echo "window._env_ = {" >> ./env-config.js

# Read each line from .env file
while read -r line || [[ -n "$line" ]];
do
  # Split variables by character `=`
  if printf '%s\n' "$line" | grep -q -e '='; then
    varname=$(printf '%s\n' "$line" | sed -e 's/=.*//')
    varvalue=$(printf '%s\n' "$line" | sed -e 's/^[^=]*=//')
  fi

  # Read value from environment variable if exist
  value=$(printf '%s\n' "${!varname}")
  # Else use values from .env file
  [[ -z $value ]] && value=${varvalue}
  
  # Link configuration property to JS file
  echo "  $varname: \"$value\"," >> ./env-config.js
done < .env

echo "}" >> ./env-config.js