﻿namespace Language.Parser
{
    public class Number : IExpression
    {
        public double Value { get; }

        public Number(double value)
        {
            Value = value;
        }


        public object Accept(IVisitor visitor)
        {
            return visitor.VisitNumber(this);
        }
        
        public static explicit operator double(Number number)
        {
            return number.Value;
        }
    }
}