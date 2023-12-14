using API.Context.Repository;
using API.Models;
using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Departament;

public class GetDepartamentsQueryHandler : IRequestHandler<GetDepartamentsQuery, IEnumerable<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;

    public GetDepartamentsQueryHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<DepartmentDto>> Handle(GetDepartamentsQuery request, CancellationToken cancellationToken)
    {
        var departments = _departmentRepository.GetAllAsNoTracking()
            .Select(x => new DepartmentDto(x.Name, x.Address, x.Email, x.Phone, x.DataCenter)).ToList();

        return departments;
    }
}