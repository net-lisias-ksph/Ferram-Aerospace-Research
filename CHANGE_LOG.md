# Ferram Aerospace Research :: Change Log

* 2017-0522: 0.15.8_de_Laval (ferram4) for KSP 1.2.2
	+ Compatibility for KSP 1.2.2 (finally)
	+ Update to MFI 1.2.4
	+ Update to MM 2.7.6
	+ Lots of compatibility changes thanks to Alexander Abramov
	+ Reduce memory use and garbage production in GUI thanks to soulsource and Virindi-AC
	+ Fix GUI button multiplication
	+ Fix stock drag arrows to be useful again
	+ Fix voxelization errors with some intake parts
	+ Fix FARAction group settings not saving
	+ Fix landing gear main axis dtermination
	+ Fix voxel errors with some stock parts
	+ Made ignorable transforms for voxelization customizable via config
* 2017-0522: 0.15.8_deLaval (ferram4) for KSP 1.2.2
	+ Compatibility for KSP 1.2.2 (finally)
	+ Update to MFI 1.2.4
	+ Update to MM 2.7.6
	+ Lots of compatibility changes thanks to Alexander Abramov
	+ Reduce memory use and garbage production in GUI thanks to soulsource and Virindi-AC
	+ Fix GUI button multiplication
	+ Fix stock drag arrows to be useful again
	+ Fix voxelization errors with some intake parts
	+ Fix FARAction group settings not saving
	+ Fix landing gear main axis dtermination
	+ Fix voxel errors with some stock parts
	+ Made ignorable transforms for voxelization customizable via config
* 2016-0701: 0.15.7.2_Lanchester (ferram4) for KSP 1.1.3
	+ Fix a serious bug in v0.15.7 and v0.15.7.1 where chutes would not provide any drag
* 2016-0701: 0.15.7.2_Lanchester (ferram4) for KSP 1.1.3
	+ Fix a serious bug in v0.15.7 and v0.15.7.1 where chutes would not provide any drag
* 2016-0630: 0.15.7.1_Kutta (ferram4) for KSP 1.1.3
	+ Update to MFI 1.1.6 to fix an incompatibility with Kopernicus and the earlier version
	+ Update CompatibilityChecker version
	+ Update license
	+ Fix an issue where voxels could be incredibly asymmetric on symmetric crafts
* 2016-0630: 0.15.7.1_Kutta (ferram4) for KSP 1.1.3
	+ Update to MFI 1.1.6 to fix an incompatibility with Kopernicus and the earlier version
	+ Update CompatibilityChecker version
	+ Update license
	+ Fix an issue where voxels could be incredibly asymmetric on symmetric crafts
* 2016-0624: 0.15.7_Kuchemann (ferram4) for KSP 1.1.3
	+ Update to ModuleManager 2.6.25
	+ Update for KSP 1.1.3 compatibility
	+ Implement higher resolution sub-voxel voxelization method
	+ Allow switching between high and low res sub-voxel methods
	+ Optimize voxel shell generation, particularly for high triangle count meshes
	+ Increase the resistance to sideways aerostructural failures for many fuselage and rocket parts
	+ Fix voxelization error that would lead to transparent mesh objects being voxelized
	+ Fix voxelization errors that could lead to incomplete voxelization of some stock procedural fairing shapes
* 2016-0624: 0.15.7_Küchemann (ferram4) for KSP 1.1.3
	+ Update to ModuleManager 2.6.25
	+ Update for KSP 1.1.3 compatibility
	+ Implement higher resolution sub-voxel voxelization method
	+ Allow switching between high and low res sub-voxel methods
	+ Optimize voxel shell generation, particularly for high triangle count meshes
	+ Increase the resistance to sideways aerostructural failures for many fuselage and rocket parts
	+ Fix voxelization error that would lead to transparent mesh objects being voxelized
	+ Fix voxelization errors that could lead to incomplete voxelization of some stock procedural fairing shapes
* 2016-0518: 0.15.6.5_Knudsen (ferram4) for KSP 1.1.2
	+ Update to ModularFlightIntegrator 1.1.4
	+ Fix a serious issue where wings would provide no forces and forces would be distributed incorrectly across vehicles
	+ Fix an issue where wing symmetry counterparts would not have equal masses
	+ Fix non-zero convective heat flux on shielded parts
