using System;

namespace Contracts
{
	public class SomethingHappened : ISomethingHappened
  {
    public string What { get; set; }
    public DateTime When { get; set; }
  }
}
