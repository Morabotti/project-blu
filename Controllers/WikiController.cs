using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class WikiController : ApiController
{
    private readonly IWikiService _wikiService;

    public WikiController(IWikiService wikiService)
    {
        _wikiService = wikiService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateWikiCategoryRequest request
    )
    {
        var response = await _wikiService.CreateWikiCategoryAsync(request);
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var response = await _wikiService.GetWikiCategoriesAsync(GetUserClaim());
        return HttpResponse(response);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
    {
        var response = await _wikiService.GetWikiCategoryByIdAsync(
            categoryId,
            GetUserClaim()
        );

        return HttpResponse(response);
    }

    [HttpPut("{categoryId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory([FromBody] WikiCategoryResponse request)
    {
        var response = await _wikiService.UpdateWikiCategoryAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("{categoryId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        var response = await _wikiService.DeleteWikiCategoryAsync(categoryId);
        return HttpResponse(response);
    }

    [HttpPost("article")]
    public async Task<IActionResult> CreateArticle(
        [FromBody] CreateWikiArticleRequest request
    )
    {
        var response = await _wikiService.CreateWikiArticleAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpGet("article/{articleId}")]
    public async Task<IActionResult> GetArticleById([FromRoute] int articleId)
    {
        var response = await _wikiService.GetWikiArticleByIdAsync(articleId, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpPut("article/{articleId}")]
    public async Task<IActionResult> UpdateArticle([FromBody] WikiArticleResponse request)
    {
        var response = await _wikiService.UpdateWikiArticleAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpDelete("article/{articleId}")]
    public async Task<IActionResult> DeleteArticle([FromRoute] int articleId)
    {
        var response = await _wikiService.DeleteWikiArticleAsync(articleId, GetUserClaim());
        return HttpResponse(response);
    }
}