* 2016-0518: 0.15.6.5_Knudsen (ferram4) for KSP 1.1.2
	+ Update to ModularFlightIntegrator 1.1.4
	+ Fix a serious issue where wings would provide no forces and forces would be distributed incorrectly across vehicles
	+ Fix an issue where wing symmetry counterparts would not have equal masses
	+ Fix non-zero convective heat flux on shielded parts
* 2016-0515: 0.15.6.4_Kleinhans (ferram4) for KSP 1.1.2
	+ Fix a no-drag issue with asteroids
	+ Fix a physics breaking issue with Tweakscaled wing parts, thanks pellinor
	+ Fix GUI window positions not loading on vessel spawn
	+ Fix distribution of forces on parts; no change in total force and torque applied to vessel, just to which parts
	+ Fix slightly negative drag on rearward-facing vehicles at high Knudsen numbers
* 2016-0515: 0.15.6.4_Kleinhans (ferram4) for KSP 1.1.2
	+ Fix a no-drag issue with asteroids
	+ Fix a physics breaking issue with Tweakscaled wing parts, thanks pellinor
	+ Fix GUI window positions not loading on vessel spawn
	+ Fix distribution of forces on parts; no change in total force and torque applied to vessel, just to which parts
	+ Fix slightly negative drag on rearward-facing vehicles at high Knudsen numbers
* 2016-0430: 0.15.6.3_Kindelberger (ferram4) for KSP 1.1.2
	+ Recompile for KSP 1.1.2 compatibility
	+ Bundle ModuleManager 2.6.24 for 1.1.2 compatibility
	+ Fix a critical error that would cause KerbalEVAs to have no aerodynamic forces applied to them
* 2016-0430: 0.15.6.3_Kindelberger (ferram4) for KSP 1.1.2
	+ Recompile for KSP 1.1.2 compatibility
	+ Bundle ModuleManager 2.6.24 for 1.1.2 compatibility
	+ Fix a critical error that would cause KerbalEVAs to have no aerodynamic forces applied to them
* 2016-0429: 0.15.6.2_Kartveli (ferram4) for KSP 1.1.1
	+ Ensure KSP 1.1.1 compatibility
	+ Upgrade to ModuleManager 2.6.23
	+ Fix new landing gear interfering with main axis determination
	+ Fix RealChute / RealChuteLite interaction breaking stock chute behavior, thanks to stupid_chris
	+ Fix mass-calc error for wing-mass-strength that resulted in all planes gaining unhealthy amounts of weight
	+ Attempt to make debug-compatibility actually work, thanks to NathanKell
* 2016-0429: 0.15.6.2_Kartveli (ferram4) for KSP 1.1.1
	+ Ensure KSP 1.1.1 compatibility
	+ Upgrade to ModuleManager 2.6.23
	+ Fix new landing gear interfering with main axis determination
	+ Fix RealChute / RealChuteLite interaction breaking stock chute behavior, thanks to stupid_chris
	+ Fix mass-calc error for wing-mass-strength that resulted in all planes gaining unhealthy amounts of weight
	+ Attempt to make debug-compatibility actually work, thanks to NathanKell
* 2016-0423: 0.15.6.1_von_Karman (ferram4) for KSP 1.1
	+ Fix a critical CPU usage bug that resulted in voxelization threads SpinWaiting forever, monopolizing the processor
	+ Fix parachutes without RealChute configs not applying forces when FAR + RC are installed, thanks to stupid_chris
	+ Fix ModuleManager database reload function hanging halfway through, breaking the game, thanks to stupid_chris
* 2016-0423: 0.15.6.1_von_Karman (ferram4) for KSP 1.1
	+ Fix a critical CPU usage bug that resulted in voxelization threads SpinWaiting forever, monopolizing the processor
	+ Fix parachutes without RealChute configs not applying forces when FAR + RC are installed, thanks to stupid_chris
	+ Fix ModuleManager database reload function hanging halfway through, breaking the game, thanks to stupid_chris
