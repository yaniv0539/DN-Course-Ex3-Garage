using System;

namespace GarageLogic
{
    internal class Wheel
    {
        public const string k_ManufacturerNameKey = "Wheels manufacturer name";
        public const string k_CurrentAirPressureKey = "Wheels air pressure";

        private const float k_MinimumAirPressure = 0;

        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaximumAirPressure;

        public Wheel(float i_MaximumAirPressure)
        {
            m_ManufacturerName = string.Empty;
            m_CurrentAirPressure = k_MinimumAirPressure;
            m_MaximumAirPressure = i_MaximumAirPressure;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return m_MaximumAirPressure;
            }
        }

        public void Inflate(float i_AirPressureToAdd)
        {
            float newAirPressure = i_AirPressureToAdd + CurrentAirPressure;

            if (isAirPressureInRange(newAirPressure))
            {
                m_CurrentAirPressure = newAirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    "wheels air pressure",
                    k_MinimumAirPressure,
                    m_MaximumAirPressure);
            }
        }

        public void resetWheel()
        {
            m_CurrentAirPressure = k_MinimumAirPressure;
            m_ManufacturerName = string.Empty;
        }

        private bool isAirPressureInRange(float i_AirPressureToCheck)
        {
            return ((0 >= i_AirPressureToCheck) || (i_AirPressureToCheck <= MaximumAirPressure));
        }

        public override string ToString()
        {
            return string.Format(
@"Manufacturer name: {0}
Current air pressure: {1}
Maximum air pressure: {2}",
            m_ManufacturerName, m_CurrentAirPressure, m_MaximumAirPressure);
        }
    }
}
