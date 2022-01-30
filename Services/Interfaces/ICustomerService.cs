namespace ProjectBlu.Services.Interfaces;

public interface ICustomerService
{
    Task<Response<CustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<Response<CustomerResponse>> GetCustomerByIdAsync(int id);
    Task<Response<List<CustomerResponse>>> GetCustomersAsync(CustomerQuery query);
    Task<Response<CustomerResponse>> UpdateCustomerAsync(CustomerResponse request);
    Task<Response<CustomerResponse>> DeleteCustomerAsync(int id);

    Task<Response<ContactResponse>> CreateContactAsync(CreateContactRequest request, UserResponse user);
    Task<Response<ContactResponse>> GetContactByIdAsync(int id);
    Task<Response<PaginationResponse<ContactResponse>>> GetContactsAsync(ContactQuery query, PaginationQuery pagination);
    Task<Response<ContactResponse>> UpdateContactAsync(ContactResponse request, UserResponse user);
    Task<Response<ContactResponse>> DeleteContactAsync(int id, UserResponse user);
}
