using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProfileController : AuthorizedController
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService, IUserService userService) : base(userService)
    {
        _profileService = profileService;
    }


    [Authorize]
    [HttpGet("{userId:Guid}")]
    public async Task<ActionResult<RequestResponse<ProfileDTO>>> GetProfileByUserId([FromRoute] Guid userId)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _profileService.GetProfileById(userId)) :
            this.ErrorMessageResult<ProfileDTO>(currentUser.Error);  
    }
    /// <summary>
    /// Adds a new food item.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ProfileDTO profile)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _profileService.AddProfile(profile, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    /// <summary>
    /// Updates an existing food item.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ProfileDTO profile)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _profileService.UpdateProfile(profile, currentUser.Result)) :
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
            this.FromServiceResponse(await _profileService.DeleteProfile(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
