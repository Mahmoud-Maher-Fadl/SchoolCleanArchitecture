using Application.User.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.User.Student.Queries;

public class GetStudentByPredQuery : IRequest<Result<List<StudentDto>>>
{
    public string value { get; set; }

    public class Handler : IRequestHandler<GetStudentByPredQuery, Result<List<StudentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentByPredQuery request,
            CancellationToken cancellationToken)
        {
            /*var students =await _context.Users
                    .Where(x => x.UserName == request.value || x.Address == request.value)
                    .ToListAsync(cancellationToken);*/
            var students =  _context.Users
                .Where(x => x.UserName == request.value || x.Address == request.value);
            var filteredStudents = Search(students, s => s.UserName == request.value || s.Address == request.value);
            Search(students, s => s.UserName == request.value);
            return students.Adapt<List<StudentDto>>().AsSuccessResult();
        }

        public static IEnumerable<T> Search<T>(IEnumerable<T> list, Predicate<T> p)
        {
            List<T> Items = new List<T>();
            foreach (var item in list)
            {
                if (p(item))
                    Items.Add(item);
            }

            return Items;
        }
    }
}