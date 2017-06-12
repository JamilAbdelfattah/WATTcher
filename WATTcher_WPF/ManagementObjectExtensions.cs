using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace WATTcher_WPF
{
    public static class ManagementObjectExtensions
    {
        public static UInt16? TryGetUInt16(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (UInt16)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static UInt32? TryGetUInt32(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (UInt32)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static UInt64? TryGetUInt64(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (UInt64)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string TryGetString(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (string)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool? TryGetBool(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (bool)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? TryGetDateTime(this ManagementObject mObject, string propertyName)
        {
            try
            {
                return (DateTime)mObject.Properties[propertyName].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
