using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, VehicleRepairRecord> m_RegisteredVehiclesRecords = [];

        public int NumberOfVehiclesRegistered
        {
            get
            {
                return m_RegisteredVehiclesRecords.Count;
            }
        }

        public void RegisterNewVehicle(string i_VehicleOwnerNameToRegsiter, string i_VehicleOwnerContactInfoToRegister,
                                       string i_LicensePlateToRegister, string i_VehicleTypeToRegister)
        {
            Vehicle newVehicle = VehicleCreator.CreateNewVehicle(i_LicensePlateToRegister, i_VehicleTypeToRegister);
            VehicleRepairRecord newRepairRecord = VehicleRepairRecord.CreateVehicleRepairRecord(i_VehicleOwnerNameToRegsiter,
                                                                            i_VehicleOwnerContactInfoToRegister, newVehicle);

            m_RegisteredVehiclesRecords.Add(i_LicensePlateToRegister, newRepairRecord);
        }

        public void ChangeVehicleRepairStatus(string i_LicensePlate, eRepairStatus i_NewRepairStatus)
        {
            m_RegisteredVehiclesRecords[i_LicensePlate].ChangeVehicleRepairStatus(i_NewRepairStatus);
        }

        public void InflateVehicleWheelsToMax(string i_RegisteredLicensePlateToInflate)
        {
            m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToInflate].Vehicle.InflateWheelsToMaximum();
        }

        public void RefuelVehicleTankByAmount(string i_RegisteredLicensePlateToRefuel, eFuelType i_FuelType, float i_FuelAmount)
        {
            m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToRefuel].Vehicle.RefuelTank(i_FuelType, i_FuelAmount);
        }

        public void RechargeVehicleBattery(string i_RegisteredLicensePlateToRecharge, float i_AmountHoursToCharge)
        {
            m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToRecharge].Vehicle.RechargeBattery(i_AmountHoursToCharge);
        }

        public string GetVehicleInformation(string i_RegisteredLicensePlateToGet)
        {
            return m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToGet].ToString();
        }

        public void ResetRepairStatus(string i_RegisteredLicensePlateToReset)
        {
            m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToReset].ResetStatus();
        }

        public Dictionary<string, string> GetRegisteredVehicleAttribues(string i_RegisteredLicensePlatedToGet)
        {
            Vehicle vehicleToGetFrom = m_RegisteredVehiclesRecords[i_RegisteredLicensePlatedToGet].Vehicle;
            return vehicleToGetFrom.Specification;
        }

        public void SetVehicleAttributesValues(string i_RegisteredLicensePlateToSet, Dictionary<string, string> i_NewAttribuesValues)
        {
            Vehicle vehicleToSetTo = m_RegisteredVehiclesRecords[i_RegisteredLicensePlateToSet].Vehicle;
            vehicleToSetTo.Specification = i_NewAttribuesValues;
        }

        public bool IsVehicleRegistered(string i_LicensePlateToSearch)
        {
            return m_RegisteredVehiclesRecords.ContainsKey(i_LicensePlateToSearch);
        }

        public List<string> GetRegisteredVehiclesLicensePlatesByStatus(eRepairStatus i_RepairStatusToFilter)
        {
            List<string> vehiclesWithFilteredStatus = new List<string>();

            foreach (KeyValuePair<string, VehicleRepairRecord> record in m_RegisteredVehiclesRecords)
            {
                if (record.Value.Status == i_RepairStatusToFilter)
                {
                    vehiclesWithFilteredStatus.Add(record.Value.Vehicle.LicensePlate);
                }
            }

            return vehiclesWithFilteredStatus;
        }

        public List<string> GetAllRegisteredVehiclesLicensePlates()
        {
            List<string> allVehiclesLicensePlates = new List<string>();

            foreach (KeyValuePair<string, VehicleRepairRecord> record in m_RegisteredVehiclesRecords)
            {
                allVehiclesLicensePlates.Add(record.Value.Vehicle.LicensePlate);
            }

            return allVehiclesLicensePlates;
        }

        public static bool IsValidVehicleType(string i_VehicleTypeToCheck)
        {
            return VehicleCreator.IsValidVehicleType(i_VehicleTypeToCheck);
        }

        public static List<string> GetVehicleTypesNames()
        {
            return VehicleCreator.GetVehicleTypes();
        }

        public static List<string> GetRepairStatusesNames()
        {
            return VehicleRepairRecord.GetRepairStatuses();
        }

        public static List<string> GetVehicleFuelTypesNames()
        {
            return FuelEngine.GetFuelTypes();
        }
    }
}
