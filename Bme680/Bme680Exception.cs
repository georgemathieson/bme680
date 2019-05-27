using System;
using System.Runtime.Serialization;

namespace Bme680
{
    /// <summary>
    /// For identifying exceptional circumstances in a <see cref="Bme680"/> context.
    /// </summary>
    [Serializable]
    public class Bme680Exception : Exception
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680Exception"/> class.
        /// </summary>
        /// <param name="message">The message to create with.</param>
        public Bme680Exception(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Bme680Exception"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to create with.</param>
        /// <param name="context">The <see cref="StreamingContext"/> to create with.</param>
        protected Bme680Exception(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
