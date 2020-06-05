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
    [Display("SweepConfigure", Group: "PXA_Demo", Description: "Insert a description here")]
    public class SweepConfigure : TestStep
    {
        #region Settings
        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("SweepTime", "", "Input Parameters", 2)]
        public double SweepTime { get; set; } = 100D;

        [DisplayAttribute("SweepPoints", "", "Input Parameters", 2)]
        public int SweepPoints { get; set; } = 101;
        #endregion

        public SweepConfigure()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.SweepConfigure( SweepPoints, SweepTime);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }

    }
}
