# Ferram Aerospace Research :: Change Log

* 2014-1227: 0.14.6 (ferram4) for KSP 0.23.5
	+ Features:
	+ Modified skin friction variation with M and Re to closer to that expected by using the Knudsen number
	+ Changed saving and loading method to allow better behavior when settings need to be cleaned during updates, especially for automated installs
	+ Modified aerodynamic failures for water landings for compatibility with upcoming BetterBuoyancy
	+ Option for aerodynamic failures to result in explosions at the joint during failure.
	+ Serious reworking to handle edge cases with lightly-clipped parts and their effects on blunt body drag (read: when people clip heatshields into the bottom of Mk1 pods and cause problems)
	+ Upgrade to MM 2.5.6
	+ Bugfixes:
	+ Fixed an issue that prevented Trajectories from functioning
	+ Fixed blunt body drag errors with AJE
	+ Fixed issues involving editor GUI and control surface deflections
	+ Fixed edge cases involving attach-node blunt body drag being applied when it shouldn't have
	+ Fixed issues with command pods containing intakes