* 2016-0420: 0.15.6_Jones (ferram4) for KSP 1.1
	+ Update to KSP 1.1
	+ Update to bundle ModuleManager 2.6.22
	+ Update to bundle ModularFlightIntegrator 1.1.3
	+ Updates to RealChuteLite, thanks to stupid_chris
	+ Compatibility changes for use of KSP debuggers, thanks to neouy
	+ Increase aerodynamic damping for fuselages to somewhat more realistic levels
	+ Fix a serious issue that disabled the majority of conduction between parts
	+ Disable win64 locking
* 2016-0420: 0.15.6_Jones (ferram4) for KSP 1.1
	+ Update to KSP 1.1
	+ Update to bundle ModuleManager 2.6.22
	+ Update to bundle ModularFlightIntegrator 1.1.3
	+ Updates to RealChuteLite, thanks to stupid_chris
	+ Compatibility changes for use of KSP debuggers, thanks to neouy
	+ Increase aerodynamic damping for fuselages to somewhat more realistic levels
	+ Fix a serious issue that disabled the majority of conduction between parts
	+ Disable win64 locking
* 2016-0218: 0.15.5.7_Johnson (ferram4) for KSP 1.0.5
	+ Tweak pitch and roll damping of fuselages to make more logical sense; excessive roll damping at high dynamic pressures for wingless vehicles has been fixed
	+ Change units for specific excess power in the Flight Data readout to be W/kg on the basis that it makes more logical sense than m^2/s^3
	+ Fix a critical error that prevented voxelizations of Kerbals or any vehicles that had Kerbals riding in a command seat
* 2016-0218: 0.15.5.7_Johnson (ferram4) for KSP 1.0.5
	+ Tweak pitch and roll damping of fuselages to make more logical sense; excessive roll damping at high dynamic pressures for wingless vehicles has been fixed
	+ Change units for specific excess power in the Flight Data readout to be W/kg on the basis that it makes more logical sense than m^2/s^3
	+ Fix a critical error that prevented voxelizations of Kerbals or any vehicles that had Kerbals riding in a command seat
* 2016-0208: 0.15.5.6_Jacobs (ferram4) for KSP 1.0.5
	+ Update to MM 2.6.18
	+ Fix more negative sonic drag issues
	+ Fix unrealistically low sonic drag
	+ Fix failure to load saved FAR data in flight
	+ Fix unrealistically high numbers in indicated airspeed at higher Mach numbers
	+ Lower critical Mach number for slender vehicles with sudden bulges and waviness in their cross-section
* 2016-0203: 0.15.5.5_Hugoniot (ferram4) for KSP 1.0.5
	+ Fix an inconsistency in calculations of sonic drag
	+ Fix possibility of sonic drag resulting in negative drag coefficients on very blunt shapes
	+ Generally increase sonic drag of blunt objects, generally decrease drag of slender objects
	+ Fix water drag failing to function under complete submersion
	+ Fix rare error where Procedural Fairings will not properly voxelize
	+ Fix GetCurrentDensity method (for external mods) to return result consistent with simulation
	+ Fix overheat interaction on load with ModuleCoreHeat
	+ Fix FAR breaking on attempts to load Training or Scenario scenes
	+ Fix spoilers and flaps not updating with settings in the editor
* 2015-1202: 0.15.5.4_Hoerner (ferram4) for KSP 1.0.5
	+ Adjust water drag for better splashdown performance
	+ Fix a serious voxelization issue with ModuleJettison, most notable in leading to no-drag reentries
	+ Fix an issue where 3rd-party voxelization updates could sometimes break the editor GUI and CoL
	+ Fix a serious issue that could lead to spontaneous crashes on aero initialization (either VAB / SPH CoL, editor GUI, or going to flight)
* 2015-1113: 0.15.5.3_vonHelmholtz (ferram4) for KSP 1.0.5
	+ Upgrade to MM 2.6.13
	+ RealChuteLite consistency with RealChute calcs and optimizations thanks to stupid_chris
	+ Implement dynamic smoothing calculations based on relative "filledness" of voxel; should help reduce effect of voxel-resolution-induced smoothing on larger vehicles
	+ Tweaks to critical Mach calculations
	+ Fix "silent" KSP update breaking hydrodynamic drag
	+ Fix some voxelization irregularities
	+ Fix control surface flap settings not appearing if the settings are turned on in flight
	+ Fix some other control surface in-flight changes oddities
	+ Fix Firehound MS example craft action groups not acting in symmetry
	+ Added E42 example craft by tetryds
