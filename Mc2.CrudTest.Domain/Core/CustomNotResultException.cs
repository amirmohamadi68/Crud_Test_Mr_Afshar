using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Core
{
    [Serializable]
    public sealed class CustomNotResultException : Exception
    {
        public NotResultTypeEnum Type { get; set; }

        public CustomNotResultException(NotResultTypeEnum type)
        {
            Type = type;
        }
        protected CustomNotResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                Type = (NotResultTypeEnum)info.GetValue("Type", typeof(NotResultTypeEnum));
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // Add the custom property to the SerializationInfo object
            info.AddValue("Type", Type, typeof(NotResultTypeEnum));
        }

    }
    public enum NotResultTypeEnum
    {
        Duplicated,
        NotExsistByFirstLastBirth,
        NotAnyCustomer,
        NotCustomerByEmail,
        NotExsist,
        InternalFail

    }
}

