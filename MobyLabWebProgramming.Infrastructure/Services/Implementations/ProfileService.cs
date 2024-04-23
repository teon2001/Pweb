using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;
        public ProfileService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PagedResponse<ProfileDTO>>> GetProfiles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken)
        {
            var profiles = await _repository.PageAsync(pagination, new ProfileProjectionSpec(), cancellationToken);
            return ServiceResponse<PagedResponse<ProfileDTO>>.ForSuccess(profiles);
        }
        public async Task<ServiceResponse> AddProfile(ProfileDTO profile, UserDTO requestingUser, CancellationToken cancellationToken = default)
        { 
            var existingProfile = await _repository.GetAsync<Profile>(new ProfileProjectionSpec(profile.Id), cancellationToken);
            if(existingProfile != null)
            {
                return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Profile already exists."));
            }
            
            var gettingUser = await _repository.GetAsync<User>(new UserProjectionSpec(profile.UserId), cancellationToken);
            
            var newProfile = new Profile
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DateOfBirth = profile.DateOfBirth,
                Bio = profile.Bio,
                User = gettingUser,
                UserId = requestingUser.Id
            };

            await _repository.AddAsync(newProfile, cancellationToken);
            return ServiceResponse.ForSuccess();
        }
        public async Task<ServiceResponse<ProfileDTO>> GetProfileById(Guid id, CancellationToken cancellationToken = default)
        {
            var profile = await _repository.GetAsync(new ProfileProjectionSpec(id), cancellationToken);
            return profile != null ?
                ServiceResponse<ProfileDTO>.ForSuccess(profile) :
                ServiceResponse<ProfileDTO>.FromError(CommonErrors.ProfileNotFound);
        }

        public async Task<ServiceResponse> UpdateProfile(ProfileDTO profile, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            //VREAU DOAR USER CURENT SA POAET PUNA PROFILE
            //if (requestingUser.Role != UserRoleEnum.Admin)
            //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can update promotions."));

            var existingProfile = await _repository.GetAsync<Profile>(new ProfileProjectionSpec(requestingUser.Id), cancellationToken);
            if (existingProfile == null)
                return ServiceResponse.FromError(CommonErrors.ProfileNotFound);
            existingProfile.Id = profile.Id;
            existingProfile.FirstName = profile.FirstName;
            existingProfile.LastName = profile.LastName;
            existingProfile.DateOfBirth = profile.DateOfBirth;
            existingProfile.Bio = profile.Bio;
            existingProfile.UserId = requestingUser.Id;
            await _repository.UpdateAsync<Profile>(existingProfile, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteProfile(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            //NUUUUUUUUUUUUUUUUUU
            //if (requestingUser.Role != UserRoleEnum.Admin)
            //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can delete profiles."));

            await _repository.DeleteAsync<Profile>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }
    }
}
