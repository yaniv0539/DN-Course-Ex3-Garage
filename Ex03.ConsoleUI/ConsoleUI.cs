using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private GarageManager m_GarageManager = new GarageManager();

        private enum eGarageMenuOptions
        {
            RegisterNewVehicle = 1,
            DisplayRegisteredVehiclesLicensePlates = 2,
            ChangeVehicleRepairStatus = 3,
            InflateVehicleWheelsToMax = 4,
            RefuelVehicleTank = 5,
            RechargeVehicleBattery = 6,
            DisplayVehicleInformation = 7,
            ExitSystem = 8
        }

        public void RunGarageSystem()
        {
            printGarageSystemTitle();

            do
            {
                printGarageMenu();
                eGarageMenuOptions userMenuSelection = getEnumInputFromUser<eGarageMenuOptions>();
                System.Console.WriteLine();

                if (userMenuSelection != eGarageMenuOptions.ExitSystem)
                {
                    executeUserMenuSelection(userMenuSelection);
                    System.Console.WriteLine("Press 'Enter' to continue...");
                    System.Console.ReadLine();
                    continue;
                }

                break;
            } while (true);
        }

        private void printGarageSystemTitle()
        {
            string garageSystemTitle = string.Format(
@"
  _____                            
 / ____|                           
| |  __  __ _ _ __ __ _  __ _  ___ 
| | |_ |/ _` | '__/ _` |/ _` |/ _ \
| |__| | (_| | | | (_| | (_| |  __/
 \_____|\__,_|_|  \__,_|\__, |\___|
                         __/ |     
                        |___/      
-----------------------------------
   Welcome to the Garage System!
-----------------------------------
");
            System.Console.WriteLine(garageSystemTitle);
        }

        private void printGarageMenu()
        {
            string garageMenu = string.Format(@"
Garage System Menu:
-----------------------------------------------------------------------
1. Register a new vehicle.
2. Display the license plates of all the vehicles.
3. Changing a vehicle state.
4. Inflate the air pressure of the wheels of a vehicle to the maximum.
5. Refuel a vehicle's tank.
6. Recharge a vehicle's battery.
7. Display a vehicle information by a license plate.
8. Exit garage system.
-----------------------------------------------------------------------
Please enter the number corresponding to your choice: ");

            System.Console.Write(garageMenu);
        }

        private void executeUserMenuSelection(eGarageMenuOptions i_UserMenuSelection)
        {
            try
            {
                switch (i_UserMenuSelection)
                {
                    case eGarageMenuOptions.RegisterNewVehicle:
                        registerNewVehicle();
                        break;
                    case eGarageMenuOptions.DisplayRegisteredVehiclesLicensePlates:
                        DisplayRegisteredVehiclesLicensePlates();
                        break;
                    case eGarageMenuOptions.ChangeVehicleRepairStatus:
                        changeVehicleRepairStatus();
                        break;
                    case eGarageMenuOptions.InflateVehicleWheelsToMax:
                        inflateVehicleWheelsToMax();
                        break;
                    case eGarageMenuOptions.RefuelVehicleTank:
                        refuelVehicleTank();
                        break;
                    case eGarageMenuOptions.RechargeVehicleBattery:
                        rechargeVehicleBattery();
                        break;
                    case eGarageMenuOptions.DisplayVehicleInformation:
                        displayVehicleInformation();
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void registerNewVehicle()
        {
            string vehicleOwnerName;
            string vehicleOwnerContactInfo;
            string vehicleLicensePlateToRegister;
            string newVehicleType;
            bool vehicleRegistered = false;
            Dictionary<string, string> vehicleAttributesList;

            System.Console.Write("Please enter the vehicle's license plate you would like to register: ");
            vehicleLicensePlateToRegister = System.Console.ReadLine();
            vehicleRegistered = m_GarageManager.IsVehicleRegistered(vehicleLicensePlateToRegister);

            if (vehicleRegistered)
            {
                System.Console.WriteLine("This vehicle is already in the system! Using the existing one.");
                m_GarageManager.ResetRepairStatus(vehicleLicensePlateToRegister);
            }
            else
            {
                System.Console.Write("Please enter the owner's name: ");
                vehicleOwnerName = System.Console.ReadLine();

                System.Console.Write("Please enter the owner's contact information: ");
                vehicleOwnerContactInfo = System.Console.ReadLine();

                newVehicleType = getVehicleTypeFromUser();
                m_GarageManager.RegisterNewVehicle(vehicleOwnerName, vehicleOwnerContactInfo,
                                                    vehicleLicensePlateToRegister, newVehicleType);
            }

            vehicleAttributesList = m_GarageManager.GetRegisteredVehicleAttribues(vehicleLicensePlateToRegister);
            changeVehicleAttributes(vehicleLicensePlateToRegister, vehicleAttributesList);
            System.Console.WriteLine("Registration Complete...");
        }

        private void DisplayRegisteredVehiclesLicensePlates()
        {
            string userInput;
            eRepairStatus? repairStatusFilter = null;
            bool validFilter = false;

            System.Console.WriteLine(
@"Please Enter:
- 'all' to display all the vehicles license plates registered in the system.
- 'filtered' to display only vehicles license plates with a specific a specific repair status.");

            do
            {
                userInput = System.Console.ReadLine().ToLower();

                if (userInput != "all" && userInput != "filtered")
                {
                    System.Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                break;
            } while (true);

            if (userInput == "filtered")
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Here are the repair statuses:");
                printStringList(GarageManager.GetRepairStatusesNames());
                System.Console.Write("Please enter the number of the wanted filter: ");
                repairStatusFilter = getEnumInputFromUser<eRepairStatus>();
                validFilter = true;
            }

            printVehiclesLicensePlatesByStatus(validFilter, repairStatusFilter);
        }

        private void changeVehicleRepairStatus()
        {
            string registeredVehicleLicensePlate = getRegisteredLicensePlateFromUser();

            printStringList(GarageManager.GetRepairStatusesNames());
            System.Console.Write("Please enter the number corresponding to your choice: ");
            eRepairStatus userNewStatus = getEnumInputFromUser<eRepairStatus>();

            m_GarageManager.ChangeVehicleRepairStatus(registeredVehicleLicensePlate, userNewStatus);
            System.Console.WriteLine("Repair Status Change Complete...");
        }

        private void inflateVehicleWheelsToMax()
        {
            string userLicensePlateInput = getRegisteredLicensePlateFromUser();

            m_GarageManager.InflateVehicleWheelsToMax(userLicensePlateInput);
            System.Console.WriteLine("Inflation Complete...");
        }

        private void refuelVehicleTank()
        {
            string registeredVehicleLicensePlate = getRegisteredLicensePlateFromUser();
            eFuelType fuelType;
            string userInput;
            bool refuelCompleted = false;
            bool parsable = false;

            printStringList(GarageManager.GetVehicleFuelTypesNames());
            System.Console.Write("Please enter the fuel type corresponding number: ");

            while (!refuelCompleted)
            {
                fuelType = getEnumInputFromUser<eFuelType>();

                System.Console.Write("Please enter the amount of liters to fuel: ");
                userInput = System.Console.ReadLine();
                parsable = float.TryParse(userInput, out float amountToFuel);

                if (!parsable)
                {
                    System.Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                try
                {
                    m_GarageManager.RefuelVehicleTankByAmount(registeredVehicleLicensePlate, fuelType, amountToFuel);
                    refuelCompleted = true;
                    System.Console.WriteLine("Refuel Complete...");
                }
                catch (ArgumentException are)
                {
                    System.Console.Write(
                        "{0}{1}Please re-enter the fuel type: ", are.Message, System.Environment.NewLine);
                }
                catch (ValueOutOfRangeException voore)
                {
                    if(voore.MinValue == voore.MaxValue)
                    {
                        System.Console.WriteLine("The fuel tank is full! Cannot add any additional fuel.");
                        break;
                    }
                    else
                    {
                        System.Console.Write(
                            "{0}{1}Please re-enter the fuel type: ", voore.Message, System.Environment.NewLine);
                    }
                }
                catch (InvalidEngineTypeException iete)
                {
                    System.Console.WriteLine(iete.Message);
                    break;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        private void rechargeVehicleBattery()
        {
            string registeredVehicleLicensePlate = getRegisteredLicensePlateFromUser();
            string userInput;
            bool rechargeCompleted = false;
            bool parsable = false;

            System.Console.Write("Please enter the amout to charge (in minutes): ");

            while (!rechargeCompleted)
            {
                userInput = System.Console.ReadLine();
                parsable = float.TryParse(userInput, out float amountToCharge);

                if (!parsable)
                {
                    System.Console.WriteLine("Invalid input! Please try again");
                    continue;
                }

                try
                {
                    m_GarageManager.RechargeVehicleBattery(registeredVehicleLicensePlate, amountToCharge / 60);
                    rechargeCompleted = true;
                    System.Console.WriteLine("Recharge Complete...");
                }
                catch (ValueOutOfRangeException voore)
                {
                    if (voore.MinValue == voore.MaxValue)
                    {
                        System.Console.WriteLine("The battery is full! Cannot add any additional power.");
                        break;
                    }
                    else
                    {
                        System.Console.Write(
                            "{0}{1}Please re-enter the amout to charge: ", voore.Message, System.Environment.NewLine);
                    }
                }
                catch (InvalidEngineTypeException iete)
                {
                    System.Console.WriteLine(iete.Message);
                    break;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        private void displayVehicleInformation()
        {
            string registeredVehicleLicensePlate = getRegisteredLicensePlateFromUser();
            string vehicleInformation = m_GarageManager.GetVehicleInformation(registeredVehicleLicensePlate);

            System.Console.WriteLine(vehicleInformation);
        }

        private void changeVehicleAttributes(string i_VehicleLicensePlateToConfigue, Dictionary<string, string> i_VehicleAttributes)
        {
            bool validAttributes = false;

            System.Console.WriteLine();
            System.Console.WriteLine("Please enter the following attributes of the vehicle:");

            while (!validAttributes)
            {
                getVehicleAttributesValuesFromUser(i_VehicleAttributes);

                try
                {
                    m_GarageManager.SetVehicleAttributesValues(i_VehicleLicensePlateToConfigue, i_VehicleAttributes);
                    validAttributes = true;
                }
                catch (FormatException fre)
                {
                    System.Console.WriteLine(fre.Message);
                }
                catch (ArgumentException are)
                {
                    System.Console.WriteLine(are.Message);
                }
                catch (ValueOutOfRangeException voore)
                {
                    System.Console.WriteLine(voore.Message);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        private void getVehicleAttributesValuesFromUser(Dictionary<string, string> io_VehicleAttributesToGet)
        {
            string userInput;

            foreach (string attribute in io_VehicleAttributesToGet.Keys.ToList())
            {
                System.Console.Write("{0}: ", attribute);
                userInput = System.Console.ReadLine();
                io_VehicleAttributesToGet[attribute] = userInput;
            }
        }

        private string getRegisteredLicensePlateFromUser()
        {
            string userLicensePlateInput;
            bool registeredVehicle = false;

            if(m_GarageManager.NumberOfVehiclesRegistered == 0)
            {
                throw new Exception("There are no vehicles in the garage!");
            }

            System.Console.Write("Please enter the registered vehicle's license plate: ");

            do
            {
                userLicensePlateInput = System.Console.ReadLine();
                registeredVehicle = m_GarageManager.IsVehicleRegistered(userLicensePlateInput);

                if (!registeredVehicle)
                {
                    System.Console.WriteLine("We dont have such vehicle in our garage. Please try again");
                    continue;
                }

                break;
            } while (true);

            return userLicensePlateInput;
        }

        private string getVehicleTypeFromUser()
        {
            string userTypeInput;

            System.Console.WriteLine();
            System.Console.WriteLine("Here are the types of vehicles this system supports:");
            printStringList(GarageManager.GetVehicleTypesNames());
            System.Console.Write("Please enter the vehicle type you'd like to register: ");

            do
            {
                userTypeInput = System.Console.ReadLine().ToLower();

                if (!GarageManager.IsValidVehicleType(userTypeInput))
                {
                    System.Console.WriteLine("Invalid vehicle type! Please try again");
                    continue;
                }

                break;
            } while (true);

            return userTypeInput;
        }

        private void printVehiclesLicensePlatesByStatus(bool i_PrintByFilter, eRepairStatus? i_StatusFilter)
        {
            int index = 1;
            List<string> licensePlatesToPrint;

            if (i_PrintByFilter)
            {
                licensePlatesToPrint = m_GarageManager.GetRegisteredVehiclesLicensePlatesByStatus(i_StatusFilter.Value);
            }
            else
            {
                licensePlatesToPrint = m_GarageManager.GetAllRegisteredVehiclesLicensePlates();
            }

            System.Console.WriteLine();

            if (licensePlatesToPrint.Count == 0)
            {
                System.Console.WriteLine("There are no vehicles registered...");
            }
            else
            {
                System.Console.WriteLine("Printing registered vehicles license plates:");

                foreach (string licensePlate in licensePlatesToPrint)
                {
                    System.Console.Write(@"{0}. ", index);
                    System.Console.WriteLine(licensePlate);
                    index++;
                }
            }
        }

        private TEnum getEnumInputFromUser<TEnum>() where TEnum : struct
        {
            TEnum userEnumInput;
            bool parsable = false;

            do
            {
                string userInput = System.Console.ReadLine();

                parsable = Enum.TryParse(userInput, out userEnumInput);

                if (!parsable || !Enum.IsDefined(typeof(TEnum), userEnumInput))
                {
                    System.Console.WriteLine("Invalid input! Please try again.");
                    continue;
                }

                break;
            } while (true);

            return userEnumInput;
        }

        private void printStringList(List<string> i_StringListToPrint)
        {
            foreach (string item in i_StringListToPrint)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
