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
using System.Windows.Forms.VisualStyles;
using System.CodeDom;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace PXA_Demo
{
    #region enum

    public enum EScreenTheme { TDColor, TDMonochrome, FCOLor, FMONochrome }
    public enum ETraceName { TRACE1, TRACE2, TRACE3, TRACE4, TRACE5, TRACE6, ALL }
    public enum EImageFormat { jpeg, png}
    public enum EFileFormat { csv}
    public enum ETraceFormat { trace, csv}

    #endregion

    [Display("MemoryHandling", Group: "PXA_Demo", Description: "Insert a description here")]
    public class MemoryHandling : TestStep
    {
        #region Settings
        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here", Order: 1)]
        public N9030B MyInst { get; set; }
        [DisplayAttribute("ScreenTheme", "TDColor, TDMonochrome, FCOLor, FMONochrome", "Input Parameters", 2)]
        public EScreenTheme ScreenTheme { get; set; } = EScreenTheme.FCOLor;

        private string _FolderLocation = @"C:\Temp\";
        [DisplayAttribute("Folder path", "", "Enter Directory", 1.1)]
        [DirectoryPath]
        public string FolderLocation { get => _FolderLocation; set => _FolderLocation = value; }

        [DisplayAttribute("Save Data File", "", "Data File", 3.1)]
        public bool enableFileSaving { get; set; } = false;

        private string _Filename = "DataFile";
        [DisplayAttribute("Filename", "", "Data File", 3.2)]
        [EnabledIf("enableFileSaving", true, HideIfDisabled = true)]
        public string Filename { get => _Filename; set => _Filename = value; }

        private EFileFormat _FileFormat = EFileFormat.csv;
        [Display("File Format", "", "Data File", 3.3)]
        [EnabledIf("enableFileSaving", true, HideIfDisabled = true)]
        public EFileFormat FileFormat { get => _FileFormat; set => _FileFormat = value; }

        [DisplayAttribute("Save Screen", "", "Screen Capture", 4.1)]
        public bool enableScreenSaving { get; set; } = false;

        private string _ScreenSaveName = "ScreenShot";
        [DisplayAttribute("ScreenSaveName", "", "Screen Capture", 4.2)]
        [EnabledIf("enableScreenSaving", true, HideIfDisabled = true)]
        public string ScreenSaveName { get => _ScreenSaveName; set => _ScreenSaveName = value; }

        private EImageFormat _ImageFormat = EImageFormat.jpeg;
        [Display("Image Format", "", "Screen Capture", 4.3)]
        [EnabledIf("enableScreenSaving", true, HideIfDisabled = true)]
        public EImageFormat ImageFormat { get => _ImageFormat; set => _ImageFormat = value; }

        [DisplayAttribute("TraceName", "TRACE1, TRACE2, TRACE3, TRACE4, TRACE5, TRACE6, ALL", "Input Parameters", 5.1)]
        public bool enableTraceName { get; set; } = false;

        [DisplayAttribute("TraceName", "TRACE1, TRACE2, TRACE3, TRACE4, TRACE5, TRACE6, ALL", "Input Parameters", 5.2)]
        [EnabledIf("enableTraceName", true , HideIfDisabled = true)]
        public ETraceName TraceName { get; set; } = ETraceName.TRACE1;

        [DisplayAttribute("Trace Formate", "trace, csv", "Input Parameters", 5.3)]
        [EnabledIf("enableTraceName", true, HideIfDisabled = true)]
        public ETraceFormat TraceFormat { get; set; } = ETraceFormat.trace;

        [DisplayAttribute("Marker Table", "", "Marker Table", 6)]
        public bool enableMarkerTable { get; set; } = false;

        [DisplayAttribute("Peak Table", "", "Peak Table", 7)]
        public bool enablePeakTable { get; set; } = false;

        #endregion

        public MemoryHandling()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.
            string PathOfDataFile = FolderLocation + Filename + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + "." + FileFormat;
            string PathOfScreenshot = FolderLocation + ScreenSaveName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + "." + ImageFormat;
            MessageBox.Show(PathOfDataFile + " & " + PathOfScreenshot);

            MyInst.IoTimeout = 5000;
            Thread.Sleep(2000);
            MyInst.MemoryHandling(FolderLocation, enableFileSaving, Filename, FileFormat, enableScreenSaving,
                ScreenSaveName, ImageFormat, ScreenTheme, enableTraceName, TraceName, TraceFormat, enableMarkerTable, enablePeakTable);
            UpgradeVerdict(Verdict.Pass);

            
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
