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
    [Display("Measurement", Group: "PXA_Demo", Description: "Insert a description here")]
    public class Measurement : TestStep
    {
        #region Settings
        private OpenTap.TraceSource LogOutput = OpenTap.Log.CreateSource("Output");

        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        

        [DisplayAttribute("AC Power", "", "Input Parameters", 2.1)]
        public bool enableOutputACP { get; set; }

        [DisplayAttribute("ACPowerNo", "", "Input Parameters", 2.2)]
        [EnabledIf("enableOutputACP", true, HideIfDisabled = true)]
        public uint ACPowerNo { get; set; } = 1u;

        [DisplayAttribute("Burst Power", "", "Input Parameters", 2.3)]
        public bool enableOutputBPower { get; set; }

        [DisplayAttribute("BPOWerNo", "", "Input Parameters", 2.4)]
        [EnabledIf("enableOutputBPower", true, HideIfDisabled = true)]
        public uint BPOWerNo { get; set; } = 1u;

        [DisplayAttribute("Channel Power", "", "Input Parameters", 2.5)]
        public bool enableOutputChPower { get; set; }

        [DisplayAttribute("CHPowerNo", "", "Input Parameters", 2.6)]
        [EnabledIf("enableOutputChPower", true, HideIfDisabled = true)]

        public uint CHPowerNo { get; set; } = 1u;

        private Single[] _OutputACP = { };
        private Single[] _OutputBPower = { };
        private Single[] _OutputChPower = { };
        [DisplayAttribute("AC Power", "", "Outputs Parameters", 3.1)]
        public Single[] OutputACP { get => _OutputACP; }
        [DisplayAttribute("Burst Power", "", "Outputs Parameters", 3.2)]
        public Single[] OutputBPower { get => _OutputBPower; }
        [DisplayAttribute("Channel Power", "", "Outputs Parameters", 3.5)]
        public Single[] OutputChPower { get => _OutputChPower; }
        #endregion

        public Measurement()
        {
            Log.Info("Measurement of Parameters Begins...");
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.Measurements(LogOutput, enableOutputACP, enableOutputBPower, enableOutputChPower, ACPowerNo, BPOWerNo, CHPowerNo, ref _OutputACP, ref _OutputBPower, ref _OutputChPower);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
