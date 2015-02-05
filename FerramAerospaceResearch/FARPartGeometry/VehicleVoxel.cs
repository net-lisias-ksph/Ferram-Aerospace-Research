﻿/*
Ferram Aerospace Research v0.14.6
Copyright 2014, Michael Ferrara, aka Ferram4

    This file is part of Ferram Aerospace Research.

    Ferram Aerospace Research is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Ferram Aerospace Research is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Ferram Aerospace Research.  If not, see <http://www.gnu.org/licenses/>.

    Serious thanks:		a.g., for tons of bugfixes and code-refactorings
            			Taverius, for correcting a ton of incorrect values
            			sarbian, for refactoring code for working with MechJeb, and the Module Manager 1.5 updates
            			ialdabaoth (who is awesome), who originally created Module Manager
                        Regex, for adding RPM support
            			Duxwing, for copy editing the readme
 * 
 * Kerbal Engineer Redux created by Cybutek, Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License
 *      Referenced for starting point for fixing the "editor click-through-GUI" bug
 *
 * Part.cfg changes powered by sarbian & ialdabaoth's ModuleManager plugin; used with permission
 *	http://forum.kerbalspaceprogram.com/threads/55219
 *
 * Toolbar integration powered by blizzy78's Toolbar plugin; used with permission
 *	http://forum.kerbalspaceprogram.com/threads/60863
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace FerramAerospaceResearch.FARPartGeometry
{
    public class VehicleVoxel
    {
        float elementSize;
        float invElementSize;
        VoxelSection[, ,] voxelSections;
        int xLength, yLength, zLength;
        int itemsQueued = 0;
        object _locker = new object();
        Vector3 lowerRightCorner;

        public VehicleVoxel(List<Part> partList, int elementCount, bool multiThreaded)
        {
            Bounds vesselBounds = new Bounds();
            List<GeometryPartModule> geoModules = new List<GeometryPartModule>();
            for(int i = 0; i < partList.Count; i++)
            {
                Part p = partList[i];
                GeometryPartModule m = p.GetComponent<GeometryPartModule>();
                if (m != null)
                {
                    vesselBounds.Encapsulate(m.overallMeshBounds);
                    geoModules.Add(m);
                }
            }

            float voxelVolume = vesselBounds.size.x * vesselBounds.size.y * vesselBounds.size.z;
            float elementVol = voxelVolume / (float)elementCount;
            elementSize = (float)Math.Pow(elementVol, 1f / 3f);
            invElementSize = 1 / elementSize;

            float tmp = 0.125f * invElementSize;

            xLength = (int)Math.Ceiling(vesselBounds.size.x * tmp);
            yLength = (int)Math.Ceiling(vesselBounds.size.y * tmp);
            zLength = (int)Math.Ceiling(vesselBounds.size.z * tmp);

            Debug.Log(elementSize);
            Debug.Log(xLength + " " + yLength + " " + zLength);
            Debug.Log(vesselBounds);

            lowerRightCorner = vesselBounds.min;

            voxelSections = new VoxelSection[xLength, yLength, zLength];

            for(int i = 0; i < geoModules.Count; i++)
            {
                GeometryPartModule m = geoModules[i];
                for (int j = 0; j < m.geometryMeshes.Count; j++)
                {
                    if (multiThreaded)
                    {
                        WorkData data = new WorkData(m.part, m.geometryMeshes[j], m.meshToVesselMatrixList[j]);
                        ThreadPool.QueueUserWorkItem(UpdateFromMesh, data);
                    }
                    else
                        UpdateFromMesh(m.geometryMeshes[j], m.part, m.meshToVesselMatrixList[j]);
                    itemsQueued++;
                }
            }
            System.Threading.ThreadPriority currentPrio = Thread.CurrentThread.Priority;
            Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            while (itemsQueued > 0)
            {
                Thread.Sleep(15);
            }
            Thread.CurrentThread.Priority = currentPrio;
            try
            {

                //SolidifyVoxel();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        ~VehicleVoxel()
        {
            ClearVisualVoxels();
        }

        private void SetVoxelSection(int i, int j, int k, Part part)
        {
            int iSec, jSec, kSec;
            //Find the voxel section that this point points to
            iSec = i / 8;
            jSec = j / 8;
            kSec = k / 8;
            VoxelSection section;

            lock (voxelSections)
            {
                section = voxelSections[iSec, jSec, kSec];
                if (section == null)
                {
                    section = new VoxelSection(elementSize, 8, 8, 8, lowerRightCorner + new Vector3(iSec, jSec, kSec) * elementSize * 8);
                    voxelSections[iSec, jSec, kSec] = section;
                }
            }

            //Debug.Log(i.ToString() + ", " + j.ToString() + ", " + k.ToString() + ", " + part.partInfo.title);

            section.SetVoxelPoint(i % 8, j % 8, k % 8, part);
        }

        private Part GetPartAtVoxelPos(int i, int j, int k)
        {
            int iSec, jSec, kSec;
            //Find the voxel section that this point points to
            iSec = i / 8;
            jSec = j / 8;
            kSec = k / 8;

            VoxelSection section;
            lock (voxelSections)
            {
                section = voxelSections[iSec, jSec, kSec];
            }
            if (section == null)
                return null;

            return section.GetVoxelPoint(i % 8, j % 8, k % 8);
        }

        private void UpdateFromMesh(object stuff)
        {
            WorkData data = (WorkData)stuff;
            Part part = data.part;
            Mesh mesh = data.mesh;
            Matrix4x4 transform = data.transform;
            UpdateFromMesh(mesh, part, transform);
        }
        private void UpdateFromMesh(Mesh mesh, Part part, Matrix4x4 transform)
        {
            Vector3[] vertsVoxelSpace = new Vector3[mesh.vertices.Length];
            Bounds meshBounds = new Bounds();
            for (int i = 0; i < vertsVoxelSpace.Length; i++)
            {
                Vector3 vert = transform.MultiplyPoint3x4(mesh.vertices[i]);
                meshBounds.Encapsulate(vert);
                vertsVoxelSpace[i] = vert;
            }

            if (meshBounds.size.x < elementSize && meshBounds.size.y < elementSize && meshBounds.size.z < elementSize)
            {
                CalculateVoxelShellFromTinyMesh(ref meshBounds, ref part);
                return;
            }

            float rc = 0.5f;

            for (int a = 0; a < mesh.triangles.Length; a += 3)
            {
                Vector3 vert1, vert2, vert3;

                vert1 = vertsVoxelSpace[mesh.triangles[a]];
                vert2 = vertsVoxelSpace[mesh.triangles[a + 1]];
                vert3 = vertsVoxelSpace[mesh.triangles[a + 2]];


                CalculateVoxelShellForTriangle(ref vert1, ref vert2, ref vert3, ref rc, ref part);
            }

            lock (_locker)
            {
                itemsQueued -= 1;
            }
        }

        private void CalculateVoxelShellFromTinyMesh(ref Bounds meshBounds, ref Part part)
        {
            int lowerI, lowerJ, lowerK;
            int upperI, upperJ, upperK;

            Vector3 min, max;
            min = (meshBounds.min - lowerRightCorner) * invElementSize;
            max = (meshBounds.max - lowerRightCorner) * invElementSize;

            lowerI = (int)Math.Floor(min.x);
            lowerJ = (int)Math.Floor(min.y);
            lowerK = (int)Math.Floor(min.z);

            upperI = (int)Math.Ceiling(max.x);
            upperJ = (int)Math.Ceiling(max.y);
            upperK = (int)Math.Ceiling(max.z);

            lowerI = Math.Max(lowerI, 0);
            lowerJ = Math.Max(lowerJ, 0);
            lowerK = Math.Max(lowerK, 0);

            upperI = Math.Min(upperI, xLength * 8 - 1);
            upperJ = Math.Min(upperJ, yLength * 8 - 1);
            upperK = Math.Min(upperK, zLength * 8 - 1);

            for (int i = lowerI; i <= upperI; i++)
                for (int j = lowerJ; j <= upperJ; j++)
                    for (int k = lowerK; k <= upperK; k++)
                    {
                        SetVoxelSection(i, j, k, part);
                    }
        }

        private void CalculateVoxelShellForTriangle(ref Vector3 vert1, ref Vector3 vert2, ref Vector3 vert3, ref float rc, ref Part part)
        {
            Vector4 plane = CalculateEquationOfPlane(ref vert1, ref vert2, ref vert3);

            float x, y, z;
            x = Math.Abs(plane.x);
            y = Math.Abs(plane.y);
            z = Math.Abs(plane.z);

            Vector4 indexPlane = TransformPlaneToIndices(plane);

            if (x > y && x > z)
                VoxelShellTrianglePerpX(ref indexPlane, ref vert1, ref vert2, ref vert3, ref rc, ref part);
            else if(y > x && y > z)
                VoxelShellTrianglePerpY(ref indexPlane, ref vert1, ref vert2, ref vert3, ref rc, ref part);
            else
                VoxelShellTrianglePerpZ(ref indexPlane, ref vert1, ref vert2, ref vert3, ref rc, ref part);

            #region OldCode
            /*//thickness for calculating a 26 neighborhood around the plane
            float t26 = ThicknessForVoxel(ref plane);

            Vector3 lowerBound = (triBounds.min - lowerRightCorner - new Vector3(rc, rc, rc)) / elementSize;
            Vector3 upperBound = (triBounds.max - lowerRightCorner + new Vector3(rc, rc, rc)) / elementSize;

            int lowerI, lowerJ, lowerK;
            int upperI, upperJ, upperK;

            lowerI = (int)Math.Floor(lowerBound.x);
            lowerJ = (int)Math.Floor(lowerBound.y);
            lowerK = (int)Math.Floor(lowerBound.z);

            upperI = (int)Math.Ceiling(upperBound.x);
            upperJ = (int)Math.Ceiling(upperBound.y);
            upperK = (int)Math.Ceiling(upperBound.z);

            lowerI = Math.Max(lowerI, 0);
            lowerJ = Math.Max(lowerJ, 0);
            lowerK = Math.Max(lowerK, 0);

            upperI = Math.Min(upperI, xLength * 8 - 1);
            upperJ = Math.Min(upperJ, yLength * 8 - 1);
            upperK = Math.Min(upperK, zLength * 8 - 1);

            for (int i = lowerI; i <= upperI; i++)
                for (int j = lowerJ; j <= upperJ; j++)
                    for (int k = lowerK; k <= upperK; k++)
                    {
                        Vector3 pt = lowerRightCorner + new Vector3(i, j, k) * elementSize;

                        /*if (CheckAndSetForPlane(ref pt, ref t26, ref plane))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }

                        if (CheckAndSetForVert(ref pt, ref rc, ref vert1))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }
                        if (CheckAndSetForVert(ref pt, ref rc, ref vert2))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }
                        if (CheckAndSetForVert(ref pt, ref rc, ref vert3))
                        { 
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }

                        if (CheckAndSetForEdge(ref pt, ref rc, ref vert1, ref vert2))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }
                        if (CheckAndSetForEdge(ref pt, ref rc, ref vert2, ref vert3))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }
                        if (CheckAndSetForEdge(ref pt, ref rc, ref vert3, ref vert1))
                        {
                            SetVoxelSection(i, j, k, part);
                            continue;
                        }
                    }*/
            #endregion
        }

        private void VoxelShellTrianglePerpX(ref Vector4 indexPlane, ref Vector3 vert1, ref Vector3 vert2, ref Vector3 vert3, ref float rc, ref Part part)
        {
            Vector2 vert1Proj, vert2Proj, vert3Proj;
            vert1Proj = new Vector2(vert1.y - lowerRightCorner.y, vert1.z - lowerRightCorner.z) * invElementSize;
            vert2Proj = new Vector2(vert2.y - lowerRightCorner.y, vert2.z - lowerRightCorner.z) * invElementSize;
            vert3Proj = new Vector2(vert3.y - lowerRightCorner.y, vert3.z - lowerRightCorner.z) * invElementSize;

            #region EdgeOffset
            /*
            Vector2 sideExt12, sideExt23, sideExt31;
            float tmp;

            sideExt12 = vert2Proj - vert1Proj;
            tmp = sideExt12.x;
            sideExt12.x = -sideExt12.y;
            sideExt12.y = tmp;
            sideExt12.Normalize();

            sideExt12 *= -Math.Sign(Vector2.Dot(sideExt12, vert3Proj - vert1Proj));

            sideExt23 = vert3Proj - vert2Proj;
            tmp = sideExt23.x;
            sideExt23.x = -sideExt23.y;
            sideExt23.y = tmp;
            sideExt23.Normalize();

            sideExt23 *= -Math.Sign(Vector2.Dot(sideExt23, vert1Proj - vert2Proj));

            sideExt31 = vert1Proj - vert3Proj;
            tmp = sideExt23.x;
            sideExt31.x = -sideExt31.y;
            sideExt31.y = tmp;
            sideExt31.Normalize();

            sideExt31 *= -Math.Sign(Vector2.Dot(sideExt31, vert2Proj - vert3Proj));

            vert1Proj += (sideExt12 + sideExt31) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt31));
            vert2Proj += (sideExt23 + sideExt12) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt23));
            vert3Proj += (sideExt31 + sideExt23) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt23, sideExt31));
            */
            #endregion

            Vector2 p1p2, p1p3;
            p1p2 = vert2Proj - vert1Proj;
            p1p3 = vert3Proj - vert1Proj;

            float dot12_12, dot12_13, dot13_13;
            dot12_12 = Vector2.Dot(p1p2, p1p2);
            dot12_13 = Vector2.Dot(p1p2, p1p3);
            dot13_13 = Vector2.Dot(p1p3, p1p3);

            float invDenom = 1 / (dot12_12 * dot13_13 - dot12_13 * dot12_13);

            int lowJ, highJ, lowK, highK;
            lowJ = (int)Math.Min(vert1Proj.x, Math.Min(vert2Proj.x, vert3Proj.x));
            highJ = (int)Math.Ceiling(Math.Max(vert1Proj.x, Math.Max(vert2Proj.x, vert3Proj.x)));
            lowK = (int)Math.Min(vert1Proj.y, Math.Min(vert2Proj.y, vert3Proj.y));
            highK = (int)Math.Ceiling(Math.Max(vert1Proj.y, Math.Max(vert2Proj.y, vert3Proj.y)));


            lowJ = Math.Max(lowJ, 0);
            lowK = Math.Max(lowK, 0);
            highJ = Math.Min(highJ, yLength * 8 - 1);
            highK = Math.Min(highK, zLength * 8 - 1);

            for (int j = lowJ; j <= highJ; j++)
                for (int k = lowK; k <= highK; k++)
                {
                    Vector2 pt = new Vector2(j, k);
                    Vector2 p1TestPt = pt - vert1Proj;
                    float dot12_test, dot13_test;
                    dot12_test = Vector2.Dot(p1p2, p1TestPt);
                    dot13_test = Vector2.Dot(p1p3, p1TestPt);

                    float u, v;
                    u = (dot13_13 * dot12_test - dot12_13 * dot13_test) * invDenom;
                    v = (dot12_12 * dot13_test - dot12_13 * dot12_test) * invDenom;

                    if (u >= 0 && v >= 0 && u + v <= 1)
                    {
                        int i = (int)Math.Round(-(indexPlane.y * j + indexPlane.z * k + indexPlane.w) / indexPlane.x);
                        if (i < 0 || i >= xLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);
                        continue;
                    }

                    Vector2 p2TestPt = pt - vert2Proj;
                    Vector2 p3TestPt = pt - vert3Proj;
                    if (p1TestPt.magnitude < rc || p2TestPt.magnitude < rc || p3TestPt.magnitude < rc)
                    {
                        int i = (int)Math.Round(-(indexPlane.y * j + indexPlane.z * k + indexPlane.w) / indexPlane.x);
                        if (i < 0 || i >= xLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);
                        continue;
                    }

                    if ((u >= 0 && u <= 1 && Vector3.Exclude(p1p2, p1TestPt).magnitude < rc) ||
                        (v >= 0 && v <= 1 && Vector3.Exclude(p1p3, p1TestPt).magnitude < rc) ||
                        (u >= 0 && v >= 0 && Vector3.Exclude(vert3Proj - vert2Proj, p2TestPt).magnitude < rc))
                    {
                        int i = (int)Math.Round(-(indexPlane.y * j + indexPlane.z * k + indexPlane.w) / indexPlane.x);
                        if (i < 0 || i >= xLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);

                    }
                }
        }

        private void VoxelShellTrianglePerpY(ref Vector4 indexPlane, ref Vector3 vert1, ref Vector3 vert2, ref Vector3 vert3, ref float rc, ref Part part)
        {
            Vector2 vert1Proj, vert2Proj, vert3Proj;
            vert1Proj = new Vector2(vert1.x - lowerRightCorner.x, vert1.z - lowerRightCorner.z) * invElementSize;
            vert2Proj = new Vector2(vert2.x - lowerRightCorner.x, vert2.z - lowerRightCorner.z) * invElementSize;
            vert3Proj = new Vector2(vert3.x - lowerRightCorner.x, vert3.z - lowerRightCorner.z) * invElementSize;

            #region EdgeOffset
            /*
            Vector2 sideExt12, sideExt23, sideExt31;
            float tmp;

            sideExt12 = vert2Proj - vert1Proj;
            tmp = sideExt12.x;
            sideExt12.x = -sideExt12.y;
            sideExt12.y = tmp;
            sideExt12.Normalize();

            if (Vector2.Dot(sideExt12, vert3Proj - vert1Proj) > 0)
                sideExt12 = -sideExt12;

            sideExt23 = vert3Proj - vert2Proj;
            tmp = sideExt23.x;
            sideExt23.x = -sideExt23.y;
            sideExt23.y = tmp;
            sideExt23.Normalize();

            if (Vector2.Dot(sideExt23, vert1Proj - vert2Proj) > 0)
                sideExt23 = -sideExt23;

            sideExt31 = vert1Proj - vert3Proj;
            tmp = sideExt23.x;
            sideExt31.x = -sideExt31.y;
            sideExt31.y = tmp;
            sideExt31.Normalize();

            if (Vector2.Dot(sideExt31, vert2Proj - vert3Proj) > 0)
                sideExt31 = -sideExt31;

            vert1Proj += (sideExt12 + sideExt31) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt31));
            vert2Proj += (sideExt23 + sideExt12) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt23));
            vert3Proj += (sideExt31 + sideExt23) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt23, sideExt31));
            */
            #endregion
            
            Vector2 p1p2, p1p3;
            p1p2 = vert2Proj - vert1Proj;
            p1p3 = vert3Proj - vert1Proj;

            float dot12_12, dot12_13, dot13_13;
            dot12_12 = Vector2.Dot(p1p2, p1p2);
            dot12_13 = Vector2.Dot(p1p2, p1p3);
            dot13_13 = Vector2.Dot(p1p3, p1p3);

            float invDenom = 1 / (dot12_12 * dot13_13 - dot12_13 * dot12_13);

            int lowI, highI, lowK, highK;
            lowI = (int)Math.Min(vert1Proj.x, Math.Min(vert2Proj.x, vert3Proj.x));
            highI = (int)Math.Ceiling(Math.Max(vert1Proj.x, Math.Max(vert2Proj.x, vert3Proj.x)));
            lowK = (int)Math.Min(vert1Proj.y, Math.Min(vert2Proj.y, vert3Proj.y));
            highK = (int)Math.Ceiling(Math.Max(vert1Proj.y, Math.Max(vert2Proj.y, vert3Proj.y)));


            lowI = Math.Max(lowI, 0);
            lowK = Math.Max(lowK, 0);
            highI = Math.Min(highI, xLength * 8 - 1);
            highK = Math.Min(highK, zLength * 8 - 1);

            for (int i = lowI; i <= highI; i++)
                for (int k = lowK; k <= highK; k++)
                {
                    Vector2 pt = new Vector2(i, k);
                    Vector2 p1TestPt = pt - vert1Proj;
                    float dot12_test, dot13_test;
                    dot12_test = Vector2.Dot(p1p2, p1TestPt);
                    dot13_test = Vector2.Dot(p1p3, p1TestPt);

                    float u, v;
                    u = (dot13_13 * dot12_test - dot12_13 * dot13_test) * invDenom;
                    v = (dot12_12 * dot13_test - dot12_13 * dot12_test) * invDenom;

                    if (u >= 0 && v >= 0 && u + v <= 1)
                    {
                        int j = (int)Math.Round(-(indexPlane.x * i + indexPlane.z * k + indexPlane.w) / indexPlane.y);
                        if (j < 0 || j >= yLength * 8)
                            continue;
                        SetVoxelSection(i, j, k, part);
                        continue;
                    }
                    Vector2 p2TestPt = pt - vert2Proj;
                    Vector2 p3TestPt = pt - vert3Proj;
                    if (p1TestPt.magnitude < rc || p2TestPt.magnitude < rc || p3TestPt.magnitude < rc)
                    {
                        int j = (int)Math.Round(-(indexPlane.x * i + indexPlane.z * k + indexPlane.w) / indexPlane.y);
                        if (j < 0 || j >= yLength * 8)
                            continue;
                        SetVoxelSection(i, j, k, part);
                        continue;
                    }

                    if ((u >= 0 && u <= 1 && Vector3.Exclude(p1p2, p1TestPt).magnitude < rc) ||
                        (v >= 0 && v <= 1 && Vector3.Exclude(p1p3, p1TestPt).magnitude < rc) ||
                        (u >= 0 && v >= 0 && Vector3.Exclude(vert3Proj - vert2Proj, p2TestPt).magnitude < rc))
                    {
                        int j = (int)Math.Round(-(indexPlane.x * i + indexPlane.z * k + indexPlane.w) / indexPlane.y);
                        if (j < 0 || j >= yLength * 8)
                            continue;
                        SetVoxelSection(i, j, k, part);
                    }
                }
            #region Theoretically more efficient code, but requires bugfixing
            /*int lowerI = startI, upperI = startI;
            int i = startI, k = startK;

            bool incrementI = true;
            bool incrementK = true;
            while (true)
            {
                Vector2 p1TestPt = new Vector2(i, k) - vert1Proj;
                float dot12_test, dot13_test;
                dot12_test = Vector2.Dot(p1p2, p1TestPt);
                dot13_test = Vector2.Dot(p1p3, p1TestPt);

                float u, v;
                u = (dot13_13 * dot12_test - dot12_13 * dot13_test) * invDenom;
                v = (dot12_12 * dot13_test - dot12_13 * dot12_test) * invDenom;

                if (u >= 0 && v >= 0 && u + v < 1)
                {
                    int j = (int)Math.Round(-(indexPlane.x * i + indexPlane.z * k + indexPlane.w) / indexPlane.y);
                    SetVoxelSection(i, j, k, part);
                    if (incrementI)
                    {
                        i++;
                        upperI = i;
                    }
                    else
                    {
                        i--;
                        lowerI = i;
                    }
                }
                else if (incrementI)
                {
                    incrementI = false;
                    i = startI;
                }
                else
                {
                    incrementI = true;
                    startI = (lowerI + upperI) / 2;
                    if (lowerI == upperI)
                    {
                        if (incrementK)
                        {
                            incrementK = false;
                            k = startK;
                            lowerI = startI;
                            upperI = startI;
                            continue;
                        }
                        else
                            break;
                    }
                    lowerI = startI;
                    upperI = startI;

                    if (incrementK)
                        k++;
                    else
                        k--;
                }
            }*/
            #endregion
        }

        private void VoxelShellTrianglePerpZ(ref Vector4 indexPlane, ref Vector3 vert1, ref Vector3 vert2, ref Vector3 vert3, ref float rc, ref Part part)
        {
            Vector2 vert1Proj, vert2Proj, vert3Proj;
            vert1Proj = new Vector2(vert1.x - lowerRightCorner.x, vert1.y - lowerRightCorner.y) * invElementSize;
            vert2Proj = new Vector2(vert2.x - lowerRightCorner.x, vert2.y - lowerRightCorner.y) * invElementSize;
            vert3Proj = new Vector2(vert3.x - lowerRightCorner.x, vert3.y - lowerRightCorner.y) * invElementSize;

            #region EdgeOffset
            /*
            Vector2 sideExt12, sideExt23, sideExt31;
            float tmp;

            sideExt12 = vert2Proj - vert1Proj;
            tmp = sideExt12.x;
            sideExt12.x = -sideExt12.y;
            sideExt12.y = tmp;
            sideExt12.Normalize();

            sideExt12 *= -Math.Sign(Vector2.Dot(sideExt12, vert3Proj - vert1Proj));

            sideExt23 = vert3Proj - vert2Proj;
            tmp = sideExt23.x;
            sideExt23.x = -sideExt23.y;
            sideExt23.y = tmp;
            sideExt23.Normalize();

            sideExt23 *= -Math.Sign(Vector2.Dot(sideExt23, vert1Proj - vert2Proj));

            sideExt31 = vert1Proj - vert3Proj;
            tmp = sideExt23.x;
            sideExt31.x = -sideExt31.y;
            sideExt31.y = tmp;
            sideExt31.Normalize();

            sideExt31 *= -Math.Sign(Vector2.Dot(sideExt31, vert2Proj - vert3Proj));

            vert1Proj += (sideExt12 + sideExt31) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt31));
            vert2Proj += (sideExt23 + sideExt12) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt12, sideExt23));
            vert3Proj += (sideExt31 + sideExt23) * rc * TriVertexShiftFactor(Vector2.Dot(sideExt23, sideExt31));
            */
            #endregion

            Vector2 p1p2, p1p3;
            p1p2 = vert2Proj - vert1Proj;
            p1p3 = vert3Proj - vert1Proj;

            float dot12_12, dot12_13, dot13_13;
            dot12_12 = Vector2.Dot(p1p2, p1p2);
            dot12_13 = Vector2.Dot(p1p2, p1p3);
            dot13_13 = Vector2.Dot(p1p3, p1p3);

            float invDenom = 1 / (dot12_12 * dot13_13 - dot12_13 * dot12_13);

            int lowI, highI, lowJ, highJ;
            lowI = (int)Math.Min(vert1Proj.x, Math.Min(vert2Proj.x, vert3Proj.x));
            highI = (int)Math.Ceiling(Math.Max(vert1Proj.x, Math.Max(vert2Proj.x, vert3Proj.x)));
            lowJ = (int)Math.Min(vert1Proj.y, Math.Min(vert2Proj.y, vert3Proj.y));
            highJ = (int)Math.Ceiling(Math.Max(vert1Proj.y, Math.Max(vert2Proj.y, vert3Proj.y)));


            lowI = Math.Max(lowI, 0);
            lowJ = Math.Max(lowJ, 0);
            highI = Math.Min(highI, xLength * 8 - 1);
            highJ = Math.Min(highJ, yLength * 8 - 1);

            for (int i = lowI; i <= highI; i++)
                for (int j = lowJ; j <= highJ; j++)
                {
                    Vector2 pt = new Vector2(i, j);
                    Vector2 p1TestPt = pt - vert1Proj;
                    float dot12_test, dot13_test;
                    dot12_test = Vector2.Dot(p1p2, p1TestPt);
                    dot13_test = Vector2.Dot(p1p3, p1TestPt);

                    float u, v;
                    u = (dot13_13 * dot12_test - dot12_13 * dot13_test) * invDenom;
                    v = (dot12_12 * dot13_test - dot12_13 * dot12_test) * invDenom;

                    if (u >= 0 && v >= 0 && u + v <= 1)
                    {
                        int k = (int)Math.Round(-(indexPlane.x * i + indexPlane.y * j + indexPlane.w) / indexPlane.z);
                        if (k < 0 || k >= zLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);
                        continue;
                    }
                    Vector2 p2TestPt = pt - vert2Proj;
                    Vector2 p3TestPt = pt - vert3Proj;
                    if (p1TestPt.magnitude < rc || p2TestPt.magnitude < rc || p3TestPt.magnitude < rc)
                    {
                        int k = (int)Math.Round(-(indexPlane.x * i + indexPlane.y * j + indexPlane.w) / indexPlane.z);
                        if (k < 0 || k >= zLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);
                        continue;
                    }

                    if ((u >= 0 && u <= 1 && Vector3.Exclude(p1p2, p1TestPt).magnitude < rc) ||
                        (v >= 0 && v <= 1 && Vector3.Exclude(p1p3, p1TestPt).magnitude < rc) ||
                        (u >= 0 && v >= 0 && Vector3.Exclude(vert3Proj - vert2Proj, p2TestPt).magnitude < rc))
                    {
                        int k = (int)Math.Round(-(indexPlane.x * i + indexPlane.y * j + indexPlane.w) / indexPlane.z);
                        if (k < 0 || k >= zLength * 8)
                            continue;

                        SetVoxelSection(i, j, k, part);
                    }
                }
        }

        private Vector4 CalculateEquationOfPlane(ref Vector3 pt1, ref Vector3 pt2, ref Vector3 pt3)
        {
            Vector3 p1p2 = pt2 - pt1;
            Vector3 p1p3 = pt3 - pt1;

            Vector3 tmp = Vector3.Cross(p1p2, p1p3).normalized;

            Vector4 result = new Vector4(tmp.x, tmp.y, tmp.z);

            result.w = -(pt1.x * result.x + pt1.y * result.y + pt1.z * result.z);

            return result;
        }

        private Vector4 TransformPlaneToIndices(Vector4 plane)
        {
            Vector4 newPlane = new Vector4();
            newPlane.x = plane.x * elementSize;
            newPlane.y = plane.y * elementSize;
            newPlane.z = plane.z * elementSize;
            newPlane.w = plane.w + plane.x * lowerRightCorner.x + plane.y * lowerRightCorner.y + plane.z * lowerRightCorner.z;

            return newPlane;
        }

        private float TriVertexShiftFactor(float dotProdVector)
        {
            float result = (float)(Math.Cos(0.5 * Math.Acos(dotProdVector)));
            result *= result;
            result = 0.5f / result;
            return result;
        }

        public float[] CrossSectionalArea(Vector3 orientation)
        {
            float[] crossSections = new float[yLength * 8];
            float areaPerElement = elementSize * elementSize;
            for (int j = 0; j < yLength * 8; j++)
            {
                float area = 0;
                for (int i = 0; i < xLength * 8; i++)
                    for (int k = 0; k < zLength * 8; k++)
                    {
                        if (GetPartAtVoxelPos(i, j, k) != null)
                            area += areaPerElement;
                    }
                Debug.Log(area);
                crossSections[j] = area;
            }
            return crossSections;
        }

        public void ClearVisualVoxels()
        {
            for (int i = 0; i < xLength; i++)
                for (int j = 0; j < yLength; j++)
                    for (int k = 0; k < zLength; k++)
                    {
                        VoxelSection section = voxelSections[i, j, k];
                        if (section != null)
                        {
                            section.ClearVisualVoxels();
                        }
                    }
        }

        public void VisualizeVoxel(Vector3 vesselOffset)
        {
            for(int i = 0; i < xLength; i++)
                for(int j = 0; j < yLength; j++)
                    for(int k = 0; k < zLength; k++)
                    {
                        VoxelSection section = voxelSections[i, j, k];
                        if(section != null)
                        {
                            section.VisualizeVoxels(vesselOffset);
                        }
                    }
        }

        private void SolidifyVoxel()
        {
            SweepPlanePoint[,] sweepPlane = new SweepPlanePoint[xLength * 8, zLength * 8];
            List<SweepPlanePoint> activePts = new List<SweepPlanePoint>();
            List<SweepPlanePoint> inactiveInteriorPts = new List<SweepPlanePoint>();
            SweepPlanePoint[] neighboringSweepPlanePts = new SweepPlanePoint[4];

            for (int j = 0; j < yLength * 8; j++)      //Iterate from front of vehicle to back
            {
                for (int i = 0; i < xLength * 8; i++)    //Iterate across the cross-section plane to add voxel shell and mark active interior points
                    for (int k = 0; k < zLength * 8; k++)
                    {
                        Part p = GetPartAtVoxelPos(i, j, k);
                        SweepPlanePoint pt = sweepPlane[i, k];

                        if (pt == null && p != null)        //If there is a section of voxel there, but no pt, add a new voxel shell pt to the sweep plane
                        {
                            pt = new SweepPlanePoint(p, i, k);
                            sweepPlane[i, k] = pt;
                            continue;
                        }
                        else if (pt != null)
                        {
                            if (p == null)                  //If there is a pt there, but no part listed, this is an interior pt or a the cross-section is shrinking
                            {
                                if (pt.mark == SweepPlanePoint.MarkingType.VoxelShell)  //label it as active so that it can be determined once all the points have been checked
                                {
                                    pt.mark = SweepPlanePoint.MarkingType.Active;
                                    activePts.Add(pt);      //And add it to the list of active interior pts
                                }
                            }
                            else
                            {       //Make sure the point is labeled as a voxel shell if there is already a part there
                                pt.mark = SweepPlanePoint.MarkingType.VoxelShell;
                            }
                        }
                    }

                for(int i = 0; i < activePts.Count; i++)    //Then, iterate through all active points for this section
                {
                    SweepPlanePoint activeInteriorPt = activePts[i];    //Get active interior pt

                    if (activeInteriorPt.i + 1 < xLength * 8)           //And all of its 4-neighbors
                        neighboringSweepPlanePts[0] = sweepPlane[activeInteriorPt.i + 1, activeInteriorPt.k];
                    else
                        neighboringSweepPlanePts[0] = null;

                    if (activeInteriorPt.i - 1 >= 0)
                        neighboringSweepPlanePts[1] = sweepPlane[activeInteriorPt.i - 1, activeInteriorPt.k];
                    else
                        neighboringSweepPlanePts[1] = null;

                    if (activeInteriorPt.k + 1 < zLength * 8)
                        neighboringSweepPlanePts[2] = sweepPlane[activeInteriorPt.i, activeInteriorPt.k + 1];
                    else
                        neighboringSweepPlanePts[2] = null;

                    if (activeInteriorPt.k - 1 >= 0)
                        neighboringSweepPlanePts[3] = sweepPlane[activeInteriorPt.i, activeInteriorPt.k - 1];
                    else
                        neighboringSweepPlanePts[3] = null;

                    bool remove = false;
                    for (int m = 0; m < neighboringSweepPlanePts.Length; m++)       //Check if the active point is surrounded by 4 neighbors
                        if (neighboringSweepPlanePts[m] == null)                    //If any of them are null, this active point is larger than the current cross-section
                        {                                                           //In that case, it should be set to be removed
                            remove = true;
                            break;
                        }
                    if(remove)              //If it is set to be removed...
                    {
                        for (int m = 0; m < neighboringSweepPlanePts.Length; m++)   //Go through all the neighboring points
                        {
                            SweepPlanePoint neighbor = neighboringSweepPlanePts[m];
                            if (neighbor != null && neighbor.mark == SweepPlanePoint.MarkingType.InactiveInterior)    //For the ones that exist, and are inactive interior...
                            {
                                neighbor.mark = SweepPlanePoint.MarkingType.Active;         //...mark them active
                                inactiveInteriorPts.Remove(neighbor);                       //remove them from inactiveInterior
                                activePts.Add(neighbor);                                    //And add them to the end of activePts
                            }
                        }
                        sweepPlane[activeInteriorPt.i, activeInteriorPt.k] = null;          //Then, set this point to null in the sweepPlane
                        SetVoxelSection(activeInteriorPt.i, j, activeInteriorPt.k, null);   //Set the point on the voxel to null
                        activePts[i] = null;                                                //And clear it out for this guy
                    }
                    else
                    {           //If it's surrounded by other points, it's inactive; add it to that list
                        activeInteriorPt.mark = SweepPlanePoint.MarkingType.InactiveInterior;
                        inactiveInteriorPts.Add(activeInteriorPt);
                    }
                }
                Debug.Log(activePts.Count + " " + inactiveInteriorPts.Count);
                activePts.Clear();      //Clear activePts every iteration

                for(int i = 0; i < inactiveInteriorPts.Count; i++)      //Any remaining inactive interior pts are guaranteed to be on the inside of the vehicle
                {
                    SweepPlanePoint inactivePt = inactiveInteriorPts[i];        //Get each
                    SetVoxelSection(inactivePt.i, j, inactivePt.k, inactivePt.part);    //And update the voxel accordingly
                }
            }

            //Cleanup
            sweepPlane = null;
            activePts = null;
            inactiveInteriorPts = null;
            neighboringSweepPlanePts = null;
        }

        private class SweepPlanePoint
        {
            public Part part;
            public int i, k;

            public MarkingType mark = MarkingType.VoxelShell;

            public SweepPlanePoint(Part part, int i, int k)
            {
                this.i = i;
                this.k = k;
                this.part = part;
            }

            public enum MarkingType
            {
                VoxelShell,
                Active,
                InactiveInterior
            }
        }

        private class WorkData
        {
            public Part part;
            public Mesh mesh;
            public Matrix4x4 transform;

            public WorkData(Part part, Mesh mesh, Matrix4x4 transform)
            {
                this.part = part;
                this.mesh = mesh;
                this.transform = transform;
            }
        }
    }
}
