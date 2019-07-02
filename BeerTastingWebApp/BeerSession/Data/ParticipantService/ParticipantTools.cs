using BeerSession.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Data.ParticipantService
{
    public class ParticipantTools : IParticipantTools
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;
        public ParticipantTools(ApplicationDbContext _dbContext, IConfiguration config)
        {
            dbContext = _dbContext;
            configuration = config;
        }
        public async Task GetParticipantAsync(string tastingId, string email, string name)
        {
            var tastingObject = dbContext.Tasting.First(z => z.TastingTag.ToString() == tastingId);
            var newPart = new Participant
            {
                Name = name,
                Email = email,
                Tasting = tastingObject
            };

            dbContext.Add(newPart);
            await dbContext.SaveChangesAsync();
            SendMailToParticipant(newPart, tastingId);
        }

        public async Task RemoveParticipant(string tastingId, string name)
        {
            var tastingObject = dbContext.Tasting.Include(i => i.Participants).First(z => z.TastingTag.ToString() == tastingId);

            var participant = tastingObject.Participants.First(p => p.Name == name);

            dbContext.Remove(participant);
            await dbContext.SaveChangesAsync();
        }

        public async Task SendMailToParticipant(Participant participant, string tastingId)
        {
            var apiKey = configuration.GetValue<string>("APIKeys:SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@beersessions.com", "BeerSession Meister");
            var subject = "Trying to send a email to a partisipant";
            var to = new EmailAddress(participant.Email, participant.Name);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = $"<a href='https://localhost:44349/tasting/catchparticipant?tastingId={tastingId}'>Join the session</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                dbContext.Participant.First(w => w.Email == participant.Email).MailSent = true;
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
