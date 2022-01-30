using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class CustomerService : ICustomerService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public CustomerService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<ContactResponse>> CreateContactAsync(
        CreateContactRequest request,
        UserResponse user
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<CustomerResponse>> CreateCustomerAsync(
        CreateCustomerRequest request
    )
    {
        throw new NotImplementedException();
    }


    public async Task<Response<ContactResponse>> DeleteContactAsync(
        int id,
        UserResponse user
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<CustomerResponse>> DeleteCustomerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ContactResponse>> GetContactByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<PaginationResponse<ContactResponse>>> GetContactsAsync(
        ContactQuery query,
        PaginationQuery pagination
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<CustomerResponse>> GetCustomerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<CustomerResponse>>> GetCustomersAsync(
        CustomerQuery query
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ContactResponse>> UpdateContactAsync(
        ContactResponse request,
        UserResponse user
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<CustomerResponse>> UpdateCustomerAsync(
        CustomerResponse request
    )
    {
        throw new NotImplementedException();
    }
}
