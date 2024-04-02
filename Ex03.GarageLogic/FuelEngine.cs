using System.Collections.Generic;
using System;

namespace GarageLogic
{
    public enum eFuelType
    {
        Soler = 1,
        Octan95,
        Octan96,
        Octan98
    }

    internal class FuelEngine : Engine
    {
        private readonly eFuelType m_FuelType;

        private FuelEngine(float i_TankCapacity, eFuelType i_FuelType) : base(i_TankCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public float LitersInTank
        {
            get
            {
                return this.CurrentEnergySourceSupply;
            }
        }

        public float TankCapacity
        {
            get
            {
                return this.EnergySourceCapacity;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return this.m_FuelType;
            }
        }

        public static FuelEngine CreateFuelEngine(float i_DesiredFuelTankCapacity, eFuelType i_FuelType)
        {
            return new FuelEngine(i_DesiredFuelTankCapacity, i_FuelType);
        }

        public void RefuelTank(float i_AmoutOutLitersToPump, eFuelType i_FuelTypeToPump)
        {

            if (i_FuelTypeToPump == this.m_FuelType)
            {
                this.AddEnergySourceSupply(i_AmoutOutLitersToPump);
            }
            else
            {
                string exMessage = string.Format("Invalid fuel type specified! Correct one is {0}", this.m_FuelType.ToString());

                throw new ArgumentException(exMessage);
            }
        }

        public void EmptyFuelTank()
        {
            this.EmptyEnergySourceSupply();
        }

        public static List<string> GetFuelTypes()
        {
            List<string> fuelTypes = new List<string>()
            {
                "1. Soler",
                "2. Octan 95",
                "3. Octan 96",
                "4. Octan 98"
            };

            return fuelTypes;
        }

        public override string GetEnergySourceRequiredName()
        {
            return string.Format("Fuel amount in liters");
        }

        public override string ToString()
        {
            return string.Format(
@"Current liters in fuel tank: {0}
Fuel tank capacity: {1}
Fuel type: {2}",
            this.LitersInTank, this.TankCapacity, m_FuelType.ToString());
        }
    }
}
