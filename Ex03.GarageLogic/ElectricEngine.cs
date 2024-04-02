namespace GarageLogic
{
    internal class ElectricEngine : Engine
    {
        private ElectricEngine(float i_MaximumBatteryHours) : base(i_MaximumBatteryHours)
        {
        }

        public float BatteryRemainingHours
        {
            get
            {
                return this.CurrentEnergySourceSupply;
            }
        }

        public float BatteryMaximumHours
        {
            get
            {
                return this.EnergySourceCapacity;
            }
        }

        public static ElectricEngine CreateElectricEngine(float i_DesiredMaximumBatteryHours)
        {
            ElectricEngine theElectricEngine = new ElectricEngine(i_DesiredMaximumBatteryHours);

            return theElectricEngine;
        }

        public void RechargeBattery(float i_AmoutOfHoursToCharge)
        {
            this.AddEnergySourceSupply(i_AmoutOfHoursToCharge);
        }

        public void EmptyBattery()
        {
            this.EmptyEnergySourceSupply();
        }

        public override string GetEnergySourceRequiredName()
        {
            return string.Format("Battery time");
        }

        public override string ToString()
        {
            return string.Format(
@"Battery's remaining time in hours : {0}
Battery's Maximum time in hours: {1}",
            this.BatteryRemainingHours, this.BatteryMaximumHours);
        }
    }
}
