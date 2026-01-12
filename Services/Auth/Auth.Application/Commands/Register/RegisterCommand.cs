using Auth.Application.Interfaces;
using Auth.Application.Models;
using MediatR;

namespace Auth.Application.Commands.Register;

public class RegisterCommand : IRequest<AuthResponseModel>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
public class RegisterCommandHandler(IAuthDbContext _dbContext) : IRequestHandler<RegisterCommand, AuthResponseModel>
{
    public Task<AuthResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}