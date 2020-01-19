using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeCodeGenerator
    {
        public static void Generate(string outputPath)
        {
            string generatedDate = DateTime.UtcNow.ToShortDateString() + " - " + DateTime.UtcNow.ToShortTimeString();

            ZigBeeXmlParser zclParser = new ZigBeeXmlParser();
            zclParser.AddFile("./Resources/XXXX_General.xml");

            zclParser.AddFile("./Resources/0000_Basic.xml");
            zclParser.AddFile("./Resources/0001_PowerConfiguration.xml");
            zclParser.AddFile("./Resources/0003_Identify.xml");
            zclParser.AddFile("./Resources/0004_Groups.xml");
            zclParser.AddFile("./Resources/0005_Scenes.xml");
            zclParser.AddFile("./Resources/0006_OnOff.xml");
            zclParser.AddFile("./Resources/0007_OnOffSwitchConfiguration.xml");
            zclParser.AddFile("./Resources/0008_LevelControl.xml");
            zclParser.AddFile("./Resources/0009_Alarms.xml");
            zclParser.AddFile("./Resources/000A_Time.xml");
            zclParser.AddFile("./Resources/000B_RssiLocation.xml");
            zclParser.AddFile("./Resources/000C_AnalogInputBasic.xml");
            zclParser.AddFile("./Resources/000F_BinaryInputBasic.xml");
            zclParser.AddFile("./Resources/0012_MultistateInputBasic.xml");
            zclParser.AddFile("./Resources/0013_MultistateOutputBasic.xml");
            zclParser.AddFile("./Resources/0014_MultistateValueBasic.xml");
            zclParser.AddFile("./Resources/0015_Commissioning.xml");
            zclParser.AddFile("./Resources/0019_OtaUpgrade.xml");
            zclParser.AddFile("./Resources/0020_PollControl.xml");
            zclParser.AddFile("./Resources/0021_Greenpower.xml");

            zclParser.AddFile("./Resources/0101_DoorLock.xml");
            zclParser.AddFile("./Resources/0102_WindowCovering.xml");
         
            zclParser.AddFile("./Resources/0201_Thermostat.xml");
            zclParser.AddFile("./Resources/0202_FanControl.xml");
            zclParser.AddFile("./Resources/0203_DehumidificationControl.xml");
            zclParser.AddFile("./Resources/0204_ThermostatUserInterfaceConfiguration.xml");
                    
            zclParser.AddFile("./Resources/0300_ColorControl.xml");
                  
            zclParser.AddFile("./Resources/0400_IlluminanceMeasurement.xml");
            zclParser.AddFile("./Resources/0401_IlluminanceLevelSensing.xml");
            zclParser.AddFile("./Resources/0402_TemperatureMeasurement.xml");
            zclParser.AddFile("./Resources/0403_PressureMeasurement.xml");
            zclParser.AddFile("./Resources/0404_FlowMeasurement.xml");
            zclParser.AddFile("./Resources/0405_RelativeHumidityMeasurement.xml");
            zclParser.AddFile("./Resources/0406_OccupancySensing.xml");
 
            zclParser.AddFile("./Resources/0500_IasZone.xml");
            zclParser.AddFile("./Resources/0501_IasAce.xml");
            zclParser.AddFile("./Resources/0502_IasWd.xml");
            
            zclParser.AddFile("./Resources/0700_Price.xml");
            zclParser.AddFile("./Resources/0701_DemandResponseAndLoadControl.xml");
            zclParser.AddFile("./Resources/0702_Metering.xml");
            zclParser.AddFile("./Resources/0703_Messaging.xml");
            //TODO: there is conflict with protocol class generated for the following cluster and the protocol namespace
            //zclParser.AddFile("./Resources/0704_SmartEnergyTunneling.xml");
            zclParser.AddFile("./Resources/0705_Prepayment.xml");
            zclParser.AddFile("./Resources/0800_KeyEstablishment.xml");
                            
            zclParser.AddFile("./Resources/0B04_ElectricalMeasurement.xml");
            zclParser.AddFile("./Resources/0B05_Diagnostics.xml");

            List<ZigBeeXmlCluster> zclClusters = zclParser.ParseClusterConfiguration();

            ZigBeeXmlParser zdoParser = new ZigBeeXmlParser();
            zdoParser.AddFile("./Resources/XXXX_ZigBeeDeviceObject.xml");

            List<ZigBeeXmlCluster> zdoClusters = zdoParser.ParseClusterConfiguration();

            // Process all enums, bitmaps and structures first so we have a consolidated list.
            // We use this later when generating the imports in the cluster and command classes.
            List<ZigBeeXmlCluster> allClusters = new List<ZigBeeXmlCluster>();
            allClusters.AddRange(zclClusters);
            allClusters.AddRange(zdoClusters);
            ZigBeeZclDependencyGenerator typeGenerator = new ZigBeeZclDependencyGenerator(outputPath, allClusters, generatedDate);
            Dictionary<string, string> zclTypes = typeGenerator.GetDependencyMap();

            new ZigBeeZclClusterGenerator(outputPath, zclClusters, generatedDate, zclTypes);
            new ZigBeeZclCommandGenerator(outputPath, zclClusters, generatedDate, zclTypes);
            new ZigBeeZclConstantGenerator(outputPath, zclClusters, generatedDate, zclTypes);
            new ZigBeeZclStructureGenerator(outputPath, zclClusters, generatedDate, zclTypes);
            new ZigBeeZclClusterTypeGenerator(outputPath, zclClusters, generatedDate, zclTypes);

            new ZigBeeZclCommandGenerator(outputPath, zdoClusters, generatedDate, zclTypes);

            //zclParser = new ZigBeeXmlParser();
            //zclParser.AddFile("./Resources/zigbee_constants.xml");
            //ZigBeeXmlGlobal globals = zclParser.ParseGlobalConfiguration();

            //foreach (ZigBeeXmlConstant constant in globals.Constants)
            //{
            //    new ZigBeeZclConstantGenerator(constant);
            //}

            //new ZigBeeZclReadmeGenerator(zclClusters);
        }
    }
}