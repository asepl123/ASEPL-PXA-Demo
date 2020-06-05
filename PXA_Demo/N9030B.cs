using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;
using System.Windows.Forms;

//Note this template assumes that you have a SCPI based instrument, and accordingly
//extends the ScpiInstrument base class.

//If you do NOT have a SCPI based instrument, you should modify this instance to extend
//the (less powerful) Instrument base class.

namespace PXA_Demo
{
    [Display("N9030B", Group: "PXA_Demo", Description: "Insert a description here")]
    public class N9030B : ScpiInstrument
    {
        #region Settings
        // ToDo: Add property here for each parameter the end user should be able to change
        #endregion

        public N9030B()
        {
            Name = "PXA N9030B";
            VisaAddress = "Simulate";
            // ToDo: Set default values for properties / settings.
        }

        /// <summary>
        /// Open procedure for the instrument.
        /// </summary>
        public override void Open()
        {

            base.Open();
            MessageBox.Show($"Connection to {VisaAddress} successful");
            Log.Info(string.Format("Connection to {0} successful", VisaAddress));
            // TODO:  Open the connection to the instrument here

            if (!IdnString.Contains("Instrument ID"))
            {
                Log.Error("This instrument driver does not support the connected instrument.");
                throw new ArgumentException("Wrong instrument type.");
            }

        }

        /// <summary>
        /// Close procedure for the instrument.
        /// </summary>
        public override void Close()
        {
            // TODO:  Shut down the connection to the instrument here.
            base.Close();
        }

