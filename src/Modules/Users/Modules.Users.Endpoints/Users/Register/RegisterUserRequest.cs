using System.Text.Json.Serialization;

namespace Modules.Users.Endpoints.Users.Register;

public sealed record RegisterUserRequest(
    [property: JsonPropertyName("first_name")]
    string FirstName, 
    [property: JsonPropertyName("last_name")]
    string LastName, 
    [property: JsonPropertyName("email")]
    string Email, 
    [property: JsonPropertyName("password")]
    string Password
);
