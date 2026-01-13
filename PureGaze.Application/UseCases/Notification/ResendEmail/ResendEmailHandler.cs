using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Notification.ResendEmail;

public class ResendEmailHandler(
    IEmailRepository emailRepository,
    IEmailSender emailSender) 
    : IRequestHandler<ResendEmailCommand>
{
    public async Task Handle(ResendEmailCommand command, CancellationToken ct)
    {
        var email = await emailRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Email with id {command.Id} not found");
        
        try
        {
            await emailSender.SendAsync(email, ct);
            
            email.Status = EmailStatus.Sent;
            email.SentAt = DateTime.UtcNow;
        }
        catch (Exception e)
        { 
            //TODO: log exception    
        }
        
        email.UpdatedAt = DateTime.UtcNow;
        email.RetryCount++;
        
        await emailRepository.SaveChangesAsync(ct);
    }
}