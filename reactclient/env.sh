#!/bin/bash

# Recriar o arquivo de configuração
rm -rf ./env-config.js
touch ./env-config.js

# Add attribution
echo "window._env_ = {" >> ./env-config.js

# Read each line from .env file
# Cada linha representa pares chave-valor
while read -r line || [[ -n "$line" ]];
do
  # Split variables by character `=`
  if printf '%s\n' "$line" | grep -q -e '='; then
    varname=$(printf '%s\n' "$line" | sed -e 's/=.*//')
    varvalue=$(printf '%s\n' "$line" | sed -e 's/^[^=]*=//')
  fi

  # Ler os valores da variável atual se existir como uma variável de ambiente
  value=$(printf '%s\n' "${!varname}")
  # Caso contrário, usar o valor do arquivo .env
  [[ -z $value ]] && value=${varvalue}
  
  # Associar a propriedade de configuração ao arquivo JS
  echo "  $varname: \"$value\"," >> ./env-config.js
done < .env

echo "}" >> ./env-config.js