using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]/[action]")]
public class IngredientController : AuthorizedController
{
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService, IUserService userService) : base(userService)
    {
        _ingredientService = ingredientService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<IngredientDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _ingredientService.GetIngredientById(id)) :
            this.ErrorMessageResult<IngredientDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{foodId:guid}")]
    public async Task<ActionResult<RequestResponse<PagedResponse<IngredientDTO>>>> GetIngredientsForFood([FromQuery] PaginationSearchQueryParams pagination, [FromRoute] Guid foodId)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _ingredientService.GetIngredients(pagination, foodId)) :
            this.ErrorMessageResult<PagedResponse<IngredientDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] IngredientDTO ingredient)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _ingredientService.AddIngredient(ingredient, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] IngredientDTO ingredient)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _ingredientService.UpdateIngredient(ingredient, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _ingredientService.DeleteIngredient(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
