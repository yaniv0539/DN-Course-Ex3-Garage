using System.Collections.Generic;

namespace GarageLogic
{
    internal static class VehicleCreator
    {
        private static List<string> s_VehicleTypes = new List<string>()
        {
            "Fueled Car",
            "Electric Car",
            "Fueled Motorcycle",
            "Electric Motorcycle",
            "Truck"
        };

        public static Vehicle CreateNewVehicle(string i_LicensePlate, string i_VehicleType)
        {
            Vehicle newVehicle;

            switch (i_VehicleType)
            {
                case "fueled car":
                    newVehicle = Car.CreateCar(i_LicensePlate, FuelEngine.CreateFuelEngine(Car.k_MaximumFuelAmount, Car.k_FuelType));
                    break;
                case "electric car":
                    newVehicle = Car.CreateCar(i_LicensePlate, ElectricEngine.CreateElectricEngine(Car.k_MaximumBattaryTime));
                    break;
                case "fueled motorcycle":
                    newVehicle = Motorcycle.CreateMotorcycle(i_LicensePlate, FuelEngine.CreateFuelEngine(Motorcycle.k_MaximumFuelAmount, Motorcycle.k_FuelType));
                    break;
                case "electric motorcycle":
                    newVehicle = Motorcycle.CreateMotorcycle(i_LicensePlate, ElectricEngine.CreateElectricEngine(Motorcycle.k_MaximumBattaryTime));
                    break;
                case "truck":
                    newVehicle = Truck.CreateTruck(i_LicensePlate, FuelEngine.CreateFuelEngine(Truck.k_MaximumFuelAmount, Truck.k_FuelType));
                    break;
                default:
                    newVehicle = null;
                    break;
            }

            return newVehicle;
        }

        public static List<string> GetVehicleTypes()
        {
            return new List<string>(s_VehicleTypes);
        }

        public static bool IsValidVehicleType(string i_VehicleTypeToCheck)
        {
            bool valid = false;

            foreach (string vehicleType in s_VehicleTypes)
            {
                if (i_VehicleTypeToCheck.ToLower() == vehicleType.ToLower())
                {
                    valid = true;
                    break;
                }
            }

            return valid;
        }
    }
}
