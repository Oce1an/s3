using System.Numerics;

namespace Entities
{
	public static class GenericMath
	{
		public static T Add<T>(T a, T b) where T : IAdditionOperators<T, T, T>
		{
			return a + b;
		}

		public static T Multiply<T>(T a, T b) where T : IMultiplyOperators<T, T, T>
		{
			return a * b;
		}
	}
}