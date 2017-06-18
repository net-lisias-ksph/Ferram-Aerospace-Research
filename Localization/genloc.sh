#!/bin/bash

printf "Localization\n{"

for CFGFILE in *.cfg
do
	CFGNAME=${CFGFILE%.cfg}
	printf "\n\t"
	printf '%s' "$CFGNAME"
	printf "\n\t{\n"

	cat $CFGFILE | sed -e $'s/^/\t\t/g'

	printf "\n\t}\n"
done

printf "}"
