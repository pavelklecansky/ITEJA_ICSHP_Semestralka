using System;
using System.Collections.Generic;

namespace Language
{
    public class Environment
    {
        private Environment parentEnviroment;
        private Dictionary<string, object> values = new();


        public Environment()
        {
            parentEnviroment = null;
        }

        public Environment(Environment parentEnviroment)
        {
            this.parentEnviroment = parentEnviroment;
        }

        public object Get(string name)
        {
            if (values.TryGetValue(name,out object value))
            {
                return value;
            }

            if (parentEnviroment != null)
            {
                return parentEnviroment.Get(name);
            }

            throw new ArgumentException("Variable don't exists");
        }
        
        public void Define(string name, object value)
        {
            values.Add(name, value);
        }

        public void Assign(string name, object value)
        {
            if (values.ContainsKey(name))
            {
                values[name] = value;
                return;
            }
            
            if (parentEnviroment != null)
            {
                parentEnviroment.Assign(name,value);
                return;
            }
            
            throw new ArgumentException("Undefined variable");
        }
    }
}