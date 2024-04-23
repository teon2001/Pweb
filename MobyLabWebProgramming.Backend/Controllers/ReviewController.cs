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

[ApiController]
[Route("api/[controller]")]
public class ReviewController : AuthorizedController
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService, IUserService userService) : base(userService)
    {
        _reviewService = reviewService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ReviewDTO>>> GetReviewById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _reviewService.GetReviewById(id)) :
            this.ErrorMessageResult<ReviewDTO>(currentUser.Error);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<RequestResponse<PagedResponse<ReviewDTO>>>> GetReviews([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _reviewService.GetReviews(pagination)) :
            this.ErrorMessageResult<PagedResponse<ReviewDTO>>(currentUser.Error);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<RequestResponse>> AddReview([FromBody] ReviewDTO review)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _reviewService.AddReview(review, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    //[HttpPut("{id}")]
    //[Authorize]
    //public async Task<ActionResult<RequestResponse>> UpdateReview(Guid id, [FromBody] ReviewDTO review)
    //{
    //    var currentUser = await _userService.GetCurrentUser(); // Assumption: There is a method to get current user's details.
    //    review.Id = id; // Ensure the ID is set from path.
    //    var result = await _reviewService.UpdateReview(review, currentUser);
    //    if (result.IsSuccess)
    //        return Ok(result);
    //    return BadRequest(result.ErrorMessage);
    //}

    //[HttpDelete("{id}")]
    //[Authorize]
    //public async Task<ActionResult<RequestResponse>> DeleteReview(Guid id)
    //{
    //    var currentUser = await _userService.GetCurrentUser();
    //    var result = await _reviewService.DeleteReview(id, currentUser);
    //    if (result.IsSuccess)
    //        return Ok(result);
    //    return BadRequest(result.ErrorMessage);
    //}
}
