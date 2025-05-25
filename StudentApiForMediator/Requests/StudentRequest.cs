using Mediator;

namespace StudentApiForMediator.Requests
{
    public class StudentAddRequest : IRequest<StudentAddResponse>
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class StudentAddResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
    }

}