* 2015-1110: 0.15.5.2_Helmbold (ferram4) for KSP 1.0.5
	+ Compatibility with KSP 1.0.5
	+ Upgrade to MFI 1.1.2
	+ Optimizations of runtime aerodynamic calculations
	+ Cut background memory usage for voxels to ~60% of previous value
	+ Increases in consistency of properties with similar voxel shapes
	+ Full support for new stock hydrodynamic drag
	+ Voxel model used in calculating radiative influx from celestial bodies
	+ Reduction in first-load inconsistency in editor
	+ More varied support for intake ducting setups, including support for stock Goliath engine
	+ Tweaks to intake drag at low airbreather throttles
	+ Editor GUI header cleanup
	+ Dropdowns notated with down triangles for clarity
	+ Added AoA Arrow to make AoAs for static analysis sweeps and stability deriv sims clearer
	+ Fix for voxelization issues with degenerate triangles
	+ Fix for voxelization issues with meshes with 0 triangles
	+ Fix for B9 pWings not solidifying properly
	+ Fix editor race condition in displaying sonic drag for vehicles
	+ Fix for multiple vehicle aerodynamic NREs that could break aero
	+ Fix for vehicle aerodynamics breaking under certain vessel-part configurations
	+ Updated FAR Firehound MS, FAR SkyEye, FAR Montauk Shuttle to be more useful in KSP 1.0.5
* 2015-0907: 0.15.5.1_Hayes (ferram4) for KSP 1.0.0
	+ Upgrade to MM 2.6.8
	+ Fix some legacy wing interaction issues
	+ Fix drag properties drifting slowly over multiple voxelization events due to numerical errors
	+ Fix parts being occluded when main axis is in a strange orientation
	+ Fix in-flight control surface tweaks not applying to symmetry counterparts
	+ Fix KerbalEVAs working with Vanguard Parachutes
	+ Fix for a critical error where detached boosters, weapons, debris, etc. would not have drag properties
