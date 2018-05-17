using MvcExercise.Core.Models;

namespace MvcExercise.Infrastructure.Interfaces
{
	public interface IContactManager
	{
		bool AddContact(Contact contact);
	}
}
