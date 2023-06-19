using MassTransit;
using RepositoryLayer.Entity;
using System.Threading.Tasks;

namespace TicketConsumer.Services
{
    public class TicketUserConsumer : IConsumer<UserEntity>
    {
        public async Task Consume(ConsumeContext<UserEntity> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
