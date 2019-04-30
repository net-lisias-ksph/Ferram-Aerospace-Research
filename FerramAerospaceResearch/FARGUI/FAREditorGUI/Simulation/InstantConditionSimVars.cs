using System;
using System.Collections.Generic;
using UnityEngine;

namespace FerramAerospaceResearch.FARGUI.FAREditorGUI.Simulation
{
    class InstantConditionSimVars
    {
        private InstantConditionSim parent;
        private InstantConditionSimInput iterationInput;
        private InstantConditionSimOutput iterationOutput;
        private double neededCl; // this is the target for level flight
        private Vector3d CoM;

        public InstantConditionSimVars(InstantConditionSim parent, double machNumber, double neededCl, double beta, double phi, int flap, bool spoilers)
        {
            this.parent = parent;
            this.neededCl = neededCl;
            this.CoM = parent.GetCoM();
            iterationInput = new InstantConditionSimInput(0,beta,phi, 0,0,0, machNumber, 0, flap, spoilers);
            iterationOutput = new InstantConditionSimOutput();
        }
    }
}
