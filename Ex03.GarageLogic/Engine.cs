using System.Collections.Generic;
using System;

namespace GarageLogic
{
    internal abstract class Engine
    {
        private const int k_MinimumEnergySource = 0;

        private float m_CurrentEnergySourceSupply = k_MinimumEnergySource;
        private readonly float m_EnergySourceCapacity;

        protected Engine(float i_EnergySourceCapacity)
        {
            m_EnergySourceCapacity = i_EnergySourceCapacity;
        }

        protected float CurrentEnergySourceSupply
        {
            get
            {
                return m_CurrentEnergySourceSupply;
            }
        }

        protected float EnergySourceCapacity
        {
            get
            {
                return m_EnergySourceCapacity;
            }
        }

        protected void AddEnergySourceSupply(float i_EnergySourceSupplyToAdd)
        {
            float newEnergySourceSupply = m_CurrentEnergySourceSupply + i_EnergySourceSupplyToAdd;

            if (isEnergySourceSupplyInRange(newEnergySourceSupply))
            {
                m_CurrentEnergySourceSupply = newEnergySourceSupply;
            }
            else
            {
                throw new ValueOutOfRangeException(
                    new Exception(),
                    this.GetEnergySourceRequiredName(),
                    k_MinimumEnergySource,
                    m_EnergySourceCapacity - m_CurrentEnergySourceSupply);
            }
        }

        protected void EmptyEnergySourceSupply()
        {
            m_CurrentEnergySourceSupply = k_MinimumEnergySource;
        }

        private bool isEnergySourceSupplyInRange(float i_EnergySourceSupplyToCheck)
        {
            return ((0 >= i_EnergySourceSupplyToCheck) || (i_EnergySourceSupplyToCheck <= m_EnergySourceCapacity));
        }

        public abstract string GetEnergySourceRequiredName();
    }
}
