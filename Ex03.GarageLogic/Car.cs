using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace GarageLogic
{
    internal class Car : Vehicle
    {
        public enum eCarColor
        {
            Blue,
            White,
            Red,
            Yellow
        }

        public enum eCarNumberOfDoors
        {
            Two,
            Three,
            Four,
            Five
        }

        private const string k_CarColorKey = "Car color";
        private const string k_CarNumberOfDoorsKey = "Car number of doors";

        public const eFuelType k_FuelType = eFuelType.Octan95;
        public const float k_MaximumFuelAmount = 58f;
        public const int k_NumberOfWheels = 5;
        public const float k_MaximumWheelsAirPressure = 30;
        public const float k_MaximumBattaryTime = 4.8f;

        private eCarColor? m_Color;
        private eCarNumberOfDoors? m_NumberOfDoors;

        private Car(string i_LicensePlate, Engine i_Engine) :
            base(i_LicensePlate, i_Engine, k_MaximumWheelsAirPressure, k_NumberOfWheels)
        {
            base.Specification.Add(k_CarColorKey, string.Empty);
            base.Specification.Add(k_CarNumberOfDoorsKey, string.Empty);
            m_Color = null;
            m_NumberOfDoors = null;
        }

        public static Car CreateCar(string i_LicensePlate, Engine i_EngineType)
        {
            return new Car(i_LicensePlate, i_EngineType);
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

                    bool parsable = Enum.TryParse(value[k_CarColorKey], true, out eCarColor carColorToChange);

                    if (!parsable)
                    {
                        throw new FormatException("Car color must be blue, white, red or yellow");
                    }

                    m_Color = carColorToChange;

                    parsable = Enum.TryParse(value[k_CarNumberOfDoorsKey], true, out eCarNumberOfDoors carNumberOfDoorsToChange);

                    if (!parsable)
                    {
                        throw new FormatException("Car number of doors must be 2, 3, 4 or 5");
                    }

                    m_NumberOfDoors = carNumberOfDoorsToChange;
                }
                catch (Exception ex)
                {
                    base.ResetSpecifications();
                    m_Color = null;
                    m_NumberOfDoors = null;
                    throw ex;
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Color: {1}
Number of doors: {2}",
            base.ToString(), m_Color.ToString(), m_NumberOfDoors.ToString());
        }
    }
}
