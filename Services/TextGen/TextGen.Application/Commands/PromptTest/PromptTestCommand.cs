using MediatR;
using TextGen.Application.Services;

namespace TextGen.Application.Commands.PromptText;

public class PromptTestCommand: IRequest<string>
{
    public string Prompt { get; set; } = null;
}
public class PromptTestCommandHandler(ILlmClient _llmClient) : IRequestHandler<PromptTestCommand, string>
{
    public async Task<string> Handle(PromptTestCommand request, CancellationToken cancellationToken)
    {
        var textResult = await _llmClient.PromptTest<PromptTestResponseModel>(request.Prompt, cancellationToken);

        return textResult.Response;
    }

    private class PromptTestResponseModel
    {
        public string Response { get; set; } = null;
    }
}