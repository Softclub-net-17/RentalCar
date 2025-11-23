using Microsoft.AspNetCore.Http;
using RentalCar.Application.Common.Results;
using RentalCar.Application.Interfaces;
using RentalCar.Application.Users.DTOS;
using RentalCar.Application.Users.Mappers;
using RentalCar.Application.Users.Queries;
using RentalCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Users.Handlers
{
    public class UserGetMeQueryHandler(IUserRepository userRepository,
        IHttpContextAccessor httpContext) : IQueryHandler<UserGetMeQuery, Result<UserGetDto>>
    {
        public async Task<Result<UserGetDto>> HandleAsync(UserGetMeQuery query)
        {
            var userIdClaim = httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Result<UserGetDto>.Fail("Unauthorized", ErrorType.Unauthorized);

            if (!int.TryParse(userIdClaim, out var userId))
                return Result<UserGetDto>.Fail("Invalid token", ErrorType.Unauthorized);

            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                return Result<UserGetDto>.Fail("User not found", ErrorType.NotFound);

            var item = user.ToDto();
            return Result<UserGetDto>.Ok(item);
        }
    }
}
