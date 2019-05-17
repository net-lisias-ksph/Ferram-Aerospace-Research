using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FerramAerospaceResearch.FARGUI.FAREditorGUI
{
    class StaticAnalysisExportFile
    {
        private System.Globalization.CultureInfo enus;
        private float[] machlist;
        private float[] aoadeglist;
        private List<double[]> datarows;

        private const string floatstyleculture = "en-US";
        private const string floatstyleformat = "E8";

        public StaticAnalysisExportFile()
        {
            enus = System.Globalization.CultureInfo.CreateSpecificCulture(floatstyleculture);
            LoadConfigLists(out machlist, out aoadeglist, enus);
            datarows = new List<Double[]>();
        }

        static public string ConfigFilePath
        {
            get
            {
                string path = KSPUtil.ApplicationRootPath;
                path += "GameData/FerramAerospaceResearch/Plugins/PluginData/FerramAerospaceResearch/";
                path += "saexpcfg.txt";
                return path;
            }
        }

        static public string TextFilePath
        {
            get
            {
                string path = KSPUtil.ApplicationRootPath;
                path += "GameData/FerramAerospaceResearch/Plugins/PluginData/";
                path += "StaticAnalysisSweepDataExport.txt";
                return path;
            }
        }

        public List<float> MachNumberList
        { get { return new List<float>(machlist); } }

        public List<float> AoADegreeList
        { get { return new List<float>(aoadeglist); } }

        public int DataCount
        { get { return datarows.Count; } }

        public void AddDatapoint()
        {
            throw new NotImplementedException("Static analysis export functionality not yet implemented.");
            datarows.Add(new double[] {});
        }

        // Rodhern: TODO Add static methods to help load configuration from file.

        private static void LoadConfigLists(out float[] machlist, out float[] aoadeglist, System.Globalization.CultureInfo enus)
        {
            machlist = new float[0];
            aoadeglist = new float[0];
        }

        private static string Header()
        {
            return "# not yet implemented";
        }

        private static string CSVRow(double[] values, System.Globalization.CultureInfo enus, string format)
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(values[i].ToString(format, enus));
                if (i + 1 < values.Length)
                    sb.Append(";  ");
            }
            return sb.ToString();
        }

        private string[] GetExportOutputLines()
        {
            string[] lines = new string[1 + datarows.Count];
            lines[0] = Header();
            for (int i = 0; i < datarows.Count; i++)
                lines[1 + i] = CSVRow(datarows[i], enus, floatstyleformat);
            return lines;
        }

        public void Export()
        {
            string path = TextFilePath;
            string[] lines = GetExportOutputLines();
            File.WriteAllLines(path, lines);
        }
    }
}
