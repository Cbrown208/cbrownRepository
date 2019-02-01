using System;

namespace Contracts
{
  public interface ISomethingHappened
  {
    string What { get; }
    DateTime When { get; }
  }
}
