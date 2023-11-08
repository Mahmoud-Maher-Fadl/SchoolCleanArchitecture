﻿using Application.Student.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Student.Queries;

public class GetStudentByPredQuery:IRequest<Result<List<StudentDto>>>
{
    public string value { get; set; }
    public class Handler:IRequestHandler<GetStudentByPredQuery,Result<List<StudentDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentByPredQuery request, CancellationToken cancellationToken)
        {
            var students =await _context.Students
                .ProjectToType<StudentDto>()
               // .AsQueryable()  // doesn't matter cause the ToListAsync Usage
                .ToListAsync(cancellationToken);
            var filteredStudents = Search(students, s => s.Name == request.value||s.Address == request.value);
            //Search(students,s=>s.Name==request.value);
            return filteredStudents.ToList().AsSuccessResult();

        }
        public static IEnumerable<T> Search<T>(IEnumerable<T> list,Predicate<T> p)
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