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
    [Display("FrequencySetting", Group: "PXA_Demo", Description: "Insert a description here")]
    public class FrequencySetting : TestStep
    {
        #region Settings
        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("CenterFreq", "", "Input Parameters", 2)]
        public double CenterFreq { get; set; } = 10000000D;

        [DisplayAttribute("StartFreq", "", "Input Parameters", 2)]
        public double StartFreq { get; set; } = 10D;

        [DisplayAttribute("StopFreq", "", "Input Parameters", 2)]
        public double StopFreq { get; set; } = 1000D;

        [DisplayAttribute("FreqSpan", "", "Input Parameters", 2)]
        public double FreqSpan { get; set; } = 3.6D;

        #endregion

        public FrequencySetting()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.FrequencySetting(CenterFreq, StartFreq, StopFreq, FreqSpan);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