* 2015-0825: 0.15.5_Haack (ferram4) for KSP 1.0.0
	+ Upgrade to MM 2.6.7
	+ Fix for some RealChute issues by DaMichel
	+ Animation ignoring for voxelization by Blowfish
	+ Addition of air brakes for yaw control (RudderBrakes) adopted from original code contributed by HoneyFox
	+ Reduction in memory garbage created during voxelization; this should reduce the impact of voxelization somewhat, especially hitching.  Note: some hitching may still occur with vehicles with many wing parts due to legacy wing code
	+ Runtime performance optimizations for ram drag and general aero calculations
	+ Highly optimized bounds checking for voxelization
	+ Kerbals handled by voxel model now (using a simple primitive shape, presence or absence of helmet doesn't matter)
	+ Control surface parameters available for tweaking in flight
	+ Control surface tweakables given open/close buttons for sections to reduce clutter
	+ Revert BDArmory bombs and missiles to stock model after launch to improve tracking, stability and predictions
	+ Fixed main axis issue with BDArmory parts and similarly designed parts
	+ Fixed a long-standing issue in wing aspect ratio calcs (they were double what they should have been)
	+ Fixed ram drag variation with throttle not accepting AJE jets as valid jets for that purpose
	+ Fixed drag not using the calculated critical Mach number for the beginning of the drag transonic rise
	+ Fixed possible NRE issues during steady destruction of vessel with parts being destroyed
	+ Fixed issue where increased number of wing parts would cause wing mass adjustment to increase mass without bound; mass is now bounded, but more concentrated in root parts than wingtip parts
	+ Tweaked subsonic drag downwards to get more accurate results with fixed AJE props
	+ Tweaked wing mass downwards slightly
	+ Tweaked wing strength power to result in greater strength from lower-mass wings
* 2015-0729: 0.15.4.1_Goldstein (ferram4) for KSP 1.0.0
	+ Re-implementation of aero viz coloration, thanks to mjn33
	+ Reduction in garbage produced by voxelization, prep for further garbage reductions
	+ Fixed NaN issue with KAX electric props
	+ Fixed drag-breaking NRE during rapid disintegrations
	+ Fixed some issues with Blizzy Toolbar icons
	+ Fixed exacerbation of stock heating bug
	+ Fixed control surfaces not updating direction during staging-related CoM shifts
* 2015-0712: 0.15.4_Glauert (ferram4) for KSP 1.0.0
	+ Update to MM 2.6.6
	+ Update to MFI 1.1.1, fixes gimbaling bug below 750 m/s on short vehicles
	+ Update win64 check code to MM method
	+ Added internal ducted area feature:
		- Area ducted through a vehicle from intakes to airbreathing engines will be removed from cross-section
		- Adjusts area ruling to properly model air that flows through the vehicle as opposed to around it
		- Does not count for airflow through switch-backing or reversing ducts; no benefits for intakes that feed upstream engines
		- Supports stock intake part + airbreathing engine part setups, AJE intake part + airbreathing engine part setups, and combined intake + engine part setups
	+ Slight improvement to Flight Data readouts from mjn33
	+ Toggle gear button now states "Raise" or "Lower" gear for clarity
	+ Fixed serious issue where exposed area was not updated for thermal calculations
	+ Fixed some blunt shapes having NaN drag in the transonic regime
	+ Fixed some blunt, thin-plate shapes having negative drag in the transonic regime
	+ Fixed serious issue where long, skinny vehicles would have incorrect 2nd derivatives and incorrect transonic drag as a result
	+ Fixed NRE with Launch Clamps
	+ Fixed NRE with animations that are removed from a part (for whatever reason)
	+ Cleaned up unused values from FARAeroData.cfg
	+ Added support for planets to be identified by planet name, not just index; combining these in a FARAeroData MM patch is likely to cause overwrites, don't do it
	+ Added ability to read and set flap and spoiler states from FARAPI
	+ Fixed Firespitter gear not responding to Toggle Gear button
	+ Added support for adjustable landing gear in Toggle Gear button
	+ Stopgap fix to unintended voxelization of USI Warp Drive bubbles
* 2015-0622: 0.15_3_1_Garabedian (ferram4) for KSP 1.0.0
	+ Compatibility with KSP v1.0.3 and thermal changes
	+ Fix one last error with voxelization breaking due to reverts
	+ Add ability to make parts not count for main axis determination; fix structural panels interfering with proper main axis determination
* 2015-0619: 0.15_3_Froude (ferram4) for KSP 1.0.0
	+ Update to MM 2.6.5 for greater nyan nyan
	+ Allow display of pressure coefficient (under assumption of axisymmetric flow) over the vehicle
	+ Tweak subsonic drag to be lower for slender shapes
	+ Fixed voxelization breaking due to combined memory leak + hard memory limit for voxelization after many editor -> flight cycles
	+ Fixed some race conditions in voxelization that could break aero properties
	+ Fixed deadlock in threadpool if many voxelization events triggered simultaneously
	+ Fixed possibility of deadlock if voxelization settings were updated
	+ Fixed voxelization errors for some cargo bays and other parts
	+ Fixed voxelization errors for pWings; includes support for any parts making use of mirrorAxis
	+ Fixed some longstanding wing interaction issues, including permanent stalled wings
	+ Fixed a newer issue with wing shielding on symmetry counterparts
	+ Some main axis determination improvements
	+ Fixed an where certain user atmospheric settings would not take
* 2015-0520: 0.15_2_Ferri (ferram4) for KSP 1.0.0
	+ Improved voxelization accuracy
	+ Changed CoL code again to try and make it more useful
	+ Cleaned up some unnecessary calculations
	+ Fixed voxelization breaking after many voxelization events; this fixes no-drag situations
	+ Fixed deployed spoilers not producing drag if mounted flush with vehicle
	+ Fixed some main axis issues
	+ Fixed improper heating area for atmospheric heat
	+ Fixed interaction with KIS breaking things
	+ Fixed some data not saving
	+ Fixed exceptions during EVA
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
