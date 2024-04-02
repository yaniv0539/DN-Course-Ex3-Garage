using System.Collections.Generic;
using System;

namespace GarageLogic
{
    internal class Truck : Vehicle
    {
        private const string k_IsCarryingHazardousSubstancesKey = "Is carrying hazardous substances";
        private const string k_CargoVolumeKey = "Cargo volume";
        private const int k_MinimumCargoVolume = 0;

        public const eFuelType k_FuelType = eFuelType.Soler;
        public const float k_MaximumFuelAmount = 110f;
        public const int k_NumberOfWheels = 12;
        public const float k_MaximumAirPressure = 28;

        private bool m_IsCarryingHazardousSubstances;
        private float m_CargoVolume;

        private Truck(string i_LicensePlate, Engine i_Engine)
            : base(i_LicensePlate, i_Engine, k_MaximumAirPressure, k_NumberOfWheels)
        {
            m_Specification.Add(k_IsCarryingHazardousSubstancesKey, string.Empty);
            m_Specification.Add(k_CargoVolumeKey, string.Empty);
            m_IsCarryingHazardousSubstances = false;
            m_CargoVolume = k_MinimumCargoVolume;
        }

        public static Truck CreateTruck(string i_LicensePlate, Engine i_Engine)
        {
            return new Truck(i_LicensePlate, i_Engine);
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
                    bool parsable = bool.TryParse(value[k_IsCarryingHazardousSubstancesKey], out bool isCarryingHazardousSubstancesToChange);

                    if (!parsable)
                    {
                        throw new FormatException("If truck carrying hazardous substances must be 'True' or 'False'");
                    }

                    m_IsCarryingHazardousSubstances = isCarryingHazardousSubstancesToChange;

                    parsable = int.TryParse(value[k_CargoVolumeKey], out int cargoVolumeToChange);

                    if (!parsable)
                    {
                        throw new FormatException("Truck cargo volume must be numberical value");
                    }

                    isValidCargoVolume(cargoVolumeToChange);
                    m_CargoVolume = cargoVolumeToChange;
                }
                catch (Exception ex)
                {
                    base.ResetSpecifications();
                    m_IsCarryingHazardousSubstances = false;
                    m_CargoVolume = k_MinimumCargoVolume;
                    throw ex;
                }
            }
        }

        private static void isValidCargoVolume(float i_CargoVolume)
        {
            if (i_CargoVolume < k_MinimumCargoVolume)
            {
                throw new ArgumentException("Truck cargo must be non-negative number!");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
The truck is carrying hazardous substances: {1}
Cargo volume: {2}",
            base.ToString(), m_IsCarryingHazardousSubstances.ToString(), m_CargoVolume.ToString());
        }
    }
}
