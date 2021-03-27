using System;
using System.Collections.Generic;

namespace Language
{
    /// <summary>
    /// Store bindings of names and values.
    /// </summary>
    internal class Environment
    {
        private readonly Environment parentEnvironment;
        private readonly Dictionary<string, object> values = new();


        public Environment()
        {
            parentEnvironment = null;
        }

        public Environment(Environment parentEnvironment)
        {
            this.parentEnvironment = parentEnvironment;
        }

        /// <summary>
        /// Return stored value by name
        /// </summary>
        public object Get(string name)
        {
            if (values.TryGetValue(name, out var value))
            {
                return value;
            }

            if (parentEnvironment != null)
            {
                return parentEnvironment.Get(name);
            }

            throw new ArgumentException("Variable don't exists");
        }

        /// <summary>
        /// Creates new name and value binding.
        /// </summary>
        public void Define(string name, object value)
        {
            values.Add(name, value);
        }

        /// <summary>
        /// Reassing new value to existing name.
        /// </summary>
        public void Assign(string name, object value)
        {
            if (values.ContainsKey(name))
            {
                values[name] = value;
                return;
            }

            if (parentEnvironment != null)
            {
                parentEnvironment.Assign(name, value);
                return;
            }

            throw new ArgumentException("Undefined variable");
        }
    }
}