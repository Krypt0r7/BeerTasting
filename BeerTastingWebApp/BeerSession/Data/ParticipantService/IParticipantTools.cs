using System.Threading.Tasks;
using BeerSession.Models;

namespace BeerSession.Data.ParticipantService
{
    public interface IParticipantTools
    {
        Task GetParticipantAsync(string tastingId, string email, string name);
        Task RemoveParticipant(string tastingId, string name);
        Task SendMailToParticipant(Participant participant, string tastingId);
    }
}