//using System.ComponentModel.DataAnnotations;

//namespace Nicosia.Assessment.WebApi.Controllers.Authenticate.V1.Models;

//public class AuthenticateResponse
//{
//    public UserDto User { get; set; } = null!;

//    [Required]
//    public string Token { get; set; } = null!;

//    public static AuthenticateResponse Build(AuthenticatedUserDto authenticatedUserDto)
//    {
//        return new AuthenticateResponse
//        {
//            User = new UserDto
//            {
//                Id = authenticatedUserDto.Id,
//                Email = authenticatedUserDto.Email,
//                FirstName = authenticatedUserDto.FirstName,
//                LastName = authenticatedUserDto.LastName,
//                Role = authenticatedUserDto.Role,
//                Phone = authenticatedUserDto.Avatar,
//            },
//            Token = authenticatedUserDto.Token
//        };
//    }
//}