        /// <summary>
        /// Reset Instrumet
        /// </summary>
        public void ResetN9030B()
        {
            ScpiCommand("*RST");
            ScpiCommand(":SYSTem:PRESet");
            ScpiCommand("*OPC");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CenterFreq"></param>
        /// <param name="StartFreq"></param>
        /// <param name="StopFreq"></param>
        /// <param name="FreqSpan"></param>

        public void FrequencySetting(double CenterFreq, double StartFreq, double StopFreq, double FreqSpan)
        {
            ScpiCommand(":SENSe:FREQuency:CENTer {0}", CenterFreq);
            ScpiCommand(":SENSe:FREQuency:STARt {0}", StartFreq);
            ScpiCommand(":SENSe:FREQuency:STOP {0}", StopFreq);
            ScpiCommand(":SENSe:FREQuency:SPAN {0}", FreqSpan);
            ScpiCommand("*OPC");
        }


        public void AmplitudeSetting(EAmplitudeUnit AmplitudeUnit, bool Output, double StartPower, 
            double Step, double SweepPowerSpan, bool PowerSweepState)
        {
            ScpiCommand(":UNIT:POWer {0}", AmplitudeUnit);
            ScpiCommand(":OUTPut:EXTernal:STATe {0}", Output);
            ScpiCommand(":SOURce:POWer:STARt {0}", StartPower);
            ScpiCommand(":SOURce:POWer:STEP:INCRement {0}", Step);
            ScpiCommand(":SOURce:POWer:SWEep {0}", SweepPowerSpan);
            ScpiCommand(":SOURce:POWer:SWEep:STATe {0}", PowerSweepState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SweepPoints"></param>
        /// <param name="SweepTime"></param>
        public void SweepConfigure(double SweepPoints, double SweepTime)
        {
            ScpiCommand(":SENSe:SWEep:POINts {0}", SweepPoints);
            ScpiCommand(":SENSe:SWEep:TIME {0}", SweepTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MarkerNo"></param>
        /// <param name="MarkerMode"></param>
        /// <param name="MarkerTableStatus"></param>
        /// <param name="TraceNo"></param>
        /// <param name="MarkerFreq"></param>
        public void Marker(uint MarkerNo, EMarkerMode MarkerMode, bool MarkerTableStatus,
            int TraceNo, double MarkerFreq)
        {
            ScpiCommand(":CALCulate:MARKer{0}:MODE {1}", MarkerNo, MarkerMode);
            ScpiCommand(":CALCulate:MARKer{0}:SET:CENTer", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:SET:DELTa:CENTer", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:SET:DELTa:SPAN", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:SET:STARt", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:SET:STEP", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:SET:STOP", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:TABLe:STATe {1}", MarkerNo, MarkerTableStatus);
            ScpiCommand(":CALCulate:MARKer{0}:TRACe {1}", MarkerNo, TraceNo);
            ScpiCommand(":CALCulate:MARKer{0}:X {1}", MarkerNo, MarkerFreq);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MarkerNo"></param>
        /// <param name="MarkerNormal"></param>
        /// <param name="MarkerPeakMode"></param>
        public void MarkerFunction(uint MarkerNo, bool MarkerNormal, EMarkerPeakMode MarkerPeakMode)
        {
            ScpiCommand(":CALCulate:MARKer{0}:MAXimum", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:MINimum", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:PTPeak", MarkerNo);
            ScpiCommand(":CALCulate:MARKer{0}:STATe {1}", MarkerNo, MarkerNormal);
            ScpiCommand(":CALCulate:MARKer{0}:PEAK:SEARch:MODE {1}", MarkerNo, MarkerPeakMode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PreAmpState"></param>
        /// <param name="PreAmpBand"></param>
        public void PreAmplifier(bool PreAmpState, EPreAmpBand PreAmpBand)
        {
            ScpiCommand(":SENSe:POWer:RF:GAIN:STATe {0}", PreAmpState);
            ScpiCommand(":SENSe:POWer:RF:GAIN:BAND {0}", PreAmpBand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogOutput"></param>
        /// <param name="enableOutputACP"></param>
        /// <param name="enableOutputBPower"></param>
        /// <param name="enableOutputChPower"></param>
        /// <param name="ACPowerNo"></param>
        /// <param name="BPOWerNo"></param>
        /// <param name="CHPowerNo"></param>
        /// <param name="_OutputACP"></param>
        /// <param name="_OutputBPower"></param>
        /// <param name="_OutputChPower"></param>

        public void Measurements(OpenTap.TraceSource LogOutput, bool enableOutputACP, bool enableOutputBPower, bool enableOutputChPower, uint ACPowerNo, uint BPOWerNo, uint CHPowerNo, ref Single[] _OutputACP, ref Single[] _OutputBPower, ref Single[] _OutputChPower)
        {
            ScpiCommand(":FORMat:TRACe:DATA REAL,32");
            ScpiCommand(":FORMat:BORDer SWAP");
            if (enableOutputACP == true)
            {
                _OutputACP = ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:ACPower{0}?", ACPowerNo));
                LogOutput.Info("*********************** AC Power ******************");
                LogOutput.Info("************************* " + _OutputACP + " dB *************************");
            }
            if (enableOutputBPower == true)
            {
                _OutputBPower = ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:BPOWer{0}?", BPOWerNo));
                LogOutput.Info("*********************** Burst Power ******************");
                LogOutput.Info("************************* " + _OutputBPower + " dB *************************");
            }
            if (enableOutputChPower == true)
            {
                _OutputChPower = ScpiQueryBlock<System.Single>(Scpi.Format(":MEASure:CHPower{0}?", CHPowerNo));
                LogOutput.Info("*********************** Channel Power ******************");
                LogOutput.Info("************************* " + _OutputChPower + " dB *************************");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ScreenSaveName"></param>
        /// <param name="ScreenTheme"></param>
        /// <param name="TraceName"></param>
        /// <param name="Filename"></param>
        public void MemoryHandling(string FolderLocation, bool enableFileSaving, string Filename, 
            EFileFormat FileFormat, bool enableScreenSaving, string ScreenSaveName, 
            EImageFormat ImageFormat, EScreenTheme ScreenTheme, bool enableTraceName, 
            ETraceName TraceName, ETraceFormat TraceFormat, bool enableMarkerTable, bool enablePeakTable)
        {
            if (enableFileSaving)
            {
                string PathOfDataFile = FolderLocation + Filename + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + "." + FileFormat;
            }

            if (enableScreenSaving)
            {
                string PathOfScreenshot = FolderLocation + ScreenSaveName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + "." + ImageFormat;
                ScpiCommand(":MMEMory:STORe:SCReen {0}", ScreenSaveName);
                ScpiCommand("*OPC");
                ScpiCommand(":MMEMory:STORe:SCReen:THEMe {0}", ScreenTheme);
                ScpiCommand("*OPC");
            }

            string PathOfTrace = "";
            if (enableTraceName)
            {
                if(TraceFormat == ETraceFormat.csv)
                {
                    switch (TraceName)
                    {
                        case ETraceName.TRACE1:
                        case ETraceName.TRACE2:
                        case ETraceName.TRACE3:
                        case ETraceName.TRACE4:
                        case ETraceName.TRACE5:
                        case ETraceName.TRACE6:
                            PathOfTrace = FolderLocation + TraceName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + ".csv";
                            ScpiCommand(":MMEMory:STORe:TRACe:DATA {0},{1}", TraceName, Filename);
                            ScpiCommand("*OPC");
                            break;
                        case ETraceName.ALL:
                            for (var i = 1; i <= 6; i++)
                            {
                                PathOfTrace = FolderLocation + TraceName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + ".csv";
                                ScpiCommand(":MMEMory:STORe:TRACe:DATA {0},{1}", TraceName, Filename);
                                ScpiCommand("*OPC");
                            }
                            break;
                    }
                }
                else if(TraceFormat == ETraceFormat.trace)
                {
                    PathOfTrace = FolderLocation + TraceName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + ".trace";
                    ScpiCommand(":MMEMory:STORe:TRACe {0},{1}", TraceName, Filename);
                    ScpiCommand("*OPC");
                }
                
            }

            if (enableMarkerTable)
            {
                string FilePathName = FolderLocation + "Marker_Table" + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + ".csv";
                ScpiCommand(":MMEMory:STORe:RESults:MTABle {0}", FilePathName);
            }
            if(enablePeakTable)
            {
                string FilePathName = FolderLocation + "Peak_Table" + TraceName + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss") + ".csv";
                ScpiCommand(":MMEMory:STORe:RESults:PTABle {0}", FilePathName);
            }
            
            // ScpiCommand(":MMEMory:STORe:RESults:SPECtrogram {0}", Filename);
            
        }


        public void Display(uint WINDowNo, bool state, int SelectWindow, bool GridStatus, 
            double OffsetFreq, double Dline, bool DlineState, double NormRefLevel, int NormRefPositon, 
            double Pdivision, EScaleSpacing YScaleSpacing, EScaleSpacing XScaleSpacing)
        {
            ScpiCommand(":DISPlay:WINDow{0}:FORMat:TILE", WINDowNo);
            ScpiCommand(":DISPlay:WINDow{0}:FORMat:ZOOM", WINDowNo);
            ScpiCommand(":DISPlay:WINDow{0}:MAMarker:STATe {1}", WINDowNo, state);
            ScpiCommand(":DISPlay:WINDow{0}:SELect {1}", WINDowNo, SelectWindow);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:GRATicule:GRID:STATe {1}", WINDowNo, GridStatus);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:X:SCALe:OFFSet {1}", WINDowNo, OffsetFreq);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:X:SCALe:SPACing {1}", WINDowNo, XScaleSpacing);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:SPACing {1}", WINDowNo, YScaleSpacing);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:DLINe:STATe {1}", WINDowNo, DlineState);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:DLINe {1}", WINDowNo, Dline);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:NRLevel {1}", WINDowNo, NormRefLevel);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:NRPosition {1}", WINDowNo, NormRefPositon);
            ScpiCommand(":DISPlay:WINDow{0}:TRACe:Y:SCALe:PDIVision {1}", WINDowNo, Pdivision);
        }

    }
}
