using AutoFixture;
using System.Linq;

namespace HotelReservations.Test.Extensions
{
    public static class FixtureExtension
    {
        public static Fixture SetBehaviors(this Fixture fixture)
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
