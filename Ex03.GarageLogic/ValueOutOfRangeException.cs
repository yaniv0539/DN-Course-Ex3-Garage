using System;

namespace GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }

        public ValueOutOfRangeException(Exception i_InnerExeption, string i_Message, float i_MinValue, float i_MaxValue)
            : base(
                  string.Format("The {0} provided is outside the valid range. Valid range: [{1}, {2}]",
                      i_Message, i_MinValue, i_MaxValue), i_InnerExeption)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
