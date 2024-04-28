using TulaHack.Core.Models;

namespace TulaHack.API.Contracts
{
    public record TableResponse(
        Guid id,
        Guid schemeId,
        SchemeResponse scheme,
        int numberOfPeople,
        float x,
        float y,
        float width,
        float height,
        float scaleX,
        float scaleY,
        float rotation,
        float radius,
        string fill,
        string type
        );
}
