// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace PXA_Demo
{
    #region
    public enum EScaleSpacing { LIN, LOG }
    #endregion

    [Display("Display", Group: "PXA_Demo", Description: "Insert a description here")]
    public class Display : TestStep
    {
        #region Settings

        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("state", "", "Input Parameters", 2)]
        public bool state { get; set; } = true;

        [DisplayAttribute("WINDowNo", "", "Input Parameters", 2)]
        public uint WINDowNo { get; set; } = 1u;

        [DisplayAttribute("SelectWindow", ":DISPlay:WINDow 1", "Input Parameters", 2)]
        public int SelectWindow { get; set; } = 1;

        [DisplayAttribute("GridStatus", "", "Input Parameters", 2)]
        public bool GridStatus { get; set; } = true;

        [DisplayAttribute("OffsetFreq", "", "Input Parameters", 2)]
        public double OffsetFreq { get; set; }

        [DisplayAttribute("Dline", "", "Input Parameters", 2)]
        public double Dline { get; set; } = 10D;

        [DisplayAttribute("DlineState", "", "Input Parameters", 2)]
        public bool DlineState { get; set; } = true;

        [DisplayAttribute("NormRefLevel", "", "Input Parameters", 2)]
        public double NormRefLevel { get; set; } = 0.4D;

        [DisplayAttribute("NormRefPositon", "", "Input Parameters", 2)]
        public int NormRefPositon { get; set; } = 5;

        [DisplayAttribute("Pdivision", "Enable when Y scale is LOG", "Input Parameters", 2)]
        public double Pdivision { get; set; } = 5D;

        [DisplayAttribute("YScaleSpacing", "", "Input Parameters", 2)]
        public EScaleSpacing YScaleSpacing { get; set; } = EScaleSpacing.LOG;

        [DisplayAttribute("XScaleSpacing", "LIN, LOG", "Input Parameters", 2)]
        public EScaleSpacing XScaleSpacing { get; set; } = EScaleSpacing.LOG;

        #endregion

        [Display("Zoom In")]
        [Browsable(true)]
        public void Zoom(uint WINDowNo)
        {
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:FORMat:ZOOM", WINDowNo);
            Log.Info("Zoom In Window {0}",WINDowNo);
        }
        [BrowsableAttribute(true)]
        [DisplayAttribute("Zoom Out")]
        public void UnZoom(uint WINDowNo)
        {
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:FORMat:TILE", WINDowNo);
            Log.Info("Zoom Out Window {0}", WINDowNo);
        }

        public Display()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            //MyInst.Display(WINDowNo, state, SelectWindow, GridStatus, OffsetFreq, Dline, DlineState, NormRefLevel, NormRefPositon, Pdivision, YScaleSpacing, XScaleSpacing);

            UpgradeVerdict(Verdict.Pass);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);

            MyInst.ScpiCommand(":DISPlay:WINDow{0}:FORMat:TILE", WINDowNo);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:FORMat:ZOOM", WINDowNo);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:MAMarker:STATe {1}", WINDowNo, state);
            MyInst.ScpiCommand(":DISPlay:WINDow {0}", SelectWindow);
            //MyInst.ScpiCommand(":DISPlay:WINDow{0}:SELect {1}", WINDowNo, SelectWindow);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:GRATicule:GRID:STATe {1}", WINDowNo, GridStatus);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:X:SCALe:OFFSet {1}", WINDowNo, OffsetFreq);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:X:SCALe:SPACing {1}", WINDowNo, XScaleSpacing);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:SPACing {1}", WINDowNo, YScaleSpacing);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:DLINe:STATe {1}", WINDowNo, DlineState);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:DLINe {1}", WINDowNo, Dline);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:NRLevel {1}", WINDowNo, NormRefLevel);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:NRPosition {1}", WINDowNo, NormRefPositon);
            MyInst.ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:PDIVision {1}", WINDowNo, Pdivision);

        }
    }
}
