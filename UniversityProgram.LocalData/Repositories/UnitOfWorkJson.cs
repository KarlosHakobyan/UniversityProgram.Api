using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProgram.Domain.BaseRepositories;
using UniversityProgram.Domain.BaseRepositories.CourseRepBase;
using UniversityProgram.Domain.BaseRepositories.StudentRepBase;

namespace UniversityProgram.LocalData.Repositories;
public class UnitOfWorkJson : IUnitOfWork
{
    private readonly JsonDataService _service;

    public UnitOfWorkJson(IJsonDataService service)
    {
        _service = (JsonDataService?)service;
    }
    public ICourseRepository CourseRepository => throw new NotImplementedException();

    public ICourseStudentRepository CourseStudentRepository => throw new NotImplementedException();

    public IStudentRepository StudentRepository => new StudentRepositoryJson(_service);



    public async Task Save(CancellationToken token)
    {
        await _service.SaveChangesAsync();
    }
}
