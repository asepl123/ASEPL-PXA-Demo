using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace PXA_Demo.Properties
{
    [Display("Measurement2", Group: "PXA_Demo.Properties", Description: "Insert a description here")]
    public class Measurement2 : TestStep
    {
        #region Settings

        [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
        public N9030B MyInst { get; set; }

        [DisplayAttribute("HARMonics<n>", "1=THD in %, 2=THD in dBc", "Input Parameters", 2)]
        [EnabledIf("enableHarmonics", true, HideIfDisabled = true)]
        public uint Harmonics { get; set; } = 1u;

        [DisplayAttribute("HARMonics<n>", "1=THD in %, 2=THD in dBc", "Input Parameters", 2)]
        [EnabledIf("enableOutputDistortion", true, HideIfDisabled = true)]
        public uint HarmonicsDistortion { get; set; } = 1u;

        [DisplayAttribute("AMPLitude<n>", "", "Input Parameters", 2)]
        [EnabledIf("enableOutputAmplitude", true, HideIfDisabled = true)]
        public uint Amplitude { get; set; } = 1u;

        [DisplayAttribute("FREQuency<n>", "", "Input Parameters", 2)]
        [EnabledIf("enableOutputFrequency", true, HideIfDisabled = true)]
        public uint Frequency { get; set; } = 1u;

        [DisplayAttribute("SEMask<n>", "", "Input Parameters", 2)]
        [EnabledIf("enableSEMask", true, HideIfDisabled = true)]
        public uint SEMask { get; set; } = 1u;

        [DisplayAttribute("SPURious<n>", "", "Input Parameters", 2)]
        [EnabledIf("enableSpurious", true, HideIfDisabled = true)]
        public uint Spurious { get; set; } = 1u;

        [DisplayAttribute("BPOWer<n>", "", "Input Parameters", 2)]
        [EnabledIf("enableBPower", true, HideIfDisabled = true)]
        public uint BPower { get; set; } = 1u;

        [DisplayAttribute("TOI", "", "TOI", 3)]
        public bool enableTOI { get; set; } = false;

        [DisplayAttribute("IP3", "", "IP3", 3)]
        public bool enableIP3 { get; set; } = false;

        [DisplayAttribute("SEM", "", "SEM", 3)]
        public bool enableSEMask { get; set; } = false;

        [DisplayAttribute("Spurious", "", "Spurious", 3)]
        public bool enableSpurious { get; set; } = false;

        [DisplayAttribute("Burst Power", "", "Burst Power", 3)]
        public bool enableBPower { get; set; } = false;

        [DisplayAttribute("Harmonics", "", "Harmonics", 3)]
        public bool enableHarmonics { get; set; } = false;

        [DisplayAttribute("Output Harmonics", "", "Harmonics", 3)]
        [EnabledIf("enableHarmonics", true, HideIfDisabled = true)]
        public bool enableOutputHarmonics { get; set; } = false;

        [DisplayAttribute("Output Amplitude", "", "Harmonics", 3)]
        [EnabledIf("enableHarmonics", true, HideIfDisabled = true)]
        public bool enableOutputAmplitude { get; set; } = false;

        [DisplayAttribute("Output Distortion", "", "Harmonics", 3)]
        [EnabledIf("enableHarmonics", true, HideIfDisabled = true)]
        public bool enableOutputDistortion { get; set; } = false;

        [DisplayAttribute("Output Frequency", "", "Harmonics", 3)]
        [EnabledIf("enableHarmonics", true, HideIfDisabled = true)]
        public bool enableOutputFrequency { get; set; } = false;

        //Outputs

        [DisplayAttribute("amplitude", "", "Output Parameters", 3)]
        public Double[] OutputAmplitude;

        private Double OutputDistortion;

        private Double OutputFrequency;

        private Double[] OutputAllFreq;

        private Double[] OutputAllAmp;

        private Double OutputFundFreq;

        // returns 13 outputs
        private Double[] OutputToi;

        // Returns the worst-case Output Intercept Power value in dBm
        private Double[] OutputIp3;

        private Single[] OutputSEMask;

        private Single[] OutputSpurious;

        private Single[] OutputBPower;

        private Double[] OutputHarmonics;

        #endregion

        public Measurement2()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // Harmonics
            if (enableHarmonics)
            {
                if (enableOutputHarmonics)
                {
                    MyInst.ScpiCommand(":FORMat:TRACe:DATA REAL,64");
                    MyInst.ScpiCommand(":FORMat:BORDer SWAP");
                    OutputHarmonics = MyInst.ScpiQueryBlock<System.Double>(Scpi.Format(":MEASure:HARMonics{0}?", Harmonics));
                }

                if (enableOutputAmplitude)
                {
                    OutputAmplitude = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":MEASure:HARMonics{0}:AMPLitude{1}?", Harmonics, Amplitude), true);
                    OutputAllAmp = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":MEASure:HARMonics{0}:AMPLitude{1}:ALL?", Harmonics, Harmonics), true);
                }

                if(enableOutputDistortion) OutputDistortion = MyInst.ScpiQuery<System.Double>(Scpi.Format(":MEASure:HARMonics{0}:DISTortion?", HarmonicsDistortion), true);

                if (enableOutputFrequency)
                {
                    OutputFrequency = MyInst.ScpiQuery<System.Double>(Scpi.Format(":MEASure:HARMonics{0}:FREQuency{1}?", Harmonics, Frequency), true);
                    OutputAllFreq = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":MEASure:HARMonics{0}:FREQuency{1}:ALL?", Harmonics, Frequency), true);
                    OutputFundFreq = MyInst.ScpiQuery<System.Double>(Scpi.Format(":MEASure:HARMonics{0}:FUNDamental?", Harmonics), true);
                }
                
            }

            // TOI
            if (enableTOI)
            {
                MyInst.ScpiCommand(":FORMat:TRACe:DATA REAL,64");
                MyInst.ScpiCommand(":FORMat:BORDer SWAP");
                OutputToi = MyInst.ScpiQueryBlock<System.Double>(Scpi.Format(":MEASure:TOI2?"));
            }

            if (enableIP3)
            {
                OutputIp3 = MyInst.ScpiQueryBlock<System.Double>(Scpi.Format(":MEASure:TOI1:IP3?"));
            }

            // SEM
            if (enableSEMask)
            {
                MyInst.ScpiCommand(":FORMat:TRACe:DATA REAL,32");
                MyInst.ScpiCommand(":FORMat:BORDer SWAP");
                OutputSEMask = MyInst.ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:SEMask{0}?", SEMask));
            }

            // Spurious
            if (enableSpurious)
            {
                MyInst.ScpiCommand(":FORMat:TRACe:DATA REAL,32");
                MyInst.ScpiCommand(":FORMat:BORDer SWAP");
                OutputSpurious = MyInst.ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:SPURious{0}?", Spurious));
            }

            // BurstPower
            if (enableBPower)
            {
                MyInst.ScpiCommand(":FORMat:TRACe:DATA REAL,32");
                MyInst.ScpiCommand(":FORMat:BORDer SWAP");
                OutputBPower = MyInst.ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:BPOWer{0}?", BPower));
            }
            
        }
    }
}
