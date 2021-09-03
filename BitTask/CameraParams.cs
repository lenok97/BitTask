using System;

namespace BitTask
{ 
	public class CameraParams
	{
		private float averageHumanHeight = 160;

        public CameraParams(float distance, float heightFromFloor)
        {
            Distance = ValidateParam(distance);
            HeightFromFloor = ValidateParam(heightFromFloor);
        }

        public long Id { get; set; }

		public float Distance { get; set; }

        public float HeightFromFloor { get; set; }

        public float HeightFromObject 
        { get => 
			Math.Abs(HeightFromFloor - averageHumanHeight); }

        public float VerticalAngle 
		{ get => 
				(float)((180 / Math.PI) * Math.Atan(HeightFromObject / Distance));}

		private float ValidateParam(float param)
        {
			if (param <= 0)
				throw new ArgumentException("Parameter should be > 0");
			return param;
        }
	}
}
