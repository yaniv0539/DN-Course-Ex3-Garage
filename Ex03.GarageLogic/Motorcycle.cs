using System.Collections.Generic;
using System;
using static GarageLogic.Car;

namespace GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            A2,
            AB,
            B2
        }

        private const string k_LicenseTypeKey = "License type";
        private const string k_EngineVolumeKey = "Engine volume";
        private const int k_MinimumEngineVolume = 0;

        public const eFuelType k_FuelType = eFuelType.Octan98;
        public const float k_MaximumFuelAmount = 5.8f;
        public const int k_NumberOfWheels = 2;
        public const float k_MaximumWheelsAirPressure = 29;
        public const float k_MaximumBattaryTime = 2.8f;

        private eLicenseType? m_licenseType;
        private int m_EngineVolume;

        private Motorcycle(string i_LicensePlate, Engine i_Engine)
            : base(i_LicensePlate, i_Engine, k_MaximumWheelsAirPressure, k_NumberOfWheels)
        {
            base.Specification.Add(k_LicenseTypeKey, string.Empty);
            base.Specification.Add(k_EngineVolumeKey, string.Empty);
            m_licenseType = null;
            m_EngineVolume = k_MinimumEngineVolume;
        }

        public static Motorcycle CreateMotorcycle(string i_LicensePlate, Engine i_Engine)
        {
            return new Motorcycle(i_LicensePlate, i_Engine);
        }

        public override Dictionary<string, string> Specification
        {
            get
            {
                return new Dictionary<string, string>(m_Specification);
            }
            set
            {
                try
                {
                    base.Specification = value;

                    bool parsable = Enum.TryParse(value[k_LicenseTypeKey], true, out eLicenseType licenseTypeToChange);

                    if (!parsable)
                    {
                        throw new FormatException("License must be of type: A1, A2, AB or B2");
                    }

                    m_licenseType = licenseTypeToChange;

                    parsable = int.TryParse(value[k_EngineVolumeKey], out int engineVolumeToChange);

                    if (!parsable)
                    {
                        throw new FormatException("Engine volume must be numerical value");
                    }

                    EngineVolumeValidation(engineVolumeToChange);
                    m_EngineVolume = engineVolumeToChange;
                }
                catch (Exception ex)
                {
                    base.ResetSpecifications();
                    m_licenseType = null;
                    m_EngineVolume = k_MinimumEngineVolume;
                    throw ex;
                }
            }
        }

        private static void EngineVolumeValidation(int i_EngineVolume)
        {
            if (i_EngineVolume < 0)
            {
                throw new ArgumentException("Value must be non-negative", nameof(i_EngineVolume));
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
License type: {1}
Engine volume in cc: {2}",
            base.ToString(), m_licenseType.ToString(), m_EngineVolume.ToString());
        }
    }
}
