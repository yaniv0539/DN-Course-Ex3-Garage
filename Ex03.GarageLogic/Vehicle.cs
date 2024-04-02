using System.Collections.Generic;
using System;
using System.Text;

namespace GarageLogic
{
    internal abstract class Vehicle
    {
        private const string k_ModelNameKey = "Model Name";
        private const float k_MinimumEnergyStatusPrecentage = 0;

        private readonly string m_LicensePlate;
        private string m_ModelName;
        private float m_EnergyStatusPrecentage;
        private List<Wheel> m_Wheels;
        private Engine m_Engine;
        protected Dictionary<string, string> m_Specification = [];

        protected Vehicle(string i_LicensePlate, Engine i_EngineType, float i_MaximumWheelsAirPressure, int i_NumberOfWheels)
        {
            m_LicensePlate = i_LicensePlate;
            m_Engine = i_EngineType;
            m_ModelName = string.Empty;
            m_EnergyStatusPrecentage = k_MinimumEnergyStatusPrecentage;

            m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaximumWheelsAirPressure));
            }

            m_Specification = new Dictionary<string, string>
            {
                { k_ModelNameKey, string.Empty},
                { Engine.GetEnergySourceRequiredName(), string.Empty},
                { Wheel.k_ManufacturerNameKey, string.Empty},
                { Wheel.k_CurrentAirPressureKey, string.Empty}
            };
        }

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public virtual Dictionary<string, string> Specification
        {
            get
            {
                return m_Specification;
            }
            set
            {
                try
                {
                    m_ModelName = value[k_ModelNameKey];
                    addEnergySourceSupply(value[Engine.GetEnergySourceRequiredName()]);

                    bool parsable = float.TryParse(value[Wheel.k_CurrentAirPressureKey], out float airPressureToAdd);

                    if (!parsable)
                    {
                        throw new FormatException("Wheels air pressure must be a numerical input value");
                    }

                    foreach (Wheel wheel in m_Wheels)
                    {
                        wheel.ManufacturerName = value[Wheel.k_ManufacturerNameKey];
                        wheel.Inflate(airPressureToAdd);
                    }
                }
                catch (Exception ex)
                {
                    ResetSpecifications();
                    throw ex;
                }
            }
        }

        private void addEnergySourceSupply(string i_EnergySourceSupplyToAdd)
        {
            bool parsable = false;
            FuelEngine fuelEngine = m_Engine as FuelEngine;

            parsable = float.TryParse(i_EnergySourceSupplyToAdd, out float energySourceSupplyToAdd);

            if (!parsable)
            {
                throw new FormatException(string.Format(
                    "{0} must be a numerical input value", m_Engine.GetEnergySourceRequiredName()));
            }

            if (fuelEngine != null)
            {
                RefuelTank(fuelEngine.FuelType, energySourceSupplyToAdd);
            }
            else
            {
                RechargeBattery(energySourceSupplyToAdd);
            }
        }

        public void RefuelTank(eFuelType i_FuelType, float i_FuelAmount)
        {
            FuelEngine fuelEngine = m_Engine as FuelEngine;

            if (fuelEngine != null)
            {
                fuelEngine.RefuelTank(i_FuelAmount, i_FuelType);
                m_EnergyStatusPrecentage = (fuelEngine.LitersInTank / fuelEngine.TankCapacity) * 100;
            }
            else
            {
                throw new InvalidEngineTypeException("Cannot refuel an electric car!");
            }
        }

        public void RechargeBattery(float i_AmountHoursToCharge)
        {
            ElectricEngine electricEngine = m_Engine as ElectricEngine;

            if (electricEngine != null)
            {
                electricEngine.RechargeBattery(i_AmountHoursToCharge);
                m_EnergyStatusPrecentage = (electricEngine.BatteryRemainingHours / electricEngine.BatteryMaximumHours) * 100;
            }
            else
            {
                throw new InvalidEngineTypeException("Cannot refuel an electric car!");
            }
        }

        public void InflateWheelsToMaximum()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Inflate(wheel.MaximumAirPressure - wheel.CurrentAirPressure);
            }
        }

        protected void ResetSpecifications()
        {
            m_ModelName = string.Empty;
            resetEngineEnergySupply();

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.resetWheel();
            }
        }

        private void resetEngineEnergySupply()
        {
            FuelEngine fuelEngine = m_Engine as FuelEngine;
            ElectricEngine electricEngine = m_Engine as ElectricEngine;

            if (fuelEngine != null)
            {
                fuelEngine.EmptyFuelTank();
            }
            else
            {
                electricEngine.EmptyBattery();
            }

            m_EnergyStatusPrecentage = k_MinimumEnergyStatusPrecentage;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;

            if (obj is Vehicle toCompareTo)
            {
                equals = this.LicensePlate == toCompareTo.LicensePlate;
            }

            return equals;
        }

        public static bool operator ==(Vehicle vehicle1, Vehicle vehicle2)
        {
            return vehicle1.Equals(vehicle2);
        }

        public static bool operator !=(Vehicle vehicle1, Vehicle vehicle2)
        {
            return !vehicle1.Equals(vehicle2);
        }

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
@"License plate: {0}
Model name: {1}
Energy status: {2}%

Engine:
{3}

Wheels:
{4}",
            m_LicensePlate, m_ModelName, Math.Round(m_EnergyStatusPrecentage, 2), m_Engine.ToString(), wheelsToString());
        }

        private string wheelsToString()
        {
            StringBuilder wheelsToStringBuilder = new StringBuilder();
            int wheelNumber = 1;

            foreach (Wheel wheel in m_Wheels)
            {
                wheelsToStringBuilder.Append(string.Format(
@"Wheel {0}:
{1}
",
                wheelNumber, wheel.ToString()));
                wheelNumber++;
            }

            return wheelsToStringBuilder.ToString();
        }
    }
}
