using System;

namespace GarageLogic
{
    public class InvalidEngineTypeException : Exception
    {
        public InvalidEngineTypeException(string i_Message) : 
            base(string.Format(
                "Invalid action on the provided engine!{0}{1}", System.Environment.NewLine, i_Message))
        {
        }
    }
}
