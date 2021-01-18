using System;

namespace DotNetCoords
{
  /// <summary>
  /// NotDefinedOnUtmGridException exception class, thrown when parameters are provided that do not fit 
  /// into the Universal Transverse Mercator grid
  /// </summary>
  [Serializable]
  public sealed class NotDefinedOnUtmGridException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NotDefinedOnUtmGridException"/> class.
    /// </summary>
    public NotDefinedOnUtmGridException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotDefinedOnUtmGridException"/> class.
    /// </summary>
    /// <param name="message">The message details of the exception.</param>
    public NotDefinedOnUtmGridException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotDefinedOnUtmGridException"/> class.
    /// </summary>
    /// <param name="message">The message details of the exception.</param>
    /// <param name="innerException">The inner exception.</param>
    public NotDefinedOnUtmGridException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
