using System.Collections.Generic;
using System;

namespace GarageLogic
{
    public enum eRepairStatus
    {
        Fixed = 1,
        Repairing,
        Paid
    }

    internal class VehicleRepairRecord
    {
        private const eRepairStatus k_DefaultRepairStatus = eRepairStatus.Repairing;

        private string m_OwnerName;
        private string m_OwnerContactInfo;
        private eRepairStatus m_RepairStatus;
        private readonly Vehicle m_OwnerVehicle;

        private VehicleRepairRecord(string i_OwnerName, string i_OwnerContactInfo, Vehicle i_OwnerVehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerContactInfo = i_OwnerContactInfo;
            m_RepairStatus = k_DefaultRepairStatus;
            m_OwnerVehicle = i_OwnerVehicle;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_OwnerVehicle;
            }
        }

        public eRepairStatus Status
        {
            get
            {
                return m_RepairStatus;
            }

            private set
            {
                m_RepairStatus = value;
            }
        }

        public static VehicleRepairRecord CreateVehicleRepairRecord(string i_DesiredOwnerName, string i_DesiredOwnerContactInfo, Vehicle i_DesiredOwnerVehicle)
        {
            if(i_DesiredOwnerVehicle == null)
            {
                throw new ArgumentNullException();
            }

            return new VehicleRepairRecord(i_DesiredOwnerName, i_DesiredOwnerContactInfo, i_DesiredOwnerVehicle);
        }

        public void ChangeVehicleRepairStatus(eRepairStatus i_RepairStatusToChangeTo)
        {
            this.Status = i_RepairStatusToChangeTo;
        }

        public void ResetStatus()
        {
            this.Status = k_DefaultRepairStatus;
        }

        public static List<string> GetRepairStatuses()
        {
            List<string> repairStatuses = new List<string>()
            {
                "1. Fixed",
                "2. Repairing",
                "3. Paid"
            };

            return repairStatuses;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;

            if (obj is VehicleRepairRecord toCompareTo)
            {
                equals = this.m_OwnerVehicle == toCompareTo.m_OwnerVehicle;
            }

            return equals;
        }

        public static bool operator ==(VehicleRepairRecord i_ClientInfoCard1, VehicleRepairRecord i_ClientInfoCard2)
        {
            return i_ClientInfoCard1.m_OwnerVehicle == i_ClientInfoCard2.m_OwnerVehicle;
        }

        public static bool operator !=(VehicleRepairRecord i_ClientInfoCard1, VehicleRepairRecord i_ClientInfoCard2)
        {
            return !(i_ClientInfoCard1 == i_ClientInfoCard2);
        }

        public override int GetHashCode()
        {
            return m_OwnerVehicle.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle owner is: {0}
Contact information: {1}
Vehicle repair status is: {2}

The vehicle information:
{3}",
            m_OwnerName, m_OwnerContactInfo, m_RepairStatus, m_OwnerVehicle);
        }
    }
}
