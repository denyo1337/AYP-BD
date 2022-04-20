using MediatR;

namespace Application.Functions.Commands
{
    public class TestCommand : IRequest<string>
    {
        public string Test { get; set; }
        public TestCommand(string test)
        {
            Test = test;
        }
    }

    public class TestCommandHandler : IRequestHandler<TestCommand, string>
    {
        public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.Test);
        }
    }
}
