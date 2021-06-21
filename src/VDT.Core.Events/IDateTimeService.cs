using System;

namespace VDT.Core.Events {
    /// <summary>
    /// Service for static date and time members
    /// </summary>
    public interface IDateTimeService {
        /// <summary>
        /// Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; } 
    }
}
