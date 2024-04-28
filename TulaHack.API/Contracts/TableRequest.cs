using TulaHack.Core.Models;

namespace TulaHack.API.Contracts
{
    public record TableRequest(
        Guid id,
        Guid schemeId,
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
