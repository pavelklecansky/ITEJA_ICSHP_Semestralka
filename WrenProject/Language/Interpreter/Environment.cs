using System;
using System.Collections.Generic;

namespace Language.Interpreter
{
    /// <summary>
    /// Store bindings of names and values.
    /// </summary>
    internal class Environment
    {
        private readonly Environment _parentEnvironment;
        private readonly Dictionary<string, object> _values = new();


        public Environment()
        {
            _parentEnvironment = null;
        }

        public Environment(Environment parentEnvironment)
        {
            _parentEnvironment = parentEnvironment;
        }

        /// <summary>
        /// Return stored value by name
        /// </summary>
        public object Get(string name)
        {
            if (_values.TryGetValue(name, out var value))
            {
                return value;
            }

            if (_parentEnvironment != null)
            {
                return _parentEnvironment.Get(name);
            }

            throw new ArgumentException("Variable don't exists");
        }

        /// <summary>
        /// Creates new name and value binding.
        /// </summary>
        public void Define(string name, object value)
        {
            _values.Add(name, value);
        }

        /// <summary>
        /// Reassing new value to existing name.
        /// </summary>
        public void Assign(string name, object value)
        {
            if (_values.ContainsKey(name))
            {
                _values[name] = value;
                return;
            }

            if (_parentEnvironment != null)
            {
                _parentEnvironment.Assign(name, value);
                return;
            }

            throw new ArgumentException("Undefined variable");
        }
    }
}