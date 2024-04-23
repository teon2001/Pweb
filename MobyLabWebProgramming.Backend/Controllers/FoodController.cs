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

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// Controller for managing food items.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class FoodController : AuthorizedController
{
    private readonly IFoodService _foodItemService;

    public FoodController(IFoodService foodItemService, IUserService userService) : base(userService)
    {
        _foodItemService = foodItemService;
    }

    /// <summary>
    /// Retrieves a single food item by ID.
    /// </summary>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<FoodDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _foodItemService.GetFoodItemById(id)) :
            this.ErrorMessageResult<FoodDTO>(currentUser.Error);
    }

    /// <summary>
    /// Retrieves a page of food items.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<FoodDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _foodItemService.GetFoodItems(pagination)) :
            this.ErrorMessageResult<PagedResponse<FoodDTO>>(currentUser.Error);
    }

    /// <summary>
    /// Adds a new food item.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] FoodDTO foodItem)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _foodItemService.AddFoodItem(foodItem, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// Updates an existing food item.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] FoodDTO foodItem)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _foodItemService.UpdateFoodItem(foodItem, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
    /// <summary>
    /// Deletes a food item.
    /// </summary>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _foodItemService.DeleteFoodItem(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// Adds an ingredient to a food item.
    /// </summary>
    [Authorize]
    [HttpPost("{foodId:guid}/addIngredient/{ingredientId:guid}")]
    public async Task<ActionResult<RequestResponse>> AddIngredientToFood(Guid foodId, Guid ingredientId)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Result != null)
        {
            var result = await _foodItemService.AddIngredientToFood(foodId, ingredientId);
            return this.FromServiceResponse(result);
        }
        return this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// Removes an ingredient from a food item.
    /// </summary>
    [Authorize]
    [HttpDelete("{foodId:guid}/removeIngredient/{ingredientId:guid}")]
    public async Task<ActionResult<RequestResponse>> RemoveIngredientFromFood(Guid foodId, Guid ingredientId)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Result != null)
        {
            var result = await _foodItemService.RemoveIngredientFromFood(foodId, ingredientId);
            return this.FromServiceResponse(result);
        }
        return this.ErrorMessageResult(currentUser.Error);
    }
}
 
