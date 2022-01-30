using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        var response = await _customerService.CreateCustomerAsync(request);
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers(
        [FromQuery] CustomerQuery customerQuery
    )
    {
        var response = await _customerService.GetCustomersAsync(customerQuery);
        return HttpResponse(response);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int customerId)
    {
        var response = await _customerService.GetCustomerByIdAsync(customerId);
        return HttpResponse(response);
    }

    [HttpPut("{customerId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerResponse request)
    {
        var response = await _customerService.UpdateCustomerAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("{customerId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
    {
        var response = await _customerService.DeleteCustomerAsync(customerId);
        return HttpResponse(response);
    }

    [HttpPost("contact")]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
    {
        var response = await _customerService.CreateContactAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpGet("contact")]
    public async Task<IActionResult> GetContacts(
        [FromQuery] ContactQuery query,
        [FromQuery] PaginationQuery pagination
    )
    {
        var response = await _customerService.GetContactsAsync(query, pagination);
        return HttpResponse(response);
    }

    [HttpGet("contact/{contactId}")]
    public async Task<IActionResult> GetContactById([FromRoute] int contactId)
    {
        var response = await _customerService.GetContactByIdAsync(contactId);
        return HttpResponse(response);
    }

    [HttpPut("contact/{contactId}")]
    public async Task<IActionResult> UpdateContact([FromBody] ContactResponse request)
    {
        var response = await _customerService.UpdateContactAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpDelete("contact/{contactId}")]
    public async Task<IActionResult> DeleteContact([FromRoute] int contactId)
    {
        var response = await _customerService.DeleteContactAsync(contactId, GetUserClaim());
        return HttpResponse(response);
    }
}