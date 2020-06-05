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
    #region enum

    public enum EMarkerMode { POSition, DELTa, FIXed }

    #endregion

    [Display("Marker", Group: "PXA_Demo", Description: "Insert a description here")]
    public class Marker : TestStep
    {
        #region Settings
        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("MarkerNo", "", "Input Parameters", 2)]
        public uint MarkerNo { get; set; } = 1u;


        [DisplayAttribute("MarkerMode", "POSition, DELTa, FIXed", "Input Parameters", 2)]
        public EMarkerMode MarkerMode { get; set; } = EMarkerMode.POSition;

        [DisplayAttribute("MarkerTableStatus", "", "Input Parameters", 2)]
        public bool MarkerTableStatus { get; set; } = true;

        [DisplayAttribute("TraceNo", "1-6", "Input Parameters", 2)]
        public int TraceNo { get; set; } = 1;

        [DisplayAttribute("MarkerFreq", "", "Input Parameters", 2)]
        public double MarkerFreq { get; set; } = 10D;

        #endregion

        public Marker()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.Marker( MarkerNo, MarkerMode, MarkerTableStatus, TraceNo, MarkerFreq);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
