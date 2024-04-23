using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
    public static ErrorMessage FoodItemNotFound => new(HttpStatusCode.NotFound, "Food doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage IngredientNotFound => new(HttpStatusCode.NotFound, "Ingredient doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage PromotionNotFound => new(HttpStatusCode.NotFound, "Promotion doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ReservationNotFound => new(HttpStatusCode.NotFound, "Reservation doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TableNotFound => new(HttpStatusCode.NotFound, "Table doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ReviewNotFound => new(HttpStatusCode.NotFound, "Review doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProfileNotFound => new(HttpStatusCode.NotFound, "Profile doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage IngredientNotInFood => new(HttpStatusCode.NotFound, "Ingredient is not in the food!", ErrorCodes.EntityNotFound);

}
