# Ferram Aerospace Research :: Change Log

* 2015-0511: 0.15_Fanno (ferram4) for KSP 1.0.0
	+ Fixed improper voxelization of debris and vehicles dropped from existing vessel, including effects on stock "occlusion" system
	+ Fixed improper determination of vehicle main axis
	+ Fixed Kerbal EVAs having no drag
	+ Fixed exceptions where outirght disintegration could prevent some vehicles from having aerodynamics applied
	+ Added upper cap on memory allocated for voxelization
	+ Changed calculation of CoL to make more sense
	+ Fixed error in determining AoA for nominal flight in Stability Derivative GUI
	+ Hid yellow aero moment arrows by default in aero overlay to reduce user confusion
	+ Fixed lift / drag arrows remaining on wings that become shielded when aero overlay is open
	+ Switched to a cleaner method of setting internal speedometers
	+ Disable control surfaces auto-response below 5 m/s to prevent wacky flailing during load / when stopped
	+ Change compatibility settings to reject KSP 1.0.0, which is not compatible with RealChuteLite
	+ Updated save-load method to save more reliably and not throw exceptions
* 2015-0508: 0.15_Euler (ferram4) for KSP 1.0
	+ Compatibility with KSP 1.0, 1.0.1, and 1.0.2
	+ Upgraded to MM 2.6.3
	+ Introduction of ModularFlightIntegrator for interfacing with KSP drag / heating systems without interference with other mods
	+ Replaced previous part-based drag model with new vessel-centered, voxel-powered model:
		- Generates voxel model of vehicle using part meshes, accounting for part clipping
		- Drag is calculated for vehicle as a whole, rather than linear combination of parts
		- Payload fairings and cargo bays are emergent from code and do not require special treatment with configs
		- Area ruling of vehicles is accounted for; unsmooth area distributions will result in very high drag at and above Mach 1
		- Body lift accounts for vehicle shape in determining potential and viscous flow contributions
		- Areas exposed to outside used for stock heating calculations
	+ Performance optimizations in legacy wing model
	+ Jet engine windmilling drag accounted for at intakes
	+ Editor GUI improvements including:
		- Greater clarity in AoA / Mach sweep tab
		- Stability deriv GUI math modified for improved accuracy
		- Stability deriv simulation tweaked to fix some minor issues in displaying and calculating response
		- Addition of a Transonic Design tab that displays cross-section distribution and drag at Mach 1 for area ruling purposes
	+ Parachute methods have been replaced with RealChuteLite implementation by stupid_chris:
		- Less severe parachute deployment
		- Parachutes melt / break in high Mach number flows
		- No interference with RealChute
	+ Changes to FARAPI to get information faster
	+ FARBasicDragModel, FARPayloadFairingModule, FARCargoBayModule are now obsolete and removed from the codebase
	+ Extensive reorganizing of source to reduce spaghetti and improve maintainability
	+ Modifications to Firehound and Colibri to function with new flight model
	+ Addition of Blitzableiter and SkyEye example crafts
	+ A 1.5x increase to all stock gimbal ranges
* 2015-0402: 0.14.7 (ferram4) for KSP 0.23.5
	+ Features:
	+ Raised stalled-wing drag up to proper maximum levels
	+ Adjusted intake drag to be lower
	+ Improved method of dealing with very high vertex count parts for geometry purposes
	+ Upgraded to MM 2.5.13
	+ Included FAR Colibri, a VTOL by Tetryds as an example craft
	+ Bugfixes:
	+ Fixed an issue preventing loading custom-defined FARBasicDragModels
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
