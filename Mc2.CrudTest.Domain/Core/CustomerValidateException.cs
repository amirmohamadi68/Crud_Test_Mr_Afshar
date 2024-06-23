using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Core
{

    [Serializable]
    public sealed class CustomerValidateException : Exception   /// i now they must be abstract exeption class or generic class
                                                                /// and customer exceptions must be in use cases layer not here
                                                                /// and the exceptions in domain layer must throw standard exceptions
                                                                /// and check that in use cases (Appplication) layer and there trowing Generic Exceptions to exceptionHandlers midleware
                                                                /// but i think would not have time to doing thoese...im sorry
    {
        private List<ValidationError> DataError { get; set; }

        public CustomerValidateException(List<ValidationError> errors)
        {
            DataError = errors;
        }

        protected CustomerValidateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DataError = (List<ValidationError>)info.GetValue("DataError", typeof(List<ValidationError>));
        }

        public List<ValidationError> GetErrors()
        {
            return DataError;
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // Serialize the DataError list by adding it to the SerializationInfo object
            info.AddValue("DataError", DataError, typeof(List<ValidationError>));
        }
    }
}
