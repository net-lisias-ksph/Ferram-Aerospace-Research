using System;
using System.Collections.Generic;
using System.IO;

namespace FerramAerospaceResearch.FARGUI.FAREditorGUI.Simulation
{
    class SweepExportOutputVariable
    {
        public string name;
        public string comment;
        public double[] values;

        public SweepExportOutputVariable(string name, string comment, double[] values)
        {
            this.name = name;
            this.comment = comment;
            this.values = values; // currently just a reference copy
        }
    }


    class SweepExportOutput
    {
        private List<SweepExportOutputVariable> variables;

        public SweepExportOutput()
        {
            variables = new List<SweepExportOutputVariable>();
        }

        public void AddVariableToList(string name, string comment, double[] values)
        {
            variables.Add(new SweepExportOutputVariable(name, comment, values));
        }

        public void AddSizeVariables(InstantConditionSim ics, double pitch, int flapSetting, bool spoilers)
        {
            Vector3d com; double mass, area, mac, b;
            ics.GetCoMAndSize(out com, out mass, out area, out mac, out b);

            AddVariableToList("CoM", "Center of Mass", new double[3] { com.x, com.y, com.z });
            AddVariableToList("mass", "Craft mass (kg)", new double[1] { mass });
            AddVariableToList("area", "Reference area for Cl and Cd (m^2)", new double[1] { area });
            AddVariableToList("mac", "Weighted wing chord (m)", new double[1] { mac });
            AddVariableToList("b", "Weighted wing span (m)", new double[1] { b });
            AddVariableToList("pitch", "Elevator setting, [-1; 1]", new double[1] { pitch });
            AddVariableToList("flapSetting", "Flap setting (0= up, 1-2= take off, 3= landing)", new double[1] { flapSetting });
            AddVariableToList("spoilers", "Spoiler setting (0= retracted, 1= deployed)", new double[1] { spoilers? 1: 0 });
        }

        public void AddAoASweepXVariable(double machNumber, double[] alphavals)
        {
            AddVariableToList("mach", "Mach number", new double[] { machNumber });
            AddVariableToList("x(1,:)", "Angle of attack (deg)", alphavals);
        }

        public void AddMachSweepXVariable(double aoa, double[] alphavals)
        {
            AddVariableToList("aoa", "Angle of attack (deg)", new double[] { aoa });
            AddVariableToList("x(1,:)", "Mach number", alphavals);
        }

        public void AddYVariables(GraphData graphdata)
        {
            for (int j = 0; j < graphdata.yValues.Count; ++j)
            {
                string name = "y(:," + (j + 1) + ")";
                string comment = "Col " + (j + 1) + " is " + graphdata.lineNames[j];
                double[] datavals = graphdata.yValues[j];
                AddVariableToList(name, comment, datavals);
            }
        }

        private const string floatstyleculture = "en-US";
        private const string floatstyleformat = "E16";

        private string[] GetExportOutputLines()
        {
            List<string> lines = new List<string>();
            lines.Add("% SweepData");
            lines.Add("%  Exported data from latest graph update.");

            System.Globalization.CultureInfo enus =
                System.Globalization.CultureInfo.CreateSpecificCulture(floatstyleculture);

            foreach (SweepExportOutputVariable variable in variables)
            {
                lines.Add("");
                lines.Add("% " + variable.comment + ".");
                lines.Add(variable.name + " = [ ...");
                foreach (double value in variable.values)
                    lines.Add("   " + value.ToString(floatstyleformat, enus) + "; ...");
                lines.Add("   ];");
                if (variable.values.Length > 4)
                    lines.Add(""); // add extra space after long data segments
            }

            return lines.ToArray();
        }

        public void Export(bool toMachFile)
        {
            string path = KSPUtil.ApplicationRootPath;
            path += "GameData/FerramAerospaceResearch/Plugins/PluginData/";
            path += toMachFile ? "MachSweepData.m" : "AoASweepData.m";

            string[] lines = GetExportOutputLines();

            File.WriteAllLines(path, lines);
        }
    }
}
