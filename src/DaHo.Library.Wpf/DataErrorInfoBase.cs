using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DaHo.Library.Wpf
{
    public abstract class DataErrorInfoBase : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly List<ValidationResult> _manualValidationResults = new List<ValidationResult>();
        private readonly object _lock = new object();

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if the entity currently has validation errors; otherwise, <see langword="false" />.
        /// </returns>
        public bool HasErrors => _errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or <see langword="null" /> or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName = null)
        {
            lock (_lock)
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    // Return all errors for specified property
                    if (_errors.ContainsKey(propertyName) && _errors[propertyName] != null &&
                        _errors[propertyName].Count > 0)
                        return _errors[propertyName].ToList();

                    return null;

                }

                // Return all errors from all properties
                return _errors.SelectMany(err => err.Value.ToList());
            }
        }

        /// <summary>
        /// Notifies listeners that a property validation error has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if the value is valid for the property
        /// </summary>
        /// <param name="value">The value which should be checked</param>
        /// <param name="propertyName">The name of the property which should be validated.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"></see> </param>
        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null)
                {
                    MemberName = propertyName
                };

                var validationResults = new List<ValidationResult>();
                Validator.TryValidateProperty(value, validationContext, validationResults);

                //clear previous _errors from tested property  
                if (_errors.ContainsKey(propertyName))
                    _errors.Remove(propertyName);

                OnErrorsChanged(propertyName);

                HandleValidationResults(validationResults);
                HandleValidationResults(_manualValidationResults);

                _manualValidationResults.RemoveAll(x => x.MemberNames.Contains(propertyName));
            }
        }

        /// <summary>
        /// Checks if the value is valid for all properties in the current object
        /// </summary>
        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                //clear all previous _errors  
                var propNames = _errors.Keys.ToList();
                _errors.Clear();
                propNames.ForEach(OnErrorsChanged);

                HandleValidationResults(validationResults);
                HandleValidationResults(_manualValidationResults);

                _manualValidationResults.Clear();
            }
        }

        /// <summary>
        /// Adds a new data error for the specified propery
        /// </summary>
        /// <param name="propertyName">The name of the property which contains a data error</param>
        /// <param name="message">The message for the data error</param>
        public void AddNewDataError(string propertyName, string message)
        {
            _manualValidationResults.Add(new ValidationResult(message, new[] { propertyName }));
        }

        private void HandleValidationResults(ICollection<ValidationResult> validationResults)
        {
            //Group validation results by property names  
            var resultsByPropNames = validationResults
                .SelectMany(result => result.MemberNames, (result, name) => new { result, name })
                .GroupBy(t => t.name, t => t.result);

            //add _errors to dictionary and inform binding engine about _errors  
            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                _errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }
        }

    }
}
