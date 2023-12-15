using API.Context.Repository;
using MediatR;

namespace BooksAPI.Command.Department;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
{
    private readonly IRepository<API.Models.Department> _departmentRepository;

    public CreateDepartmentCommandHandler(IRepository<API.Models.Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = API.Models.Department.Create(request.DepartmentDto.Name, request.DepartmentDto.Address,
            request.DepartmentDto.Email, request.DepartmentDto.Phone, request.DepartmentDto.DataCenter);


        await _departmentRepository.AddAsync(department, department.DataCenter);
        await _departmentRepository.SaveAsync(department.DataCenter);
    }
}