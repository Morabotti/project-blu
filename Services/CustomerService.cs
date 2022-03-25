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
        var contact = _mapper.Map<Contact>(request);
        contact.CreatedById = user.DecodeId;

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        return new Response<ContactResponse>(
            _mapper.Map<ContactResponse>(contact)
        );
    }

    public async Task<Response<CustomerResponse>> CreateCustomerAsync(
        CreateCustomerRequest request
    )
    {
        var customer = _mapper.Map<Customer>(request);

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new Response<CustomerResponse>(
            _mapper.Map<CustomerResponse>(customer)
        );
    }

    public async Task<Response<ContactResponse>> GetContactByIdAsync(int id)
    {
        var contact = await _context.Contacts
            .Include(i => i.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return new Response<ContactResponse>(
            _mapper.Map<ContactResponse>(contact)
        );
    }

    public async Task<Response<CustomerResponse>> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers
            .Include(i => i.Contacts)
            .Include(i => i.Deals)
            .Include(i => i.Projects)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return new Response<CustomerResponse>(
            _mapper.Map<CustomerResponse>(customer)
        );
    }

    public async Task<Response<PaginationResponse<ContactResponse>>> GetContactsAsync(
        ContactQuery query,
        PaginationQuery pagination
    )
    {
        var contacts = await _context.Contacts
            .Include(i => i.Customer)
            .OrderBy(i => i.LastName)
            .Skip(pagination.Offset.GetValueOrDefault(0))
            .Take(pagination.Limit.GetValueOrDefault(20))
            .AsNoTracking()
            .ToListAsync();

        int count = await _context.Contacts.CountAsync();

        return new Response<PaginationResponse<ContactResponse>>(
            new PaginationResponse<ContactResponse>(
                _mapper.Map<List<ContactResponse>>(contacts),
                count
            )
        );
    }

    public async Task<Response<List<CustomerResponse>>> GetCustomersAsync(
        CustomerQuery query
    )
    {
        var customers = await _context.Customers
            .Include(i => i.Projects)
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<CustomerResponse>>(
            _mapper.Map<List<CustomerResponse>>(customers)
        );
    }

    public async Task<Response<ContactResponse>> UpdateContactAsync(
        ContactResponse request,
        UserResponse user
    )
    {
        var contact = await _context.Contacts
            .Where(i => user.Role == UserRole.Admin || i.CreatedById == user.DecodeId)
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if (contact is null)
        {
            return new Response<ContactResponse>(null);
        }

        UpdateMapper.MapValues(request, contact);

        await _context.SaveChangesAsync();

        return new Response<ContactResponse>(
            _mapper.Map<ContactResponse>(contact)
        );
    }

    public async Task<Response<CustomerResponse>> UpdateCustomerAsync(
        CustomerResponse request
    )
    {
        var mapped = _mapper.Map<Customer>(request);
        var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == mapped.Id);

        if (customer is null)
        {
            return new Response<CustomerResponse>(null);
        }

        UpdateMapper.MapValues(request, customer);

        await _context.SaveChangesAsync();

        return await GetCustomerByIdAsync(mapped.Id);
    }

    public async Task<Response<ContactResponse>> DeleteContactAsync(
        int id,
        UserResponse user
    )
    {
        var contact = await _context.Contacts
            .Where(i => user.Role == UserRole.Admin || i.CreatedById == user.DecodeId)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (contact is null)
        {
            return new Response<ContactResponse>(null);
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return new Response<ContactResponse>("Operation successful", HttpStatusCode.OK);
    }

    public async Task<Response<CustomerResponse>> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);

        if (customer is null)
        {
            return new Response<CustomerResponse>(null);
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return new Response<CustomerResponse>("Operation successful", HttpStatusCode.OK);
    }
}
