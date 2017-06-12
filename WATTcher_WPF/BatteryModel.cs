using System;
using System.Management;

namespace WATTcher_WPF
{
    internal class BatteryModel
    {
        public UInt16 Availability { get; set; }
        public UInt32 BatteryRechargeTime { get; set; }
        public UInt16 BatteryStatus { get; set; }
        public string Caption { get; set; }
        public UInt16 Chemistry { get; set; }
        public UInt32 ConfigManagerErrorCode { get; set; }
        public bool ConfigManagerUserConfig { get; set; }
        public string CreationClassName { get; set; }
        public string Description { get; set; }
        public UInt32 DesignCapacity { get; set; }
        public UInt64 DesignVoltage { get; set; }
        public string DeviceID { get; set; }
        public bool ErrorCleared { get; set; }
        public string ErrorDescription { get; set; }
        public UInt16 EstimatedChargeRemaining { get; set; }
        public UInt32 EstimatedRunTime { get; set; }
        public UInt32 ExpectedBatteryLife { get; set; }
        public UInt32 ExpectedLife { get; set; }
        public UInt32 FullChargeCapacity { get; set; }
        public DateTime InstallDate { get; set; }
        public UInt32 LastErrorCode { get; set; }
        public UInt32 MaxRechargeTime { get; set; }
        public string Name { get; set; }
        public string PNPDeviceID { get; set; }
        public UInt16 PowerManagementCapabilities { get; set; }
        public bool PowerManagementSupported { get; set; }
        public string SmartBatteryVersion { get; set; }
        public string Status { get; set; }
        public UInt16 StatusInfo { get; set; }
        public string SystemCreationClassName { get; set; }
        public string SystemName { get; set; }
        public UInt32 TimeOnBattery { get; set; }
        public UInt32 TimeToFullCharge { get; set; }

        public BatteryModel()
        {
            UpdateValues();
        }

        public void UpdateValues()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from win32_Battery");


            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject mObject in results)
            {
                Availability = mObject.TryGetUInt16("Availability") ?? 0;
                BatteryRechargeTime = mObject.TryGetUInt32("BatteryRechargeTime") ?? 0;
                BatteryStatus = mObject.TryGetUInt16("BatteryStatus") ?? 0;
                Caption = mObject.TryGetString("Caption") ?? "";
                Chemistry = mObject.TryGetUInt16("Chemistry") ?? 0;
                ConfigManagerErrorCode = mObject.TryGetUInt32("ConfigManagerErrorCode") ?? 0;
                ConfigManagerUserConfig = mObject.TryGetBool("ConfigManagerUserConfig") ?? false;
                CreationClassName = mObject.TryGetString("CreationClassName") ?? "";
                Description = mObject.TryGetString("Description") ?? "";
                DesignCapacity = mObject.TryGetUInt32("DesignCapacity") ?? 0;
                DesignVoltage = mObject.TryGetUInt64("DesignVoltage") ?? 0;
                DeviceID = mObject.TryGetString("DeviceID") ?? "";
                ErrorCleared = mObject.TryGetBool("ErrorCleared") ?? false;
                ErrorDescription = mObject.TryGetString("ErrorDescription") ?? "";
                EstimatedChargeRemaining = mObject.TryGetUInt16("EstimatedChargeRemaining") ?? 0;
                EstimatedRunTime = mObject.TryGetUInt32("EstimatedRunTime") ?? 0;
                ExpectedBatteryLife = mObject.TryGetUInt32("ExpectedBatteryLife") ?? 0;
                ExpectedLife = mObject.TryGetUInt32("ExpectedLife") ?? 0;
                FullChargeCapacity = mObject.TryGetUInt32("FullChargeCapacity") ?? 0;
                InstallDate = mObject.TryGetDateTime("InstallDate") ?? new DateTime(1, 1, 1);
                LastErrorCode = mObject.TryGetUInt32("LastErrorCode") ?? 0;
                MaxRechargeTime = mObject.TryGetUInt32("MaxRechargeTime") ?? 0;
                Name = mObject.TryGetString("Name") ?? "";
                PNPDeviceID = mObject.TryGetString("PNPDeviceID") ?? "";
                PowerManagementCapabilities = mObject.TryGetUInt16("PowerManagementCapabilities") ?? 0;
                PowerManagementSupported = mObject.TryGetBool("PowerManagementSupported") ?? false;
                SmartBatteryVersion = mObject.TryGetString("SmartBatteryVersion") ?? "";
                Status = mObject.TryGetString("Status") ?? "";
                StatusInfo = mObject.TryGetUInt16("StatusInfo") ?? 0;
                SystemCreationClassName = mObject.TryGetString("SystemCreationClassName") ?? "";
                SystemName = mObject.TryGetString("SystemName") ?? "";
                TimeOnBattery = mObject.TryGetUInt32("TimeOnBattery") ?? 0;
                TimeToFullCharge = mObject.TryGetUInt32("TimeToFullCharge") ?? 0;
            }
        }
    }
}
