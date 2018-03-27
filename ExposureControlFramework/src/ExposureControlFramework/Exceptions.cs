namespace ExposureControlFramework
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception throw when a requested exposureControlSettings was not found in the repository.
    /// </summary>
    [Serializable]
    public class ExposureControlSettingsNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsNotFoundException"/> class with no arguments.
        /// </summary>
        public ExposureControlSettingsNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsNotFoundException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name='message'>The message that explains the reason for the exception.</param>
        public ExposureControlSettingsNotFoundException(string message)
            : base(message)
        {
        }

        // Constructor form 3 (message, exception)
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsNotFoundException"/> class
        /// with a specified error message and a reference to an inner exception.
        /// </summary>
        /// <param name='message'>The message that explains the reason for the exception.</param>
        /// <param name='innerException'>The exception that is the cause of the current exception.
        /// If it is not a null reference, the current exception is raised in a catch block
        /// that handles the inner exception.</param>
        protected ExposureControlSettingsNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Constructor form 4 (info, context)
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsNotFoundException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SeraizliationInfo"/> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        /// information about the source or destination.</param>
        protected ExposureControlSettingsNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception throw when we try to create an exposureControlSettings in the repository, and an exposureControlSettings with the same name
    /// already exists
    /// </summary>
    [Serializable]
    public class ExposureControlSettingsAlreadyExistsException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsAlreadyExistsException"/> class with no arguments.
        /// </summary>
        public ExposureControlSettingsAlreadyExistsException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsAlreadyExistsException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name='message'>The message that explains the reason for the exception.</param>
        public ExposureControlSettingsAlreadyExistsException(string message)
            : base(message)
        {
        }

        // Constructor form 3 (message, exception)
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsAlreadyExistsException"/> class
        /// with a specified error message and a reference to an inner exception.
        /// </summary>
        /// <param name='message'>The message that explains the reason for the exception.</param>
        /// <param name='innerException'>The exception that is the cause of the current exception.
        /// If it is not a null reference, the current exception is raised in a catch block
        /// that handles the inner exception.</param>
        protected ExposureControlSettingsAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Constructor form 4 (info, context)
        /// <summary>
        /// Creates a new instance of the <see cref="ExposureControlSettingsAlreadyExistsException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SeraizliationInfo"/> that holds the serialized
        /// object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        /// information about the source or destination.</param>
        protected ExposureControlSettingsAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
