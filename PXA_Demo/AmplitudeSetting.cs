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
using System.Data.Common;

namespace PXA_Demo
{
    #region enum

    public enum EAmplitudeUnit { DBM, DBMV, DBMA, V, W, A, DBUV, DBUA, DBPW, DBUVM, DBUAM, DBPT, DBG }

    #endregion

    [Display("AmplitudeSetting", Group: "PXA_Demo", Description: "Insert a description here")]
    public class AmplitudeSetting : TestStep
    {
        #region Settings
        
        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("Output", "", "Input Parameters", 2)]
        public bool Output { get; set; } = true;

        [DisplayAttribute("StartPower", "", "Input Parameters", 2)]
        public double StartPower { get; set; } = -10D;

        [DisplayAttribute("Step", "", "Input Parameters", 2)]
        public double Step { get; set; } = 0.1D;

        [DisplayAttribute("SweepPowerSpan", "", "Input Parameters", 2)]
        public double SweepPowerSpan { get; set; } = 10D;

        [DisplayAttribute("PowerSweepState", "", "Input Parameters", 2)]
        public bool PowerSweepState { get; set; } = true;

        [DisplayAttribute("AmplitudeUnit", "DBM, DBMV, DBMA, V, W, A, DBUV, DBUA, DBPW, DBUVM, DBUAM, DBPT, DBG", "Input Parameters", 2)]
        public EAmplitudeUnit AmplitudeUnit { get; set; } = EAmplitudeUnit.DBM;

        #endregion

        public AmplitudeSetting()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.AmplitudeSetting(AmplitudeUnit, Output, StartPower, Step, SweepPowerSpan, PowerSweepState);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
