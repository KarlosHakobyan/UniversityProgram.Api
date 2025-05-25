using Mediator;

namespace StudentApiForMediator.Requests
{
    public class StudentAddRequest : IRequest<StudentAddResult>
    {
        public string Name { get; set; } = default!;

        public StudentAddRequest(string name)
        {
            Name = name;
        }
    }

    public class StudentAddResult
    {
    }
}
